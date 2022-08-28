using AutoMapper;
using Inventory.Application.Commands.AddPostCommand;
using Inventory.Application.Commands.AddThreadCommand;
using Inventory.Application.Response;
using Inventory.Application.Responses;

namespace Inventory.Application;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<AddThreadCommand, Domain.Model.Thread>()
            .ForMember(dest => dest.InsertDate, opt  => opt.MapFrom(_ => DateTime.Now));
        CreateMap<AddPostCommand, Domain.Model.Post>()
            .ForMember(dest => dest.InsertDate, opt => opt.MapFrom(_ => DateTime.Now));
        CreateMap<Domain.Model.Thread, GetThreadByIdResponse>();
        CreateMap<Domain.Model.Thread, GetThreadByIdWithPostsResponse>();
        CreateMap<Domain.Model.Post, GetPostByIdResponse>(); 
    }
}
