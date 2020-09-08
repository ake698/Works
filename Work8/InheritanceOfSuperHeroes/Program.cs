using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritanceOfSuperHeroes
{
    /// <summary>
    /// Simple test driver for the subclasses of the SuperHero class
    /// </summary>
    class Program
    {
        // UNCOMMENT THE INSIDE OF EACH METHOD AS YOU FINISH EACH SUB-CLASS AND 
        // ARE READY TO TEST
        //
        // THE TESTS COVERED IN THIS DRIVER MAY NOT COVER EVERYTHING COVERED BY
        // THE AMS. FEEL FREE TO INCLUDE YOUR OWN TESTS!

        static void Main(string[] args)
        {

            TestHumans();

            TestEnhancedHumans();

            TestSuperHumans();

            Console.Write("Press any key to exit ...");
            Console.ReadKey();

        }

        /// <summary>
        /// Runs various tests on the Human class. The text in square brackets 
        /// is the expected result.
        /// </summary>
        static void TestHumans()
        {
            Human Batman = new Human("Bruce Wayne", "Batman");
            Human BlackCanary = new Human("Dinah Drake", "Black Canary");

            Console.WriteLine("H1: In his normal identity Batman has no superpowers " +
                "[Bruce Wayne, 0]:\n\t{0}, {1}\n", 
                Batman.CurrentIdentity(), Batman.TotalPower());

            Batman.SwitchIdentity();

            Console.WriteLine("H2: After switching identity still has no powers " +
                "[Batman, 0]:\n\t{0}, {1}\n", 
                Batman.CurrentIdentity(), Batman.TotalPower());

            Console.WriteLine("H3: Can Batman fly with his cape? [No]:\n\t{0}\n", 
                Batman.HasPower(SuperPower.Flight) ? "Yes" : "No");

            Console.WriteLine("H4: Batman's identity switch does not alter " +
                "Black Canary's ID [Dinah Drake]:\n\t{0}\n", 
                BlackCanary.CurrentIdentity());

            BlackCanary.SwitchIdentity();

            Console.WriteLine("H6: Only Dinah Drake can chage her idenity " +
                "[Black Canary]:\n\t{0}\n", BlackCanary.CurrentIdentity());

            Console.WriteLine("H7: After switching identity Black Canary " +
                "has no powers [Black Canary, 0]:\n\t{0}, {1}\n", 
                BlackCanary.CurrentIdentity(), BlackCanary.TotalPower());
        }

        /// <summary>
        /// Runs various tests on the EnhancedHuman class. The text in square brackets 
        /// is the expected result.
        /// </summary>
        static void TestEnhancedHumans()
        {
            EnhancedHuman CaptainMarvel = new EnhancedHuman("Billy Batson", 
                "Captain Marvel", new List<SuperPower> {  
                    SuperPower.Flight,
                    SuperPower.SuperStrength,
                    SuperPower.Invulnerability
                });

            EnhancedHuman GreenLanteen = new EnhancedHuman("Hal Jordan", 
                "Green Lantern", new List<SuperPower> { 
                    SuperPower.Flight, 
                    SuperPower.SuperStrength,
                    SuperPower.SuperSpeed, 
                    SuperPower.Invulnerability
                });

            Console.WriteLine("EH1: As a mortal Captain Marvel is meek and has " +
                "no super powers [Billy Batson, 0]:\n\t{0}. {1}\n",
                CaptainMarvel.CurrentIdentity(), CaptainMarvel.TotalPower());

            CaptainMarvel.SwitchIdentity();

            Console.WriteLine("EH2: After Billy Batson says the magic word SHAZAM, " +
                "he becomes who? [Captain Marvel, 325]:\n\t{0}, {1}\n",
                CaptainMarvel.CurrentIdentity(), CaptainMarvel.TotalPower());

            Console.WriteLine("EH3: Does Captain Marvel have super strength? " +
                "[Captain Marvel, Yes]:\n\t{0}, {1}\n", 
                CaptainMarvel.CurrentIdentity(), 
                CaptainMarvel.HasPower(SuperPower.SuperStrength) ? "Yes" : "No");

            Console.WriteLine("EH4: Does Captain Marvel have X-ray vision? " +
                "[Captain Marvel, No]:\n\t{0}, {1}\n", 
                CaptainMarvel.CurrentIdentity(),
                CaptainMarvel.HasPower(SuperPower.XRayVision) ? "Yes" : "No");

            Console.WriteLine("EH5: Green Lanteen was born a a normal human. " +
                "[Hal Jordan, 0]:\n\t{0}, {1}\n", 
                GreenLanteen.CurrentIdentity(), GreenLanteen.TotalPower());

            GreenLanteen.SwitchIdentity();

            Console.WriteLine("EH6 But he can use his power ring to be a " +
                "superhero. [GreenLantern, 365]:\n\t{0}, {1}\n", 
                GreenLanteen.CurrentIdentity(), GreenLanteen.TotalPower());

            GreenLanteen.SwitchIdentity();

            Console.WriteLine("EH7: But unless he recharges his ring every " +
                "day he changes back. [Hal Jordan, 0]:\n\t{0}, {1}\n", 
                GreenLanteen.CurrentIdentity(), GreenLanteen.TotalPower());
        }

        /// <summary>
        /// Runs various tests on the SuperHuman class.The text in square brackets 
        /// is the expected result.
        /// </summary>
        static void TestSuperHumans()
        {
            SuperHuman WonderWoman = new SuperHuman("Wonder Woman", 
                "Diana Prince", new List<SuperPower> { 
                    SuperPower.SuperStrength, 
                    SuperPower.SuperIntellect
                });

            SuperHuman Superman = new SuperHuman("Superman", 
                "Clark Kent", new List<SuperPower> {
                    SuperPower.Flight, 
                    SuperPower.SuperStrength, 
                    SuperPower.XRayVision, 
                    SuperPower.SuperSpeed, 
                    SuperPower.Invulnerability
                });

            Console.WriteLine("SH1: When Superman came to Earth he already had " +
                "super powers[Superman, 385]:\n\t{0}, {1}\n", 
                Superman.CurrentIdentity(), Superman.TotalPower());

            Superman.SwitchIdentity();

            Console.WriteLine("SH2: He retains his super powers even in his " +
                "secret identity. [Clark Kent, 385]:\n\t{0}, {1}\n", 
                Superman.CurrentIdentity(), Superman.TotalPower());

            Superman.LoseAllSuperPowers();

            Console.WriteLine("SH3: However when exposed to kryponite he loses " +
                "all of his powers. [Clark Kent, 0]:\n\t{0}, {1}\n", 
                Superman.CurrentIdentity(), Superman.TotalPower());

            Console.WriteLine("SH4: But kryponite has no effect on Wonder Woman " +
                "who has all of her powers. [Wonder  Women, 165]:\n\t{0}, {1}\n", 
                WonderWoman.CurrentIdentity(), WonderWoman.TotalPower());

            WonderWoman.SwitchIdentity();

            Console.WriteLine("SH5: when she switches to her plain identity she " +
                "still has her \"Wisom of Athena\" [Diana Prince, Yes]:\n\t{0}, {1}\n", 
                WonderWoman.CurrentIdentity(),
                WonderWoman.HasPower(SuperPower.SuperIntellect) ? "Yes" : "No");
        }
    }
}
