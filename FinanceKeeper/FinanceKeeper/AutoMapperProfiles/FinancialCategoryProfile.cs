using AutoMapper;
using FinanceKeeper.Data.Entities;
using FinanceKeeper.Dtos;

namespace FinanceKeeper.AutoMapperProfiles
{
    public class FinancialCategoryProfile : Profile
    {
        public FinancialCategoryProfile()
        {
            CreateMap<FinancialCategoryDto, FinancialCategory>();
            CreateMap<FinancialCategory, FinancialCategoryDto>();
        }
    }
}
