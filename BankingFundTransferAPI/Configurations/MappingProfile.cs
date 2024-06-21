using AutoMapper;
using BankingFundTransferAPI.DTOs;
using BankingFundTransferAPI.Models;

namespace BankingFundTransferAPI.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TransferRequestDto, Transaction>();
        }
    }
}
