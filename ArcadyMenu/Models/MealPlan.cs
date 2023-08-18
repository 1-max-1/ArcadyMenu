namespace ArcadyMenu.Models {
	/// <summary> Class representing a daily meal plan, with meals sorted by breakfast, lunch and dinner. </summary>
	public class MealPlan {
		/// <summary> The data for the "Breakfast" meal. </summary>
		public Meal Breakfast { get; private set; }
		/// <summary> The data for the "Lunch" meal. </summary>
		public Meal Lunch { get; private set; }
		/// <summary> The data for the "Dinner" meal. </summary>
		public Meal Dinner { get; private set; }

		/// <summary>
		/// Creates a new <see cref="MealPlan"/> instance. The food item dictionaries for the <c>Breakfast</c>, <c>Lunch</c> and <c>Dinner</c> meals are initialized to empty.
		/// </summary>
		public MealPlan() {
			Breakfast = new();
			Lunch = new();
			Dinner = new();
		}
	}
}