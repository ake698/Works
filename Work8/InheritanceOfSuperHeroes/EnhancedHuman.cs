using System.Collections.Generic;

namespace InheritanceOfSuperHeroes
{
    /// <summary>
    /// A class representing a "enhanced human" SuperHero. Enhanced humans have
    /// super powers, but only when they are in their "enhanced" state.
    /// </summary>
    class EnhancedHuman : SuperHero
    {


        // INSERT PRIVATE FIELDS
        private List<SuperPower> superPowers;
        private bool isEnhanced = false;
        /// <summary>
        /// Constructs a new EnhancedHero instance with specified identities 
        /// and SuperPowers (uses the base constructor for SuperHero). EnhancedHumans
        /// are not enhanced upon construction.
        /// </summary>
        /// <param name="trueIdentity">The true identity of the EnhancedHuman</param>
        /// <param name="alterEgo">The alter ego of the EnhancedHuman</param>
        /// <param name="myPowers">A list of SuperPowers the EnhancedHuman possesses</param>
        /// 
        // INSERT CONSTRUCTOR
        public EnhancedHuman(string trueIdentity, string alterEgo, List<SuperPower> myPowers) : base(trueIdentity, alterEgo)
        {
            superPowers = myPowers;
        }
        /// <summary>
        /// Switches the current identity with other identity of the 
        /// EnhancedHuman. When their identity is switched, their enhanced state 
        /// is also toggled.
        /// </summary>
        /// 
        // INSERT OVERRIDE FOR SwitchIdentity METHOD
        public override void SwitchIdentity()
        {
            isEnhanced = !isEnhanced;
            base.SwitchIdentity();
        }
        /// <summary>
        /// Determines whether the EnhancedHuman has a particular SuperPower. 
        /// EnhancedHumans only have SuperPowers in their enhanced state.
        /// </summary>
        /// <param name="whatPower">The SuperPower to be queried</param>
        /// <returns>True is the EnhancedHuman has the provided SuperPower,
        ///     false otherwise</returns>
        ///     
        // INSERT OVERRIDE FOR HasPower METHOD
        public override bool HasPower(SuperPower whatPower)
        {
            if (!isEnhanced) return false;
            bool result = false;
            superPowers.ForEach(x => { if (x == whatPower) result = true; });
            return result;
        }


        /// <summary>
        /// Returns the total power of the EnhancedHuman based 
        /// on thier SuperPowers. EnhancedHumans only have SuperPowers in 
        /// their enhanced state.
        /// </summary>
        /// <returns>The total power of the EnhancedHuman</returns>
        /// 
        // INSERT OVERRIDE FOR TotalPower METHOD
        public override int TotalPower()
        {
            int total = 0;
            if (isEnhanced)
            {
                superPowers.ForEach(x => total +=(int)x);
            }
            return total;
        }


    }
}
