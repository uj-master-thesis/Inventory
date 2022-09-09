using AutoMapper;
using Inventory.Application.Interfaces.ReadRepositories;
using Inventory.Application.Responses;
using MediatR;


namespace Inventory.Application.Queries.Comment.GetUserCommentQuery;

public class GetUserCommentHandler : IRequestHandler<GetUserCommentQuery, List<CommentResponse>>
{
    private readonly IReadCommentRepository _readCommentRepository;
    private readonly IMapper _mapper;

    public GetUserCommentHandler(IMapper mapper, IReadCommentRepository readCommentRepository)
    {
        _mapper = mapper;
        _readCommentRepository = readCommentRepository;
    }

    public async Task<List<CommentResponse>> Handle(GetUserCommentQuery request, CancellationToken cancellationToken)
    {
        var comments = await _readCommentRepository.GetListOfCommentsByUser(request.UserName);
        return comments.Select(w => _mapper.Map<CommentResponse>(w)).ToList();
    }
}
