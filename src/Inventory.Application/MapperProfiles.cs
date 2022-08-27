using AutoMapper;
using Inventory.Application.Commands.AddPostCommand;
using Inventory.Application.Commands.AddThreadCommand;

namespace Inventory.Application;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<AddThreadCommand, Domain.Model.Thread>().ReverseMap();
        CreateMap<AddPostCommand, Domain.Model.Post>().ReverseMap();
    }
}
