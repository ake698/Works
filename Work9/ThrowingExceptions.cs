using System;
using System.Collections.Generic;
using System.IO;

namespace RecipeApp
{
    class RecipeError : Exception
    {
        public RecipeError () : base() { }
        public RecipeError (string message) : base(message) { }
    }

    class Measure
    {
        /// <summary>
        ///     Create a new Measure.
        /// </summary>
        /// <param name="quantity">
        ///     Physical quantity
        /// </param>
        /// <param name="units">
        ///     Units of measurement, e.g. "pcs", "kg". May be null to indicate counted objects.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     If quantity is not greater than 0:
        ///     *   Exception parameter name will be "quantity".
        ///     *   Exception message will be "Quantity may not be equal to or less than zero."
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     If units is not null and contains only white space symbols:
        ///     *   Exception parameter name will be "units".
        ///     *   Exception message will be "Either a null reference or non-blank string is required.".
        /// </exception>
        public Measure (double quantity, string units)
        {
            // INSERT CODE HERE
            if (quantity <= 0) throw new ArgumentOutOfRangeException("quantity", "Quantity may not be equal to or less than zero.");
            if(units != null && units.Trim().Equals("")) throw new ArgumentException( "Either a null reference or non-blank string is required.","units");
            Quantity = quantity;
            Units = units;
        }

        /// <summary>
        ///     Multiplies the numeric quantity by the designated scale factor.
        /// </summary>
        /// <param name="factor">
        ///     The scale factor.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     If scale factor is not greater than 0:
        ///     *   Exception parameter will be "factor".
        ///     *   Exception message will be "Scale may not be equal to or less than zero."
        /// </exception>
        public void Scale (double factor)
        {
            // INSERT CODE HERE
            if (!(factor > 0)) throw new ArgumentOutOfRangeException("factor", "Scale may not be equal to or less than zero.");
            Quantity *= factor;
        }

        /// <summary>
        ///     Get the numeric quantity.
        /// </summary>
        public double Quantity { get; private set; }

        /// <summary>
        ///     Get the unit of measurement. This is null for counted items.
        /// </summary>
        public string Units { get; private set; }

        /// <summary>
        ///     Get text representing this Measure object.
        /// </summary>
        /// <returns>
        ///     A string which contains text representing this Measure object.
        /// </returns>
        public override string ToString ()
        {
            if ( Units != null )
            {
                return $"{Quantity:F2} {Units}";
            }
            else
            {
                return $"{Quantity:F2}";
            }
        }
    }

    class Recipe
    {
        private int servings;
        private SortedDictionary<string, Measure> ingredients;

        /// <summary>
        ///     Initialise an instance of Recipe, set for 1 Serving.
        /// </summary>
        public Recipe ()
        {
            ingredients = new SortedDictionary<string, Measure>();
            Servings = 1;
        }

        /// <summary>
        ///     Add a new item to the collection of ingredients.
        /// </summary>
        /// <param name="ingredientName">
        ///     A string which specifies the name of the new item.
        /// </param>
        /// <param name="quantity">
        ///     The quantity of the item which is to be added. 
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     If ingredientName is null: 
        ///     *   Exception parameter will be "ingredientName".
        ///     *   Exception message will be "Non-null string is required for ingredient name."
        ///     If quantity is null: 
        ///     *   Exception parameter will be "quantity".
        ///     *   Exception message will be "Non-null quantity is required."
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     If ingredientName is not null, but entirely consists of white space: 
        ///     *   Exception parameter will be "ingredientName".
        ///     *   Exception message will be "Ingredient name must not be empty or white space."
        /// </exception>
        public void Add (string ingredientName, Measure quantity)
        {
            // INSERT CODE HERE
            if (ingredientName is null) throw new ArgumentNullException("ingredientName", "Non-null string is required for ingredient name.");
            if (quantity == null) throw new ArgumentNullException("quantity", "Non-null quantity is required.");
            if (string.IsNullOrWhiteSpace(ingredientName)) throw new ArgumentException("Ingredient name must not be empty or white space.", "ingredientName");
            ingredients.Add(ingredientName, quantity);
        }

        /// <summary>
        ///     Try to remove designated ingredient from the collection.
        /// </summary>
        /// <param name="ingredientName">
        ///     The name of the new ingredient.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     If ingredientName is null:
        ///     *   Exception parameter will be "ingredientName".
        ///     *   Exception message will be "Non-null string is required for ingredient name."
        /// </exception>
        public bool Remove (string ingredientName)
        {
            // INSERT CODE HERE
            if(ingredientName == null) throw new ArgumentNullException("ingredientName", "Non-null string is required for ingredient name.");
            return ingredients.Remove(ingredientName);
        }

        /// <summary>
        ///     Gets the quantity of a designated ingredient from this collection.
        /// </summary>
        /// <param name="ingredientName">
        ///     The name of the ingredient.
        /// </param>
        /// <returns>
        ///     A Measure corresponding to the requested ingredient.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     If ingredientName is null:
        ///     *   Exception parameter will be "ingredientName".
        ///     *   Exception message will be "Non-null string is required for ingredient name."
        /// </exception>
        /// <exception cref="RecipeError">
        ///     If ingredientName is not present in the collection:
        ///     *   Exception message will be "'X' has not been added to the list.", where X
        ///         is the name of the ingredient.
        /// </exception>
        public Measure Get (string ingredientName)
        {
            Measure res = null;
            // INSERT CODE HERE
            if (ingredientName == null) throw new ArgumentNullException("ingredientName", "Non-null string is required for ingredient name.");
            if ( ingredients.ContainsKey(ingredientName) )
            {
                res = ingredients[ingredientName];
            }
            else
            {
                //INSERT CODE HERE
                throw new RecipeError($"'{ingredientName}' has not been added to the list.");
            }

            return res;
        }

