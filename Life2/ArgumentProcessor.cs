using System;
using System.Collections.Generic;
using System.Linq;

namespace Life
{
    static class ArgumentProcessor
    {

        public static Options Process(string[] args)
        {
            Options options = new Options();
            try
            {
                for (int i = 0; i < args.Length; i++)
                {
                    switch (args[i])
                    {
                        case "--dimensions":
                            ProcessDimensions(args, i, options);
                            break;
                        case "--generations":
                            ProcessGenerations(args, i, options);
                            break;
                        case "--max-update":
                            ProcessUpdateRate(args, i, options);
                            break;
                        case "--random":
                            ProcessRandomFactor(args, i, options);
                            break;
                        case "--seed":
                            ProcessInputFile(args, i, options);
                            break;
                        case "--periodic":
                            options.Periodic = true;
                            break;
                        case "--step":
                            options.StepMode = true;
                            break;
                        case "--neighbour":
                            ProcessNeighbour(args, i, options);
                            break;
                        case "--survival":
                            ProcessSurvival(args, i, options);
                            break;
                        case "--birth":
                            ProcessBirth(args, i, options);
                            break;
                        case "--memory":
                            ProcessMemory(args, i, options);
                            break;
                        case "--output":
                            ProcessOutput(args, i, options);
                            break;
                        case "--ghost":
                            options.Ghost = true;
                            break;
                    }
                }
                Logging.Success("Command line arguments processed without issue!");
            }
            catch (Exception exception)
            {
                Logging.Warning(exception.Message);
                Logging.Message("Reverting to defaults for unprocessed arguments...");
            }
            finally
            {
                Logging.Message("The following options will be used:");
                Console.WriteLine(options);
            }

            return options;
        }

        private static void ProcessDimensions(string[] args, int i, Options options)
        {
            ValidateParameterCount(args, i, "dimensions", 2);

            if (!int.TryParse(args[i + 1], out int rows))
            {
                throw new ArgumentException($"Row dimension \'{args[i + 1]}\' is not a valid integer.");
            }

            if (!int.TryParse(args[i + 2], out int columns))
            {
                throw new ArgumentException($"Column dimension \'{args[i + 2]}\' is not a valid integer.");
            }

            options.Rows = rows;
            options.Columns = columns;
        }

        private static void ProcessGenerations(string[] args, int i, Options options)
        {
            ValidateParameterCount(args, i, "generations", 1);

            if (!int.TryParse(args[i + 1], out int generations))
            {
                throw new ArgumentException($"Generation count \'{args[i + 1]}\' is not a valid integer.");
            }

            options.Generations = generations;
        }

        private static void ProcessUpdateRate(string[] args, int i, Options options)
        {
            ValidateParameterCount(args, i, "max-update", 1);

            if (!double.TryParse(args[i + 1], out double updateRate))
            {
                throw new ArgumentException($"Update rate \'{args[i + 1]}\' is not a valid double.");
            }

            options.UpdateRate = updateRate;
        }

        private static void ProcessRandomFactor(string[] args, int i, Options options)
        {
            ValidateParameterCount(args, i, "random", 1);

            if (!double.TryParse(args[i + 1], out double randomFactor))
            {
                throw new ArgumentException($"Random factor \'{args[i + 1]}\' is not a valid double.");
            }

            options.RandomFactor = randomFactor;
        }

        private static void ProcessInputFile(string[] args, int i, Options options)
        {
            ValidateParameterCount(args, i, "seed", 1);

            options.InputFile = args[i + 1];
        }

