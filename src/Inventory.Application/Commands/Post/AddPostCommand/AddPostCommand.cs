using Inventory.Domain.Model;
using MediatR;

namespace Inventory.Application.Commands.AddPostCommand;

public class AddPostCommand : IRequest
{
    public string PostName { get; set; }
    public Uri? Url { get; set; }
    public string Description { get; set; }
    public string UserName { get; set; }
    public string ThreadName { get; set; }
    public string FileCompressed { get; set; }

}