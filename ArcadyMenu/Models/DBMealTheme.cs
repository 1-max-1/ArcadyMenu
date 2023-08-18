namespace ArcadyMenu.Models {
	/// <summary> Represents a record from the <c>meal_theme</c> table in the database. Maps a specific meal on a day to a theme. </summary>
	public class DBMealTheme {
		/// <summary> Breakfast, Lunch or Dinner </summary>
		public MealType Meal { get; set; }
		/// <summary> The theme of the meal.  </summary>
		public string MealTheme { get; set; } = "";
		/// <summary> The Day Of Week of this record, goes from 0 to 6, starting with Monday. </summary>
		public int DOW { get; set; }
	}
}