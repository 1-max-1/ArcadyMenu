namespace ArcadyMenu.Models {
	/// <summary> A Meal consists of a dictionary of food items, and possibly a meal theme such as "Pizza Friday". </summary>
	public class Meal {
		/// <summary>
		/// The food items available for this meal. <br/>
		/// Keys are food categories like "Main Option" or "Vegetarian Option". <br/>
		/// Values are food descriptions like "Meatlovers pizza" or "Pasta bake".
		/// </summary>
		/// <remarks>This dictionary could have zero records.</remarks>
		public Dictionary<string, string> FoodItems { get; private set; }

		/// <summary> Occasionally, some meals have themes such as "Pizza Friday". Could be null. </summary>
		public string? Theme { get; set; }

		public Meal() {
			FoodItems = new();
		}
	}
}