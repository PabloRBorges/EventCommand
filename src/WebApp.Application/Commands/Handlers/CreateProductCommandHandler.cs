using MediatR;
using WebApp.Application.Commands.Interfaces;
using WebApp.Application.Commands.Message;
using WebApp.Application.Events.Handlers;
using WebApp.Domain.Entities;
using WebApp.Domain.Events;
using WebApp.Domain.Interfaces;

namespace WebApp.Application.Commands.Handlers
{
    public class CreateProductCommandHandler : ICreateProductCommandHandler
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediator _mediator;

        public CreateProductCommandHandler(IProductRepository productRepository, IMediator mediator)
        {
            _productRepository = productRepository;
            _mediator = mediator;
        }

        public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(request.Name, request.Price);
            await _productRepository.AddProductAsync(product);

            var productCreatedEvent = new ProductCreatedEvent
            {
                Name = product.Name,
                Price = product.Price
            };

            await _mediator.Publish(productCreatedEvent, cancellationToken);

            var productUpdateEvent = new ProductUpdateEvent
            {
                Name = product.Name,
                Price = product.Price
            };

            await _mediator.Publish(productUpdateEvent, cancellationToken);

            return true;
        }
    }
}
