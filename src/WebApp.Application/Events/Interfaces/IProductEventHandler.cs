using MediatR;
using WebApp.Domain.Events;

namespace WebApp.Application.Events.Interfaces
{
    public interface IProductEventHandler : 
        INotificationHandler<ProductUpdateEvent>, 
        INotificationHandler<ProductCreatedEvent>
    {
    }
}
