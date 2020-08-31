//using System;
//using System.IO;

//using static System.Console;

//namespace Simulation
//{
//    public class Die
//    {
//        // Private variables: number of faces, current face, and random number
//        // generator.
//        // INSERT VARIABLES HERE
//        private Random generateor;
//        private int currentFace;
//        private readonly int maxFace;
//        /// <summary>
//        /// Initialise, assigning default values. Number of faces should be
//        /// 3, and current face should be 1. 
//        /// </summary>
//        /// <param name="random">
//        ///     Reference to a System.Random object which will be used to 
//        ///     simulate dice rolls.
//        /// </param>
//        // INSERT CODE HERE 
//        public Die(Random random)
//        {
//            generateor = random;
//            maxFace = 3;
//            currentFace = 1;
//        }
//        /// <summary>
//        /// Initialise, assigning the designated number of faces, and setting 
//        /// current face to 1. 
//        /// </summary>
//        /// <param name="random">
//        ///     Reference to a System.Random object which will be used to 
//        ///     simulate dice rolls.
//        /// </param>
//        /// <param name="faces">
//        ///     An integer which stipulates the number of faces. If the number 
//        ///     of faces requested is less than 3, then the requested number 
//        ///     should be ignored and the default number of faces (3) should be 
//        ///     assigned instead.
//        /// </param>
//        // INSERT CODE HERE
//        public Die(Random random, int faces)
//        {
//            generateor = random;
//            maxFace = faces < 3 ? 3 : faces;
//            currentFace = 1;
//        }


//        /// <summary>
//        /// Get the number of faces.
//        /// </summary>
//        /// <returns>
//        ///     The number of faces.
//        /// </returns>
//        // INSERT CODE HERE
//        public int GetMaxFace()
//        {
//            return maxFace;
//        }
//        /// <summary>
//        /// Roll the die by generating a random number between 1 and the 
//        /// number of faces, inclusive.
//        /// </summary>
//        // INSERT CODE HERE
//        public void RollDie()
//        {
//            currentFace = generateor.Next(1, maxFace + 1);
//        }
//        /// <summary>
//        /// Get the current face.
//        /// </summary>
//        /// <returns>The current face.</returns>
//        // INSERT CODE HERE
//        public int GetCurrentFace()
//        {
//            return currentFace;
//        }
//        /// <summary>
//        ///     Test driver for Simulation.Die.
//        ///     Interactively queries user for deposit amount, withdrawal amount,
//        ///     and interest rate, applies operation and displays results.
//        /// </summary>
//        public static void Main ()
//        {
//            Random generator = new Random(626356);
//            Die die = new Die(generator);
//            bool finished = false;

//            while ( !finished )
//            {
//                Write( menu );
//                var line = ReadLine();

//                if ( line == null ) break;

//                var fields = line.Trim().ToLower().Split( ' ' );

//                if ( fields.Length > 0 && fields[0].Length > 0 )
//                {
//                    int seed;
//                    int numFaces;

//                    switch ( fields[0][0] )
//                    {
//                        case 'd':
//                            switch ( fields.Length )
//                            {
//                                case 1:
//                                    die = new Die(generator);
//                                    break;
//                                case 2:
//                                    numFaces = int.Parse( fields[1] );
//                                    die = new Die( generator, numFaces );
//                                    break;
//                                case 3:
//                                    seed = int.Parse( fields[1] );
//                                    numFaces = int.Parse( fields[2] );
//                                    generator = new Random( seed );
//                                    die = new Die( generator, numFaces );
//                                    break;
//                                default:
//                                    break;
//                            }
//                            break;
//                        case 'r':
//                            die.RollDie();
//                            break;
//                        case 'q':
//                            finished = true;
//                            break;
//                        default:
//                            break;
//                    }
//                }

//                if ( !finished )
//                {
//                    int numFaces = die.GetMaxFace();
//                    int currFace = die.GetCurrentFace();
//                    WriteLine( $"After operation: number of faces = {numFaces}, current face = {currFace}" );
//                }
//            }
//        }

//        static readonly string menu =
//            "Enter option:\n   d = new die;  r = roll;  q = exit.\n==> ";
//    }
//}
