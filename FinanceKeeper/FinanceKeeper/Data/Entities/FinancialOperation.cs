namespace FinanceKeeper.Data.Entities
{
    public class FinancialOperation
    {
        public int OperationId { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int FinancialCategoryId { get; set; }
        //public virtual FinancialCategory Category { get; set; }
    }
}
