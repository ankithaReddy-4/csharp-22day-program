using System;

namespace AppointmentBooking
{
    public static class AppointmentManager
    {
        public static Appointment BookAppointment()
        {
            Appointment appointment = new Appointment();

            Console.Write("Enter Patient Name: ");
            appointment.PatientName = Console.ReadLine();

            string[] departments =
            {
                "General Medicine",
                "Dental",
                "Orthopedics"
            };

            string[] generalDoctors =
            {
                "Dr. A. Kumar",
                "Dr. B. Singh"
            };

            string[] dentalDoctors =
            {
                "Dr. C. Roy",
                "Dr. D. Gupta"
            };

            string[] orthopedicDoctors =
            {
                "Dr. E. Sharma",
                "Dr. F. Verma"
            };

            string[] timeSlots =
            {
                "10:00 AM",
                "11:00 AM",
                "12:00 PM"
            };

            int deptChoice;

            while (true)
            {
                Console.WriteLine("\nSelect Department:");

                for (int i = 0; i < departments.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {departments[i]}");
                }

                Console.Write("Enter Choice: ");

                if (int.TryParse(Console.ReadLine(), out deptChoice)
                    && deptChoice >= 1
                    && deptChoice <= departments.Length)
                {
                    break;
                }

                Console.WriteLine("Invalid Department Choice. Try Again.");
            }

            appointment.Department = departments[deptChoice - 1];

            string[] selectedDoctors = null;

            switch (deptChoice)
            {
                case 1:
                    selectedDoctors = generalDoctors;
                    break;

                case 2:
                    selectedDoctors = dentalDoctors;
                    break;

                case 3:
                    selectedDoctors = orthopedicDoctors;
                    break;
            }

            int doctorChoice;

            while (true)
            {
                Console.WriteLine("\nSelect Doctor:");

                for (int i = 0; i < selectedDoctors.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {selectedDoctors[i]}");
                }

                Console.Write("Enter Choice: ");

                if (int.TryParse(Console.ReadLine(), out doctorChoice)
                    && doctorChoice >= 1
                    && doctorChoice <= selectedDoctors.Length)
                {
                    break;
                }

                Console.WriteLine("Invalid Doctor Choice. Try Again.");
            }

            appointment.Doctor = selectedDoctors[doctorChoice - 1];

            int slotChoice;

            while (true)
            {
                Console.WriteLine("\nSelect Time Slot:");

                for (int i = 0; i < timeSlots.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {timeSlots[i]}");
                }

                Console.Write("Enter Choice: ");

                if (int.TryParse(Console.ReadLine(), out slotChoice)
                    && slotChoice >= 1
                    && slotChoice <= timeSlots.Length)
                {
                    break;
                }

                Console.WriteLine("Invalid Time Slot Choice. Try Again.");
            }

            appointment.TimeSlot = timeSlots[slotChoice - 1];

            return appointment;
        }

        public static void PrintTicket(Appointment appointment)
        {
            Console.WriteLine();
            Console.WriteLine("[Booking Confirmed]");
            Console.WriteLine();

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("            APPOINTMENT TICKET");
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine($"Patient:    {appointment.PatientName}");
            Console.WriteLine($"Department: {appointment.Department}");
            Console.WriteLine($"Doctor:     {appointment.Doctor}");
            Console.WriteLine($"Time:       {appointment.TimeSlot}");
            Console.WriteLine($"Status:     Confirmed");

            Console.WriteLine();
            Console.WriteLine("Please arrive 15 mins before your slot.");
            Console.WriteLine("--------------------------------------------------");
        }
    }
}