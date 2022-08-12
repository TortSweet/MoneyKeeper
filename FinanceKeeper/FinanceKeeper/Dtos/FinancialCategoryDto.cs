using System.ComponentModel;
using FinanceKeeper.Data.Entities;

namespace FinanceKeeper.Dtos
{
    public class FinancialCategoryDto
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public FinanceType Type { get; set; }
    }
}
