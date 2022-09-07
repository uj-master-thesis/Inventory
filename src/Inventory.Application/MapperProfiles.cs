﻿using AutoMapper;
using Inventory.Application.Commands.AddPostCommand;
using Inventory.Application.Commands.AddThreadCommand;
using Inventory.Application.Commands.Comment.AddCommentCommand;
using Inventory.Application.Commands.Vote;
using Inventory.Application.Response;
using Inventory.Application.Responses;

namespace Inventory.Application;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<AddThreadCommand, Domain.Model.Thread>()
            .ForMember(dest => dest.TimeStamp, opt  => opt.MapFrom(_ => DateTime.Now));

        CreateMap<AddPostCommand, Domain.Model.Post>()
            .ForMember(dest => dest.TimeStamp, opt => opt.MapFrom(_ => DateTime.Now))
            .ForMember(dest => dest.UpVotes, opt => opt.MapFrom(_ => 0))
            .ForMember(dest => dest.DownVotes, opt => opt.MapFrom(_ => 0))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(w => w.PostName));

        CreateMap<AddCommentCommand, Domain.Model.Comment>()
            .ForMember(dest => dest.TimeStamp, opt => opt.MapFrom(_ => DateTime.Now))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));

        CreateMap<AddVoteCommand, Domain.Model.Vote>()
            .ForMember(dest => dest.TimeStamp, opt => opt.MapFrom(_ => DateTime.Now))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));

        CreateMap<Domain.Model.Thread, GetThreadByIdResponse>();

        CreateMap<Domain.Model.Thread, GetThreadByIdWithPostsResponse>()
            .ForMember(dest => dest.PostCount, opt => opt.MapFrom(w =>  w.Posts.Count)); 

        CreateMap<Domain.Model.Post, GetPostResponse>()
            .ForMember(dest => dest.PostName, opt => opt.MapFrom(w => w.Name));

        CreateMap<Domain.Model.Comment, CommentResponse>();
    }
}
