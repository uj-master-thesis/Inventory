using Inventory.Domain.Model;
using MediatR;

namespace Inventory.Application.Commands.Vote;

public class AddVoteCommand : IRequest
{
    public VoteType VoteType { get; set; }
    public string UserName { get; set; }
    public string PostName { get; set; }
}
