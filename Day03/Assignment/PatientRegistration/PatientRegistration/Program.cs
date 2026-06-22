namespace PatientRegistration
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("     HOSPITAL PATIENT REGISTRATION SYSTEM");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine();

            Patient patient = RegistrationManager.RegisterPatient();

            RegistrationManager.PrintRegistrationSlip(patient);

            Console.ReadKey();
        }
    }
}