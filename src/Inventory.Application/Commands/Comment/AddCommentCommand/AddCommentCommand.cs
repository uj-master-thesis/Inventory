
using MediatR;

namespace Inventory.Application.Commands.Comment.AddCommentCommand;

public class AddCommentCommand : IRequest
{
    public string Text { get; set; }
    public string UserName { get; set; }
    public string PostName { get; set; }
    public string FileCompressed { get; set; }
}
