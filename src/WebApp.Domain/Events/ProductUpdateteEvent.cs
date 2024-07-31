using MediatR;

namespace WebApp.Domain.Events
{
    public class ProductUpdateEvent : INotification
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}