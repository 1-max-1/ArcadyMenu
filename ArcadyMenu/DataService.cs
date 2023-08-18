using ArcadyMenu.Models;
using System.Net.Http.Json;

namespace ArcadyMenu {
	/// <summary> Service for grabbing data from the backend. </summary>
	public interface IDataService {
		/// <summary> Queries the backend for the meal plan for the given date. Throws exception on HTTP request failure. </summary>
		/// <param name="date">The date to get the meal plan for. Any time component is ignored.</param>
		/// <returns>The day's <see cref="MealPlan"/>. The meal plan could have no records.</returns>
		/// <exception cref="HttpRequestException" />
		//public Task<MealPlan> GetMealPlanForDay(DateTime date);

		/// <summary> Queries the backend for the meal plan for an entire week. Throws exception on HTTP request failure. </summary>
		/// <remarks>The week used is the one that contains the given date, and starts on a Monday.</remarks>
		/// <param name="date">A date that is contained in the Monday-starting weekly meal plan.</param>
		/// <returns> A 7-element array of <see cref="MealPlan"/>'s, one for each day, starting with Monday. </returns>
		/// <exception cref="HttpRequestException" />
		public Task<MealPlan[]> GetMealPlanForWeek(DateTime date);

		/// <summary> Gets the day that starts the week that the specified day is in. </summary>
		public DateTime GetStartOfWeek(DateTime date);
	}

	public class DataService : IDataService {
		private readonly HttpClient httpClient;

		public DataService(HttpClient httpClient) {
			this.httpClient = httpClient;
		}

		// Adds the given food item db record to the correct meal in the meal plan
		private static void AddFoodItemToMealPlean(MealPlan mealPlan, DBFoodItem food) {
			if (food.Meal == MealType.Breakfast)
				mealPlan.Breakfast.FoodItems.Add(food.Category, food.FoodDesc);
			else if (food.Meal == MealType.Lunch)
				mealPlan.Lunch.FoodItems.Add(food.Category, food.FoodDesc);
			else
				mealPlan.Dinner.FoodItems.Add(food.Category, food.FoodDesc);
		}

		// Assigns the given meal theme to the correct meal in the given meal plan
		private static void AddThemeToMealPlan(MealPlan mealPlan, DBMealTheme theme) {
			if (theme.Meal == MealType.Breakfast)
				mealPlan.Breakfast.Theme = theme.MealTheme;
			else if (theme.Meal == MealType.Lunch)
				mealPlan.Lunch.Theme = theme.MealTheme;
			else
				mealPlan.Dinner.Theme = theme.MealTheme;
		}

		/*public async Task<MealPlan> GetMealPlanForDay(DateTime date) {
			MealPlan mealPlan = new();
			var foods = await httpClient.GetFromJsonAsync<DBFoodItem[]>($"http://localhost:3000/arcadymenu/mealplan/day/{date:yyyy-MM-dd}");

			foreach (DBFoodItem food in foods!) {
				AddFoodItemToMealPlean(mealPlan, food);
			}
			// TODO (if this function is ever used again): implement the single day meal themes
			return mealPlan;
		}*/

		public async Task<MealPlan[]> GetMealPlanForWeek(DateTime date) {
			MealPlan[] mealPlans = { new(),new(),new(),new(),new(),new(),new() }; // 1 per day of week
			var foods = await httpClient.GetFromJsonAsync<DBFoodItem[]>($"http://localhost:3000/arcadymenu/mealplan/week/{GetStartOfWeek(date):yyyy-MM-dd}");
			var themes = await httpClient.GetFromJsonAsync<DBMealTheme[]>($"http://localhost:3000/arcadymenu/mealthemes/week/{GetStartOfWeek(date):yyyy-MM-dd}");

			foreach (DBFoodItem food in foods!) {
				// Sort food items into their daily meal plans based on DOW (Day Of Week)
				MealPlan mealPlan = mealPlans[food.DOW!.Value];
				AddFoodItemToMealPlean(mealPlan, food);
			}

			foreach (DBMealTheme mealTheme in themes!) {
				MealPlan mealPlan = mealPlans[mealTheme.DOW];
				AddThemeToMealPlan(mealPlan, mealTheme);
			}

			return mealPlans;
		}

		public DateTime GetStartOfWeek(DateTime date) {
			// Database day of weeks start at Monday and end at Sunday, like the arcady menu,
			// but C# days start at Sunday and end at Saturday so we need to account for that.
			int dayDiff = (7 + (int)date.DayOfWeek - 1) % 7;
			return date.AddDays(dayDiff * -1);
		}
	}
}
