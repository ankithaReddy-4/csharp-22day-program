using System;

Console.WriteLine("--------------------------------------------------");
Console.WriteLine("          VITAL SIGNS MONITOR");
Console.WriteLine("--------------------------------------------------");

Console.Write("Enter Patient Name: ");
string patientName = Console.ReadLine();

double temperature = ReadTemperature();
int oxygen = ReadOxygen();
int pulse = ReadPulse();

Console.WriteLine();
Console.WriteLine("[Analyzing Data...]");
Console.WriteLine();

string status = CheckStatus(temperature, oxygen, pulse);

PrintReport(patientName, temperature, oxygen, pulse, status);


// ================= METHODS =================

static double ReadTemperature()
{
    while (true)
    {
        try
        {
            Console.Write("Enter Temperature (C): ");
            double temp = Convert.ToDouble(Console.ReadLine());

            if (temp < 30 || temp > 45)
            {
                Console.WriteLine("Invalid temperature range.");
                continue;
            }

            return temp;
        }
        catch (FormatException)
        {
            Console.WriteLine("Please enter a valid numeric temperature.");
        }
    }
}

static int ReadOxygen()
{
    while (true)
    {
        try
        {
            Console.Write("Enter Oxygen Level (%): ");
            int oxygen = Convert.ToInt32(Console.ReadLine());

            if (oxygen < 0 || oxygen > 100)
            {
                Console.WriteLine("Oxygen level must be between 0 and 100.");
                continue;
            }

            return oxygen;
        }
        catch (FormatException)
        {
            Console.WriteLine("Please enter a valid oxygen percentage.");
        }
    }
}

static int ReadPulse()
{
    while (true)
    {
        try
        {
            Console.Write("Enter Pulse Rate (BPM): ");
            int pulse = Convert.ToInt32(Console.ReadLine());

            if (pulse <= 0)
            {
                Console.WriteLine("Pulse rate must be greater than zero.");
                continue;
            }

            return pulse;
        }
        catch (FormatException)
        {
            Console.WriteLine("Please enter a valid pulse rate.");
        }
    }
}

static string CheckStatus(double temp, int oxygen, int pulse)
{
    if (temp > 39.0 ||
        oxygen < 90 ||
        pulse < 50 ||
        pulse > 120)
    {
        return "CRITICAL / EMERGENCY";
    }
    else if (temp > 37.5 ||
             oxygen < 95 ||
             pulse > 100)
    {
        return "OBSERVATION NEEDED";
    }
    else
    {
        return "NORMAL";
    }
}

static void PrintReport(string name,
                        double temp,
                        int oxygen,
                        int pulse,
                        string status)
{
    Console.WriteLine("--------------------------------------------------");
    Console.WriteLine("       MEDICAL ASSESSMENT REPORT");
    Console.WriteLine("--------------------------------------------------");

    Console.WriteLine($"Patient: {name}");
    Console.WriteLine();

    Console.WriteLine("Vitals Recorded:");
    Console.WriteLine($"- Temp:   {temp} C");
    Console.WriteLine($"- Oxygen: {oxygen} %");
    Console.WriteLine($"- Pulse:  {pulse} BPM");

    Console.WriteLine();
    Console.WriteLine($"Status Assessment: {status}");

    if (status == "CRITICAL / EMERGENCY")
    {
        Console.WriteLine("Action: Immediate doctor attention required.");
    }
    else if (status == "OBSERVATION NEEDED")
    {
        Console.WriteLine("Action: Nurse to monitor every hour.");
    }
    else
    {
        Console.WriteLine("Action: Continue routine monitoring.");
    }

    Console.WriteLine("--------------------------------------------------");
}