        private static void ProcessNeighbour(string[] args, int i, Options options)
        {
            ValidateParameterCount(args, i, "neighbour", 3);
            if (!Enum.TryParse(args[i + 1].ToUpper(), out Neighbourhood neighbourhood))
            {
                throw new ArgumentException($"Neighbour type \'{args[i + 1]}\' is not one of two string," +
                    $" either 'moore' or 'vonNeumann'.");
            }
            if (!int.TryParse(args[i + 2], out int order))
            {
                throw new ArgumentException($"Neighbour order \'{args[i + 2]}\' is not a valid integer.");
            }
            if(!bool.TryParse(args[i + 3], out bool center))
            {
                throw new ArgumentException($"Neighbour center \'{args[i + 3]}\' is not a valid bool.");
            }
            options.Neighbourhood = neighbourhood;
            options.Order = order;
            options.Center = center;
        }

        private static void ProcessSurvival(string[] args, int i, Options options)
        {
            var paramters = GetSurvivalBirthParameters(args, i, "Survival", out string original);
            options.Survival = paramters;
            options.SurvivalArg = original;
        }

        private static void ProcessBirth(string[] args, int i, Options options)
        {
            var paramters = GetSurvivalBirthParameters(args, i, "Birth", out string original);
            options.Birth = paramters;
            options.BirthArg = original;
        }

        private static int[] GetSurvivalBirthParameters(string[] args, int i, string options, out string original)
        {
            ValidateParameterCount(args, i, options, new int[]{1,2,3});
            var paramters = new List<int>();
            var count = CaculateParameterCount(args, i);
            original = "";
            for (int p = 1; p <= count; p++)
            {
                original += args[p + i] + " ";
                var tempArgs = args[p + i].Split("...");
                if (!int.TryParse(tempArgs[0], out int start))
                {
                    throw new ArgumentException($"{options} \'{tempArgs[0]}\' is not a valid integer.");
                }
                if (tempArgs.Length > 2)
                {
                    throw new ArgumentException($"{options} \'{args[p + i]}\' formate error.");
                }
                else if(tempArgs.Length == 1)
                {
                    ValidListExits(paramters, start, options);
                    paramters.Add(start);
                    continue;
                }

                if (!int.TryParse(tempArgs[1], out int end))
                {
                    throw new ArgumentException($"{options} \'{tempArgs[1]}\' is not a valid integer.");
                }


                for (int o = Math.Min(start, end); o <= Math.Max(start, end); o++)
                {
                    ValidListExits(paramters, o, options);
                    paramters.Add(o);
                }
            }
            return paramters.ToArray();
        }

        private static void ProcessMemory(string[] args, int i, Options options)
        {
            ValidateParameterCount(args, i, "memory", 1);
            if (!int.TryParse(args[i + 1], out int memory))
            {
                throw new ArgumentException($"Memory order \'{args[i + 1]}\' is not a valid integer.");
            }
            options.Memory = memory;
        }

        private static void ProcessOutput(string[] args, int i, Options options)
        {
            ValidateParameterCount(args, i, "output", 1);
            options.OutputFile = args[i + 1];
        }

        #region Validate parameter
        private static void ValidateParameterCount(string[] args, int i, string option, int numParameters)
        {
            var parameterCount = CaculateParameterCount(args, i);
            if(parameterCount != numParameters)
            {
                throw new ArgumentException($"Insufficient parameters for \'--{option}\' option " +
                    $"(provided {parameterCount}, expected {numParameters})");
            }
        }

        private static void ValidateParameterCount(string[] args, int i, string option, int[] numParameters)
        {
            var parameterCount = CaculateParameterCount(args, i);
            if (!numParameters.Contains(parameterCount))
            {
                throw new ArgumentException($"Insufficient parameters for \'--{option}\' option " +
                    $"(provided {parameterCount}, expected {string.Join(',', numParameters)})");
            }
        }

        private static void ValidListExits(List<int> paramters, int paramter, string options)
        {
            if (paramters.Contains(paramter))
            {
                throw new ArgumentException($"{options} \'{paramter}\' already exists.");
            }
        }
        #endregion
        private static int CaculateParameterCount(string[] args, int i)
        {
            int nextIndex = i;
            while (args.Length > nextIndex + 1 && !args[nextIndex + 1].Contains("--"))
            {
                nextIndex++;
            }
            return nextIndex - i;
        }
    }
}
