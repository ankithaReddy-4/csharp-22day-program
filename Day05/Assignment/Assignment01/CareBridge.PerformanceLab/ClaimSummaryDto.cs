public class ClaimSummaryDto
{
    public string Status { get; set; }
    public int ClaimCount { get; set; }
    public decimal TotalBilled { get; set; }
    public decimal TotalReimbursed { get; set; }
    public decimal Gap { get; set; }
}