namespace ArcadyMenu.Models {
	/// <summary> Class representing a daily meal plan, with food items sorted by breakfast, lunch and dinner. </summary>
	public class MealPlan {
		/// <summary>
		/// The food items available for breakfast. <br/>
		/// Keys are food categories like "Main Option" or "Vegetarian Option". <br/>
		/// Values are food descriptions like "Meatlovers pizza" or "Pasta bake".
		/// </summary>
		/// <remarks>This dictionary could have zero records.</remarks>
		public Dictionary<string, string> Breakfast { get; private set; }

		/// <summary>
		/// The food items available for lunch. <br/>
		/// Keys are food categories like "Main Option" or "Vegetarian Option". <br/>
		/// Values are food descriptions like "Meatlovers pizza" or "Pasta bake".
		/// </summary>
		/// <remarks>This dictionary could have zero records.</remarks>
		public Dictionary<string, string> Lunch { get; private set; }

		/// <summary>
		/// The food items available for dinner. <br/>
		/// Keys are food categories like "Main Option" or "Vegetarian Option". <br/>
		/// Values are food descriptions like "Meatlovers pizza" or "Pasta bake".
		/// </summary>
		/// <remarks>This dictionary could have zero records.</remarks>
		public Dictionary<string, string> Dinner { get; private set; }

		/// <summary>
		/// Creates a new <see cref="MealPlan"/> instance. The <c>Breakfast</c>, <c>Lunch</c> and <c>Dinner</c> dictionaries are initialized to empty.
		/// </summary>
		public MealPlan() {
			Breakfast = new();
			Lunch = new();
			Dinner = new();
		}
	}
}