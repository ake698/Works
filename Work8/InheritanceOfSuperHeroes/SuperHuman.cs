using System;
using System.Collections.Generic;
using System.Text;

namespace InheritanceOfSuperHeroes
{
    /// <summary>
    /// A class representing a "super human" SuperHero. Super humans have
    /// super powers, and can gain/lose super powers under certain conditions.
    /// </summary>
    class SuperHuman : SuperHero
    {

        // INSERT PRIVATE FIELDS
        private List<SuperPower> superPowers;
        /// <summary>
        /// Constructs a new SuperHuman instance with specified identities 
        /// and SuperPowers (uses the base constructor for SuperHero).
        /// </summary>
        /// <param name="trueIdentity">The true identity of the SuperHuman</param>
        /// <param name="alterEgo">The alter ego of the SuperHuman</param>
        /// <param name="myPowers">A list of SuperPowers the SuperHuman possesses</param>
        /// 
        // INSERT CONSTRUCTOR
        public SuperHuman(string trueIdentity, string alterEgo, List<SuperPower> myPowers) : base(trueIdentity, alterEgo)
        {
            superPowers = myPowers;
        }
        /// <summary>
        /// Determines whether the SuperHuman has a particular SuperPower.
        /// </summary>
        /// <param name="whatPower">The SuperPower to be queried</param>
        /// <returns>True is the SuperHuman has the provided SuperPower,
        ///     false otherwise</returns>
        ///     
        // INSERT OVERRIDE FOR HasPower METHOD
        public override bool HasPower(SuperPower whatPower)
        {
            bool result = false;
            superPowers.ForEach(x => { if (x == whatPower) result = true; });
            return result;
        }
        /// <summary>
        /// Returns the total power of the SuperHuman based on thier SuperPowers.
        /// </summary>
        /// <returns>The total power of the SuperHuman</returns>
        /// 
        // INSERT OVERRIDE FOR TotalPower METHOD
        public override int TotalPower()
        {
            int total = 0;
            superPowers.ForEach(x => total += (int)x);
            return total;
        }
        /// <summary>
        /// Adds a new SuperPower to the set of SuperPowers the SuperHuman 
        /// possesses, and adjusts their total power accordingly.
        /// </summary>
        /// <param name="newPower">The new SuperPower</param>
        /// 
        // INSERT AddSuperPower METHOD
        public void AddSuperPower(SuperPower superPower)
        {
            if (!superPowers.Contains(superPower))
                superPowers.Add(superPower);
        }


        /// <summary>
        /// Removes a particular SuperPower from the set of SuperPowers the 
        /// SuperHuman possesses (if it exists), and adjusts their total 
        /// power accordingly.
        /// </summary>
        /// <param name="power">The SuperPower to be removed</param>
        /// 
        // INSERT LoseSinglePower METHOD
        public void LoseSinglePower(SuperPower LoseSinglePower)
        {
            superPowers.Remove(LoseSinglePower);
        }
        /// <summary>
        /// Removes all SuperPowers that the SuperHuman possesses, and adjusts 
        /// their total power accordingly.
        /// </summary>
        /// 
        // INSERT LoseAllSuperPowers METHOD
        public void LoseAllSuperPowers()
        {
            superPowers.Clear();
        }


    }
}
