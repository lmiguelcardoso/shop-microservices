namespace Ordering.Application.Orders.EventHandlers.Domain
{
    internal class OrderUpdatedEventHandler(ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderUpdatedEvent>
    {
        public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain event Handled: {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
