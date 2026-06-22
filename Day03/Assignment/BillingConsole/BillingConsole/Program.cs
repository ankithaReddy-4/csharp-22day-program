const decimal CONSULTATION_FEE = 500m;
const decimal BLOOD_TEST_FEE = 200m;
const decimal XRAY_FEE = 1000m;
const decimal ADMISSION_FEE = 2000m;

Console.WriteLine("--------------------------------------------------");
Console.WriteLine("      HOSPITAL BILLING CALCULATOR");
Console.WriteLine("--------------------------------------------------");

Bill bill = new Bill();

Console.Write("Patient Name: ");
bill.PatientName = Console.ReadLine();

while (true)
{
    try
    {
        Console.Write("Patient Age: ");
        bill.Age = Convert.ToInt32(Console.ReadLine());

        if (bill.Age <= 0)
        {
            Console.WriteLine("Age must be greater than zero.");
            continue;
        }

        break;
    }
    catch (FormatException)
    {
        Console.WriteLine("Please enter a valid age.");
    }
}

bool consultationAdded = false;

while (true)
{
    Console.WriteLine();
    Console.WriteLine("Add Services:");
    Console.WriteLine($"1. Consultation ({CONSULTATION_FEE})");
    Console.WriteLine($"2. Blood Test ({BLOOD_TEST_FEE})");
    Console.WriteLine($"3. X-Ray ({XRAY_FEE})");
    Console.WriteLine($"4. Admission ({ADMISSION_FEE})");
    Console.WriteLine("5. Done");

    Console.Write("Choice: ");

    if (!int.TryParse(Console.ReadLine(), out int choice))
    {
        Console.WriteLine("Invalid choice.");
        continue;
    }

    switch (choice)
    {
        case 1:
            bill.BaseAmount += CONSULTATION_FEE;
            consultationAdded = true;
            Console.WriteLine("[Added Consultation]");
            break;

        case 2:
            bill.BaseAmount += BLOOD_TEST_FEE;
            Console.WriteLine("[Added Blood Test]");
            break;

        case 3:
            bill.BaseAmount += XRAY_FEE;
            Console.WriteLine("[Added X-Ray]");
            break;

        case 4:
            bill.BaseAmount += ADMISSION_FEE;
            Console.WriteLine("[Added Admission]");
            break;

        case 5:
            goto CalculateBill;

        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
}

CalculateBill:

Console.WriteLine();
Console.WriteLine("[Calculating Bill...]");
Console.WriteLine();

decimal discountRate = 0;

if (bill.Age > 60)
{
    discountRate = 0.20m;
    bill.DiscountAmount = bill.BaseAmount * discountRate;
}
else if (bill.Age < 10 && consultationAdded)
{
    bill.DiscountAmount = CONSULTATION_FEE * 0.50m;
}

decimal amountAfterDiscount =
    bill.BaseAmount - bill.DiscountAmount;

bill.TaxAmount = amountAfterDiscount * 0.05m;

bill.NetAmount =
    amountAfterDiscount + bill.TaxAmount;

PrintBill(bill);


// ---------------- METHODS ----------------

static void PrintBill(Bill bill)
{
    string category = "";

    if (bill.Age > 60)
        category = "Senior Citizen";
    else if (bill.Age < 10)
        category = "Child";

    Console.WriteLine("--------------------------------------------------");
    Console.WriteLine("            FINAL BILL INVOICE");
    Console.WriteLine("--------------------------------------------------");

    if (string.IsNullOrEmpty(category))
        Console.WriteLine($"Patient: {bill.PatientName}");
    else
        Console.WriteLine($"Patient: {bill.PatientName} ({category})");

    Console.WriteLine();

    Console.WriteLine($"Base Amount:      {bill.BaseAmount:F2}");
    Console.WriteLine($"Discount:        -{bill.DiscountAmount:F2}");
    Console.WriteLine($"Tax (5%):        +{bill.TaxAmount:F2}");

    Console.WriteLine();
    Console.WriteLine("--------------------------------------------------");
    Console.WriteLine($"TOTAL PAYABLE:    {bill.NetAmount:F2}");
    Console.WriteLine("--------------------------------------------------");
}