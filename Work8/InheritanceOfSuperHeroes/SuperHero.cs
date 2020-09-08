using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritanceOfSuperHeroes
{
    /// <summary>
    /// Enumeration that lists various super powers commonly available to comic
    /// book superheroes and associates a (subjective) value with each superpower.
    /// This list of powers is by no means comprehensive.
    /// </summary>
    public enum SuperPower
    {
        Flight = 100,
        SuperStrength = 75,
        XRayVision = 20,
        SuperSpeed = 40,
        Invulnerability = 150,
        SuperIntellect = 90
    }

    /// <summary>
    /// An abstract class representing a super hero. Super heros have two 
    /// identities and may have various super powers or none at all.
    /// </summary>
    public abstract class SuperHero
    {
        private string currentIdentity;
        private string otherIdentity;

        /// <summary>
        /// Constructs a new SuperHero instance with specified identities
        /// </summary>
        /// <param name="trueIdentity">The true identity of the super hero</param>
        /// <param name="alterEgo">The alter ego of the super hero</param>
        public SuperHero(string trueIdentity, string alterEgo)
        {
            currentIdentity = trueIdentity;
            otherIdentity = alterEgo;
        }

        /// <summary>
        /// Gets the current identity of the super hero
        /// </summary>
        /// <returns>The current identity of the super hero</returns>
        public string CurrentIdentity()
        {
            return currentIdentity;
        }

        /// <summary>
        /// Gets the integer value associated with a particular SuperPower
        /// </summary>
        /// <param name="power">The SuperPower</param>
        /// <returns>The associated integer value</returns>
        public static int GetPowerValue(SuperPower power)
        {
            return (int)power;
        }

        /// <summary>
        /// Switches the current identity with other identity of the hero
        /// </summary>
        public virtual void SwitchIdentity()
        {
            // REPLACE WITH PROPER IMPLEMENTATION
            string temp = currentIdentity;
            currentIdentity = otherIdentity;
            otherIdentity = temp;
            
        }

        /// <summary>
        /// Determines whether the SuperHero has a particular SuperPower
        /// </summary>
        /// <param name="whatPower">The SuperPower to be queried</param>
        /// <returns>True is the SuperHero has the provided SuperPower, false otherwise</returns>
        public abstract bool HasPower(SuperPower whatPower);

        /// <summary>
        /// Returns the total power of the SuperHero based on thier SuperPowers
        /// </summary>
        /// <returns>The total power of the SuperHero</returns>
        public abstract int TotalPower();
    }
}
