using AutoMapper;
using GestorDeLimites.Domain.Dtos;
using GestorDeLimites.Application.Models.Requests;
using GestorDeLimites.Infrastructure.Data.Dynamo;
using Amazon.DynamoDBv2.Model;

namespace GestorDeLimites.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LimiteClienteDto, LimiteCliente>();

            CreateMap<CadastrarLimiteRequest, LimiteClienteDto>()
                .ForMember(dest => dest.NumeroAgencia, opt => opt.MapFrom(src => src.Agencia))
                .ForMember(dest => dest.NumeroConta, opt => opt.MapFrom(src => src.Conta))
                .ForMember(dest => dest.LimitePIX, opt => opt.MapFrom(src => src.Limite));

            CreateMap<AlterarLimiteRequest, LimiteClienteDto>()
                .ForMember(dest => dest.LimitePIX, opt => opt.MapFrom(src => src.NovoLimite));

            CreateMap<BuscarLimiteRequest, LimiteClienteDto>()
                .ForMember(dest => dest.NumeroAgencia, opt => opt.MapFrom(src => src.Agencia))
                .ForMember(dest => dest.NumeroConta, opt => opt.MapFrom(src => src.Conta));

            CreateMap<RemoverLimiteRequest, LimiteClienteDto>()
                .ForMember(dest => dest.NumeroAgencia, opt => opt.MapFrom(src => src.Agencia))
                .ForMember(dest => dest.NumeroConta, opt => opt.MapFrom(src => src.Conta));

            CreateMap<VerificarTransacaoPixRequest, BuscarLimiteRequest>()
                .ReverseMap();

        }
    }
}
