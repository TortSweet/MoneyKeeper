namespace FinanceKeeper.Data.Entities
{
    public class MoneyReport
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public virtual ICollection<FinancialOperation>? ListOfOperations { get; set; }
    }
}
