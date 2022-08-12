namespace FinanceKeeper.Data.Entities
{
    public class FinancialCategory
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public FinanceType Type { get; set; }
    }
}
