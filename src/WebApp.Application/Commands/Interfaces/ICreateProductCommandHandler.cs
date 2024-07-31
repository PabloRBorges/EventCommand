using MediatR;
using WebApp.Application.Commands.Message;

namespace WebApp.Application.Commands.Interfaces
{
    public interface ICreateProductCommandHandler : IRequestHandler<CreateProductCommand, bool> { }
}
