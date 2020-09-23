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
                    Console.WriteLine("Please use format DD/MM/YYYY for date of birth.");
                }
                else
                {
                    Console.WriteLine("Please supply an integer value for phone number.");
                }
            }
            catch(ArgumentException e)
            {
                string errorMessage = e.Message;
                Console.WriteLine($"Invalid format: {errorMessage}");
                Console.WriteLine("Please enter Male, Female, or Other for gender.");
            }catch(Exception e)
            {
                string errorMessage = e.Message;
                Console.WriteLine($"Unknow Error: {errorMessage}");
            }

            // INSERT CODE HERE

            return result;
        }
    }
}