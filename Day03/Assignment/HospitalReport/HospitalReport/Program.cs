using HospitalReports;

List<PatientRecord> patients = new List<PatientRecord>()
{
    new PatientRecord
    {
        Name = "John Doe",
        Department = "General",
        BillAmount = 500,
        Status = "Discharged"
    },

    new PatientRecord
    {
        Name = "Jane Smith",
        Department = "Dental",
        BillAmount = 1200,
        Status = "Admitted"
    },

    new PatientRecord
    {
        Name = "Bob Brown",
        Department = "General",
        BillAmount = 400,
        Status = "Discharged"
    },

    new PatientRecord
    {
        Name = "Alice Wilson",
        Department = "Ortho",
        BillAmount = 2500,
        Status = "Admitted"
    },

    new PatientRecord
    {
        Name = "Sam Kumar",
        Department = "Dental",
        BillAmount = 800,
        Status = "Discharged"
    }
};

decimal totalRevenue = 0;

Dictionary<string, int> departmentCount =
    new Dictionary<string, int>();

foreach (PatientRecord patient in patients)
{
    totalRevenue += patient.BillAmount;

    if (departmentCount.ContainsKey(patient.Department))
    {
        departmentCount[patient.Department]++;
    }
    else
    {
        departmentCount[patient.Department] = 1;
    }
}

Console.WriteLine("--------------------------------------------------");
Console.WriteLine("      DAILY HOSPITAL ACTIVITY REPORT");
Console.WriteLine("--------------------------------------------------");

Console.WriteLine($"Date: {DateTime.Now:dd-MM-yyyy}");
Console.WriteLine();

Console.WriteLine("Patient List:");

int serialNo = 1;

foreach (PatientRecord patient in patients)
{
    Console.WriteLine(
        $"{serialNo}. {patient.Name} - {patient.Department} - ₹{patient.BillAmount}");

    serialNo++;
}

Console.WriteLine();
Console.WriteLine("--------------------------------------------------");
Console.WriteLine("SUMMARY STATISTICS");
Console.WriteLine("--------------------------------------------------");

Console.WriteLine($"Total Patients Visited: {patients.Count}");
Console.WriteLine($"Total Revenue: ₹{totalRevenue}");

Console.WriteLine();
Console.WriteLine("Traffic by Department:");

foreach (var department in departmentCount)
{
    Console.WriteLine($"- {department.Key}: {department.Value}");
}

Console.WriteLine();
Console.WriteLine("End of Report.");
Console.WriteLine("--------------------------------------------------");