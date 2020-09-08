using System;
using System.Collections.Generic;
using System.Text;

namespace InheritanceOfSuperHeroes
{
    /// <summary>
    /// A class representing a "human" super hero. Humans do not have any 
    /// super powers, and instead rely on their intellect and technologies
    /// </summary>
    class Human : SuperHero
    {


        /// <summary>
        /// Constructs a new Human instance with specified identities 
        /// (uses the base constructor for SuperHero).
        /// </summary>
        /// <param name="name">The real name of the Human</param>
        /// <param name="secretId">The secret identity of the Human</param>
        /// 
        // INSERT CONSTRUCTOR
        public Human(string trueIdentity, string alterEgo) : base(trueIdentity, alterEgo)
        {
        }
        /// <summary>
        /// Determines whether the Human has a particular SuperPower. Humans 
        /// have no powers.
        /// </summary>
        /// <param name="whatPower">The SuperPower to be queried</param>
        /// <returns>True is the Human has the provided SuperPower, false otherwise</returns>
        /// 
        // INSERT OVERRIDE FOR HasPower METHOD
        public override bool HasPower(SuperPower whatPower)
        {
            return false;
        }
        /// <summary>
        /// Calculates and returns the total power of the Human based on 
        /// thier SuperPowers. Humans have no powers.
        /// </summary>
        /// <returns>The total power of the Human</returns>
        /// 
        // INSERT OVERRIDE FOR TotalPower METHOD

        public override int TotalPower()
        {
            return 0;
        }
    }
}