        /// <summary>
        ///     Get or set the number of servings.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     If value is not greater than 0:
        ///     *   Exception parameter will not be set.
        ///     *   Exception message will be "Serves may not be equal to or less than zero."
        /// </exception>
        public int Servings {
            get { return servings; }
            set {
                // INSERT CODE HERE
                if (!(value > 0)) throw new ArgumentOutOfRangeException("Servings", "Serves may not be equal to or less than zero.");
                double scaleFactor = (double)value / servings;

                foreach ( Measure measurement in ingredients.Values )
                {
                    measurement.Scale((double)value / servings);
                }

                servings = value;
            }
        }

        /// <summary>
        ///     Gets text representing this Recipe object.
        /// </summary>
        /// <returns>
        ///     
        /// </returns>
        public override string ToString ()
        {
            StringWriter w = new StringWriter();

            foreach ( var item in ingredients )
            {
                w.WriteLine($"\"{item.Key}\",{item.Value}");
            }

            return w.ToString();
        }
    }

    class Program
    {
        static Recipe shoppingList = new Recipe();

        static void Main ()
        {

            Console.WriteLine(
            "Welcome to the recipe book. Valid input is either a comment or a command.\n" +
            "A comment is any line in which a hash symbol appears as the first visible character.\n" +
            "A command is a space-separated list of tokens. Recognised commands are:\n" +
            "    add <ingredient name> <quantity> <unit of measurement>\n" +
            "    get <ingredient name>\n" +
            "    remove <ingredient name>\n" +
            "    serve <new number of servings>\n" +
            "    list\n" +
            "Please don't add ingredients with spaces in their names!\n" +
            "Enter 'null' for a null string; embed '{s}' to generate a space."
            );

            while ( true )
            {
                Console.Write("===> ");
                var line = Console.ReadLine();

                // Check for EOF.
                if ( line == null ) break;

                line = line.Trim();

                // Check for empty line
                if ( line.Length == 0 ) return;

                // Check for comment
                if ( line[0] == (char) 35 ) continue;

                var fields = Split(line);

                if ( fields[0] == "quit" ) break;

                // Process a record for the recipe database
                try
                {
                    if ( Add(fields) ) continue;
                    if ( Get(fields) ) continue;
                    if ( Remove(fields) ) continue;
                    if ( Serve(fields) ) continue;
                    if ( List(fields) ) continue;

                    // If all else fails:
                    Console.WriteLine($"I don't know how to '{string.Join(" ", fields)}'");
                }
                catch ( Exception ex )
                {
                    Console.WriteLine($"Exception caught - {ex.GetType().Name}: {ex.Message}");
                }
            }
        }

        static private string[] Split (string s)
        {
            var fields = s.Split(' ');
            List<string> result = new List<string>();

            foreach ( var field in fields )
            {
                string f = field;

                if ( f == "null" )
                {
                    f = null;
                }
                else
                {
                    f = f.Replace("{s}", " ");
                }
                
                result.Add(f);
            }

            return result.ToArray();
        }

        private static bool List (string[] fields)
        {
            if ( fields[0] != "list" ) return false;

            Console.WriteLine("Ingredients are: ");
            Console.Write(shoppingList.ToString());
            return true;
        }

        private static bool Serve (string[] fields)
        {
            if ( fields[0] != "serve" ) return false;

            int serves;
            if ( fields.Length < 2 || !int.TryParse(fields[1], out serves) ) return false;

            shoppingList.Servings = serves;
            Console.Out.WriteLine("Successfully updated shopping list.");
            return true;
        }

        private static bool Remove (string[] fields)
        {
            if ( fields[0] != "remove" ) return false;

            if ( fields.Length < 2 ) return false;

            string name = fields[1] == null ? null : fields[1];

            bool success = shoppingList.Remove(name);
            string status = success ? "removed from" : "not found in";
            Console.Out.WriteLine($"Item '{name}' {status} shopping list.");
            Console.Write(shoppingList.ToString());
            return true;
        }

        private static bool Get (string[] fields)
        {
            if ( fields[0] != "get" ) return false;

            if ( fields.Length < 2 ) return false;

            string name = fields[1] == null ? null : fields[1];

            var result = shoppingList.Get(name);
            Console.Out.WriteLine($"Item '{name}': {result}.");

            return true;
        }

        private static bool Add (string[] fields)
        {
            if ( fields[0] != "add" ) return false;

            if ( fields.Length < 2 ) return false;

            string name = fields[1] == null ? null : fields[1];

            Measure qty = null;

            if ( fields.Length > 2 && fields[2] != null )
            {
                double amt;

                if ( !double.TryParse(fields[2], out amt) ) return false;

                string units = null;

                if ( fields.Length > 3 && fields[3] != null ) units = fields[3];

                qty = new Measure(amt, units);
            }

            shoppingList.Add(name, qty);
            Console.Out.WriteLine($"Successfully added '{name}': {qty}.");

            return true;
        }
    }
}