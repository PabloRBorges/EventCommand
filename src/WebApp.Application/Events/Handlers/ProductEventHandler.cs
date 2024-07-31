using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Application.Events.Interfaces;
using WebApp.Domain.Events;

namespace WebApp.Application.Events.Handlers
{
    public class ProductEventHandler : IProductEventHandler
    {
        public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            // Lógica para tratar o evento, ex.: enviar email de confirmação

            return Task.CompletedTask;
        }

        public Task Handle(ProductUpdateEvent notification, CancellationToken cancellationToken)
        {
            // Lógica para tratar o evento, ex.: enviar email de confirmação

            return Task.CompletedTask;
        }
    }
}
