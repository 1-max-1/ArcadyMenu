using ArcadyMenu.Models;
using System.Net.Http.Json;

namespace ArcadyMenu {
	public interface IDataService {
		/// <summary> Queries the backend for the meal plan for the given date. Throws exception on HTTP request failure. </summary>
		/// <param name="date">The date to get the meal plan for. Any time component is ignored.</param>
		/// <returns>The day's <see cref="MealPlan"/>, if it exists, otherwise <see langword="null"/>.</returns>
		/// <exception cref="HttpRequestException" />
		public Task<MealPlan?> GetMealPlanForDay(DateTime date);

		/// <summary> Queries the backend for the meal plan for an entire week. Throws exception on HTTP request failure. </summary>
		/// <remarks>The week used is the one that contains the given date, and starts on a Monday.</remarks>
		/// <param name="date">A date that is contained in the Monday-starting weekly meal plan.</param>
		/// <returns>
		/// A 7-element array of <see cref="MealPlan"/>'s, one for each day, starting with Monday. <br/>
		/// If a day does not have a meal plan, the corresponding element will be <see langword="null"/>.
		/// </returns>
		/// <exception cref="HttpRequestException" />
		public Task<MealPlan?[]> GetMealPlanForWeek(DateTime date);
	}

	public class DataService : IDataService {
		private readonly HttpClient httpClient;

		public DataService(HttpClient httpClient) {
			this.httpClient = httpClient;
		}

		public async Task<MealPlan?> GetMealPlanForDay(DateTime date) {
			MealPlan mealPlan = new();
			var foods = await httpClient.GetFromJsonAsync<DBFoodItem[]>($"http://localhost:3000/arcadymenu/mealplan/day/{date:yyyy-MM-dd}");
			if (foods!.Length == 0) return null; // No food items means no meal plan

			foreach (DBFoodItem food in foods) {
				if (food.Meal == MealType.Breakfast)
					mealPlan.Breakfast.Add(food.Category, food.FoodDesc);
				else if (food.Meal == MealType.Lunch)
					mealPlan.Lunch.Add(food.Category, food.FoodDesc);
				else
					mealPlan.Dinner.Add(food.Category, food.FoodDesc);
			}

			return mealPlan;
		}

		public async Task<MealPlan?[]> GetMealPlanForWeek(DateTime date) {
			// Get the day that starts the week that this day is in.
			// Database day of weeks start at Monday and end at Sunday, like the arcady menu,
			// but C# days start at Sunday and end at Saturday so we need to account for that.
			int dayDiff = (7 + (int)date.DayOfWeek - 1) % 7;
			DateTime startOfWeek = date.AddDays(dayDiff * -1);

			MealPlan[] mealPlans = new MealPlan[7];
			var foods = await httpClient.GetFromJsonAsync<DBFoodItem[]>($"http://localhost:3000/arcadymenu/mealplan/week/{startOfWeek:yyyy-MM-dd}");

			foreach (DBFoodItem food in foods!) {
				// Meal plan for a day is null by default, if we get an item for a day then the plan is non-null so we need to create one.
				if (mealPlans[food.DOW!.Value] == null)
					mealPlans[food.DOW!.Value] = new();

				// Sort food items into their daily meal plans based on DOW (Day Of Week)
				MealPlan mealPlan = mealPlans[food.DOW!.Value];
				if (food.Meal == MealType.Breakfast)
					mealPlan.Breakfast.Add(food.Category, food.FoodDesc);
				else if (food.Meal == MealType.Lunch)
					mealPlan.Lunch.Add(food.Category, food.FoodDesc);
				else
					mealPlan.Dinner.Add(food.Category, food.FoodDesc);
			}

			return mealPlans;
		}
	}
}
