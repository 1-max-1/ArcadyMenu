using System.Text.Json.Serialization;

namespace ArcadyMenu.Models {
	/// <summary> Breakfast, Lunch or Dinner. </summary>
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum MealType {
		Breakfast,
		Lunch,
		Dinner
	}

	/// <summary> Represents one record obtained from the backend food item database. </summary>
	public class DBFoodItem {
		public MealType Meal { get; set; }
		public string Category { get; set; } = "";
		public string FoodDesc { get; set; } = "";

		/// <summary>
		/// The Day Of Week of this record, goes from 0 to 6, starting with Monday. <br/>
		/// Only used for the weekly meal plans, will be null when the daily meal plan endpoint is used.
		/// </summary>
		public int? DOW { get; set; }
	}
}
