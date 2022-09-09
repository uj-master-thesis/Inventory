using AutoMapper;
using Inventory.Application.Interfaces.ReadRepositories;
using Inventory.Application.Responses;
using MediatR;

namespace Inventory.Application.Queries.Comment.GetPostCommentQuery;

public class GetPostCommentHandler : IRequestHandler<GetPostCommentQuery, List<CommentResponse>>
{
    private readonly IReadCommentRepository _readCommentRepository;
    private readonly IMapper _mapper; 

    public GetPostCommentHandler(IMapper mapper, IReadCommentRepository readCommentRepository)
    {
        _mapper = mapper;
        _readCommentRepository = readCommentRepository; 
    }

    public async Task<List<CommentResponse>> Handle(GetPostCommentQuery request, CancellationToken cancellationToken)
    {
        var comments = await _readCommentRepository.GetListOfCommentsByPostName(request.PostName);
        return comments.Select(w => _mapper.Map<CommentResponse>(w)).ToList(); 
    }
}
