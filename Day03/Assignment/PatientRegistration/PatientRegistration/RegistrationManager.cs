namespace PatientRegistration
{
    public static class RegistrationManager
    {
        public static Patient RegisterPatient()
        {
            Patient patient = new Patient();

            
            patient.PatientID = "PAT-" + DateTime.Now.Year + "-001";

            
            while (true)
            {
                Console.Write("Enter Patient Name: ");
                patient.Name = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(patient.Name))
                    break;

                Console.WriteLine("Error: Name cannot be empty.");
            }

           
            while (true)
            {
                try
                {
                    Console.Write("Enter Age: ");
                    patient.Age = Convert.ToInt32(Console.ReadLine());

                    if (patient.Age <= 0 || patient.Age >= 120)
                    {
                        Console.WriteLine("Error: Age must be between 1 and 119.");
                        continue;
                    }

                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Please enter a valid numeric age.");
                }
            }

           
            Console.WriteLine("Enter Gender (Male/Female/Other): ");
            patient.Gender = Console.ReadLine();

            
            while (true)
            {
                Console.Write("Enter Phone Number: ");
                patient.PhoneNumber = Console.ReadLine();

                if (patient.PhoneNumber.Length == 10 &&
                    patient.PhoneNumber.All(char.IsDigit))
                {
                    break;
                }

                Console.WriteLine("Error: Phone Number must contain exactly 10 digits.");
            }

           
            Console.Write("Enter City: ");
            patient.City = Console.ReadLine();

            return patient;
        }

        public static void PrintRegistrationSlip(Patient patient)
        {
            Console.WriteLine();
            Console.WriteLine("[Registration Complete]");
            Console.WriteLine();

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("          PATIENT REGISTRATION SLIP");
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine($"Date: {DateTime.Now:dd-MM-yyyy}");

            Console.WriteLine();
            Console.WriteLine($"Patient ID: {patient.PatientID}");
            Console.WriteLine($"Name:       {patient.Name}");
            Console.WriteLine($"Age:        {patient.Age} years");
            Console.WriteLine($"Gender:     {patient.Gender}");
            Console.WriteLine($"Contact:    {patient.PhoneNumber}");
            Console.WriteLine($"Location:   {patient.City}");

            Console.WriteLine();
            Console.WriteLine("Instructions:");
            Console.WriteLine("Please proceed to the waiting area.");

            Console.WriteLine("--------------------------------------------------");
        }
    }
}