namespace AppointmentBooking
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("       APPOINTMENT BOOKING SYSTEM");
            Console.WriteLine("--------------------------------------------------");

            Appointment appointment =
                AppointmentManager.BookAppointment();

            AppointmentManager.PrintTicket(appointment);

            Console.ReadKey();
        }
    }
}