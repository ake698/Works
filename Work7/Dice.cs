using System;
using System.IO;

using static System.Console;

namespace Simulation
{
    public class Die
    {
        // INSERT YOUR SOLUTION TO THE PREVIOUS EXERCISE HERE
        private Random generateor;
        private int currentFace;
        private readonly int maxFace;
        /// <summary>
        /// Initialise, assigning default values. Number of faces should be
        /// 3, and current face should be 1. 
        /// </summary>
        /// <param name="random">
        ///     Reference to a System.Random object which will be used to 
        ///     simulate dice rolls.
        /// </param>
        // INSERT CODE HERE 
        public Die(Random random)
        {
            generateor = random;
            maxFace = 3;
            currentFace = 1;
        }
        /// <summary>
        /// Initialise, assigning the designated number of faces, and setting 
        /// current face to 1. 
        /// </summary>
        /// <param name="random">
        ///     Reference to a System.Random object which will be used to 
        ///     simulate dice rolls.
        /// </param>
        /// <param name="faces">
        ///     An integer which stipulates the number of faces. If the number 
        ///     of faces requested is less than 3, then the requested number 
        ///     should be ignored and the default number of faces (3) should be 
        ///     assigned instead.
        /// </param>
        // INSERT CODE HERE
        public Die(Random random, int faces)
        {
            generateor = random;
            maxFace = faces < 3 ? 3 : faces;
            currentFace = 1;
        }


        /// <summary>
        /// Get the number of faces.
        /// </summary>
        /// <returns>
        ///     The number of faces.
        /// </returns>
        // INSERT CODE HERE
        public int GetMaxFace()
        {
            return maxFace;
        }
        /// <summary>
        /// Roll the die by generating a random number between 1 and the 
        /// number of faces, inclusive.
        /// </summary>
        // INSERT CODE HERE
        public void RollDie()
        {
            currentFace = generateor.Next(1, maxFace + 1);
        }
        /// <summary>
        /// Get the current face.
        /// </summary>
        /// <returns>The current face.</returns>
        // INSERT CODE HERE
        public int GetCurrentFace()
        {
            return currentFace;
        }
    }

    public class Dice
    {
        // Private variables: number of faces, current face, and random number
        // generator.
        // INSERT VARIABLES HERE
        private Die[] dice;
        /// <summary>
        /// Initialise, assigning default values. Number of faces should be
        /// 3, and current face should be 1. 
        /// </summary>
        /// <param name="random">
        ///     Reference to a System.Random object which will be used to 
        ///     simulate dice rolls.
        /// </param>
        /// <param name="numDice">
        ///     The number of dice to create and use. This quantity is 
        ///     guaranteed to be equal to or greater than 1. 
        /// </param>
        // INSERT CODE HERE 
        public Dice(Random random, int numDice)
        {
            dice = new Die[numDice];
            for (int i = 0; i < numDice; i++)
            {
                var die = new Die(random);
                dice[i] = die;
            }
        }

        /// <summary>
        /// Initialise, assigning the designated number of faces, and setting 
        /// current face to 1. 
        /// </summary>
        /// <param name="random">
        ///     Reference to a System.Random object which will be used to 
        ///     simulate dice rolls.
        /// </param>
        /// <param name="numDice">
        ///     The number of dice to create and use. This quantity is 
        ///     guaranteed to be equal to or greater than 1. 
        /// </param>
        /// <param name="faces">
        ///     An integer which stipulates the number of faces. If the number 
        ///     of faces requested is less than 3, then the requested number 
        ///     should be ignored and the default number of faces (3) should be 
        ///     assigned instead.
        /// </param>
        // INSERT CODE HERE
        public Dice(Random random, int numDice, int faces)
        {
            dice = new Die[numDice];
            for (int i = 0; i < numDice; i++)
            {
                var die = new Die(random, faces);
                dice[i] = die;
            }
        }

        /// <summary>
        /// Roll all dice in this collection.
        /// </summary>
        // INSERT CODE HERE
        public void RollAllDice()
        {
            foreach (var die in dice)
            {
                die.RollDie();
            }
        }
        /// <summary>
        /// Get the sum of the current face values of the individual dice in the
        /// collection.
        /// </summary>
        /// <returns>The sum of the current face values of the individual dice in the
        /// collection.</returns>
        // INSERT CODE HERE
        public int GetFaceValue()
        {
            int sum = 0;
            foreach (var die in dice)
            {
                sum += die.GetCurrentFace();
            }
            return sum;
        }

        /// <summary>
        ///     Test driver for Simulation.Die.
        ///     Interactively queries user for deposit amount, withdrawal amount,
        ///     and interest rate, applies operation and displays results.
        /// </summary>
        public static void Main()
        {
            Random generator = new Random(927137);
            int numDice = 2;
            int numFaces = 6;
            Dice dice = new Dice(generator, numDice, numFaces);
            bool finished = false;

            while (!finished)
            {
                Write(menu);
                var line = ReadLine();

                if (line == null) break;

                var fields = line.Trim().ToLower().Split(' ');

                if (fields.Length > 0 && fields[0].Length > 0)
                {
                    switch (fields[0][0])
                    {
                        case 'd':
                            switch (fields.Length)
                            {
                                case 1:
                                    numDice = 2;
                                    dice = new Dice(generator, numDice);
                                    break;
                                case 2:
                                    numDice = int.Parse(fields[1]);
                                    dice = new Dice(generator, numDice);
                                    break;
                                case 3:
                                    numDice = int.Parse(fields[1]);
                                    numFaces = int.Parse(fields[2]);
                                    dice = new Dice(generator, numDice, numFaces);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 'r':
                            dice.RollAllDice();
                            break;
                        case 'q':
                            finished = true;
                            break;
                        default:
                            break;
                    }
                }

                if (!finished)
                {
                    int currTotal = dice.GetFaceValue();
                    WriteLine($"After operation: current total = {currTotal}");
                }
            }
        }

        static readonly string menu =
            "Enter option:\n   d = new bunch of dice;  r = roll;  q = exit.\n==> ";
    }
}
