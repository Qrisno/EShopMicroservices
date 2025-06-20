namespace Ordering.Domain.Events;

public record OrderCreatingEvent(Order order): IDomainEvent;