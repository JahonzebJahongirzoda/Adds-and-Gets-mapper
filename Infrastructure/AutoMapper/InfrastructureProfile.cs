using AutoMapper;
using Domain.Dtos;

namespace Infrastructure.AutoMapper;

public class InfrastructureProfile:Profile
{
    public InfrastructureProfile()
    {
        CreateMap<Challange, GetChallengeDto>();
        CreateMap<Group, GetGroupDto>();
        CreateMap<AddChallengeDto, Challange>();
    }
}