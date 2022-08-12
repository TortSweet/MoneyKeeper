namespace FinanceKeeperBlazorServer.Data.Models
{
    public class FinancialOperation
    {
        public int OperationId { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int FinancialCategoryId { get; set; }
    }
}
