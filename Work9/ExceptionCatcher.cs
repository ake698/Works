using System;

namespace BirthdayApp
{
    enum Gender
    {
        Male,
        Female,
        Other
    }

    class UserRecord
    {
        public string FullName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public Gender Gender { get; private set; }
        public int PhoneNumber { get; private set; }

        public UserRecord(string fullName, DateTime dateOfBirth, Gender gender, int phoneNumber)
        {
            FullName = fullName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            PhoneNumber = phoneNumber;
        }

        public override string ToString()
        {
            return $"{FullName}; {DateOfBirth}; {Gender}; {PhoneNumber}";
        }

        public static UserRecord ScanProfile()
        {
            string fullName = Console.ReadLine();
            DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());
            Gender gender = (Gender)Enum.Parse(typeof(Gender), Console.ReadLine(), true);
            int phoneNumber = int.Parse(Console.ReadLine());
            return new UserRecord(fullName, dateOfBirth, gender, phoneNumber);
        }
    }

    class ExceptionCatcher
    {

        static void main()
        {
            while (true)
            {
                Console.WriteLine("Please enter name, date of birth, gender, and phone number, on separate lines:");

                var profile = GetValidProfile();

                if (profile != null)
                {
                    Console.WriteLine($"Successfully parsed {profile}.");
                }

                Console.WriteLine();
            }
        }

        public static UserRecord GetValidProfile()
        {
            UserRecord result = null;

            // INSERT CODE HERE
            try
            {
                result = UserRecord.ScanProfile();
            }
            catch (FormatException e)
            {
                string errorMessage = e.Message;
                Console.WriteLine($"Invalid format: {errorMessage}");
                if (errorMessage.Contains("DateTime"))
                {
                    Console.WriteLine("Please enter a suitable date of birth.");
                }
                else
                {
                    Console.WriteLine("An integer value is required for phone number.");
                }
                return null;
            }
            catch(ArgumentException e)
            {
                string errorMessage = e.Message;
                Console.WriteLine($"Invalid argument: {errorMessage}");
                Console.WriteLine("Please ensure gender is Female, Male, or Other.");
                return null;
            }catch(Exception e)
            {
                string errorMessage = e.Message;
                Console.WriteLine($"Unknow Error: {errorMessage}");
                return null;
            }

            // INSERT CODE HERE
            Console.WriteLine("Operation was successful!");
            return result;
        }
    }
}