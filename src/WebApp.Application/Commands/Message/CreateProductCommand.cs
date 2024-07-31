using MediatR;

namespace WebApp.Application.Commands.Message
{
    public class CreateProductCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
