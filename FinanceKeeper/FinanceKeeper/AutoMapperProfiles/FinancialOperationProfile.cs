using AutoMapper;
using FinanceKeeper.Data.Entities;
using FinanceKeeper.Dtos;

namespace FinanceKeeper.AutoMapperProfiles
{
    public class FinancialOperationProfile : Profile
    {
        public FinancialOperationProfile()
        {
            CreateMap<FinancialOperation, FinancialOperationDto>();
            CreateMap<FinancialOperationDto, FinancialOperation>();
        }
    }
}
