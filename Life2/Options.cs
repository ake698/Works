using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Life
{
    class Options
    {
        private const int MIN_DIMENSION = 4;
        private const int MAX_DIMENSION = 48;
        private const int MIN_GENERATION = 4;
        private const double MIN_UPDATE = 1.0;
        private const double MAX_UPDATE = 30.0;
        private const double MIN_RANDOM = 0.0;
        private const double MAX_RANDOM = 1.0;
        private const int MIN_NEIGHBOURORDER = 1;
        private const int MAX_NEIGHBOURORDER = 10;
        private const int MIN_SURVIVALBIRTH = 0;
        private const int MIN_MEMORY = 4;
        private const int MAX_MEMORY = 512;
        

        private int rows = 16;
        private int columns = 16;
        private int generations = 50;
        private double updateRate = 5.0;
        private double randomFactor = 0.5;
        private string inputFile = null;
        private int order = 1;
        private int[] survival = new int[] { 2, 3 };
        private int[] birth = new int[] { 3 };
        private int memory = 16;
        private string outputFile = null;

        public int Rows
        {
            get => rows;
            set
            {
                if (value < MIN_DIMENSION || value > MAX_DIMENSION)
                {
                    throw new ArgumentException($"Row dimension \'{value}\' is outside of the acceptable " +
                        $"range of values ({MIN_DIMENSION} - {MAX_DIMENSION})");
                }
                rows = value;
            }
        }

        public int Columns
        {
            get => columns;
            set
            {
                if (value < MIN_DIMENSION || value > MAX_DIMENSION)
                {
                    throw new ArgumentException($"Column dimension \'{value}\' is outside of the acceptable " +
                        $"range of values ({MIN_DIMENSION} - {MAX_DIMENSION})");
                }
                columns = value;
            }
        }

        public int Generations
        {
            get => generations;
            set
            {
                if (value < MIN_GENERATION)
                {
                    throw new ArgumentException($"Generation count \'{value}\' is outside of the acceptable " +
                        $"range of values ({MIN_GENERATION} and above)");
                }
                generations = value;
            }
        }

        public double UpdateRate
        {
            get => updateRate;
            set
            {
                if (value < MIN_UPDATE || value > MAX_UPDATE)
                {
                    throw new ArgumentException($"Update rate \'{value:F2}\' is outside of the acceptable " +
                        $"range of values ({MIN_UPDATE} - {MAX_UPDATE})");
                }
                updateRate = value;
            }
        }

        public double RandomFactor
        {
            get => randomFactor;
            set
            {
                if (value < MIN_RANDOM || value > MAX_RANDOM)
                {
                    throw new ArgumentException($"Random factor \'{value:F2}\' is outside of the acceptable " +
                        $"range of values ({MIN_RANDOM} - {MAX_RANDOM})");
                }
                randomFactor = value;
            }
        }

        public string InputFile
        {
            get => inputFile;
            set
            {
                if (!Path.GetExtension(value).Equals(".seed"))
                {
                    throw new ArgumentException($"Incompatible file extension \'{Path.GetExtension(value)}\'");
                }
                if (!File.Exists(value))
                {
                    throw new ArgumentException($"File \'{value}\' does not exist.");
                }
                inputFile = value;
            }
        }

        public bool Periodic { get; set; } = false;

        public bool StepMode { get; set; } = false;
        public bool Ghost { get; set; } = false;
        public Neighbourhood Neighbourhood { get; set; } = Neighbourhood.MOORE;
        public int Order 
        { get => order; 
          set 
            { 
                if(value < MIN_NEIGHBOURORDER || value > MAX_NEIGHBOURORDER)
                {
                    throw new ArgumentException($"Neighbourhood order {value} is outside of the acceptable " +
                        $"range of values ({MIN_NEIGHBOURORDER} - {MAX_NEIGHBOURORDER})");
                }
                order = value;
            } 
        }
        public bool Center { get; set; } = false;
        public int[] Survival 
        {
            get => survival;
            set
            {
                foreach (var v in value)
                {
                    if(v < MIN_SURVIVALBIRTH)
                    {
                        throw new ArgumentException($"Survival {v} is must greate than 0. ");
                    }
                }
                survival = value;
            }
        }
        public string SurvivalArg { get; set; } = "2...3 ";
        public int[] Birth 
        {
            get => birth;
            set
            {
                foreach (var v in value)
                {
                    if (v < MIN_SURVIVALBIRTH)
                    {
                        throw new ArgumentException($"Birth {v} is must greate than 0. ");
                    }
                }
                birth = value;
            }
        }
        public string BirthArg { get; set; } = "3 ";
        public int Memory
        {
            get => memory;
            set
            {
                if(value < MIN_MEMORY || value > MAX_MEMORY)
                {
                    throw new ArgumentException($"Memory {value} is outside of the acceptable " +
                        $"range of values ({MIN_MEMORY} - {MAX_MEMORY})");
                }
                memory = value;
            }
        }
        public string OutputFile
        {
            get => outputFile;
            set
            {
                // 修改
                if (!Path.GetExtension(value).Equals(".seed"))
                {
                    throw new ArgumentException($"Incompatible file extension \'{Path.GetExtension(value)}\'");
                }
                outputFile = value;
            }
        }

        public override string ToString()
        {
            const int padding = 30;

            string output = "\n";
            output += "Input File: ".PadLeft(padding) + (InputFile != null ? InputFile : "N/A") + "\n";
            output += "Output File: ".PadLeft(padding) + (OutputFile != null ? OutputFile : "N/A") + "\n";
            output += "Generations: ".PadLeft(padding) + $"{Generations}\n";
            output += "Memory: ".PadLeft(padding) + $"{Memory}\n";
            output += "Update Rate: ".PadLeft(padding) + $"{UpdateRate} updates/s\n";
            output += "Roules: ".PadLeft(padding) + $"S( {SurvivalArg}) B ( {BirthArg}) \n";
            output += "Neighbourhood: ".PadLeft(padding) + $"{Neighbourhood} ( {Order} ) \n";
            output += "Periodic: ".PadLeft(padding) + (Periodic ? "Yes" : "No") + "\n";
            output += "Rows: ".PadLeft(padding) + Rows + "\n";
            output += "Columns: ".PadLeft(padding) + Columns + "\n";
            output += "Random Factor: ".PadLeft(padding) + $"{100 * RandomFactor:F2}%\n";
            output += "Step Mode: ".PadLeft(padding) + (StepMode ? "Yes" : "No") + "\n";
            output += "Ghost Mode: ".PadLeft(padding) + (Ghost ? "Yes" : "No") + "\n";
            return output;
        }
    }
}
