namespace Messaging;

public static class ViewModelSubjectExtension
{
    public static void Subscribe<TToken,TMessage>(
        this object subscriber,
        IViewModelMessageBus messageBus,
        TToken token)
        where TToken : IEquatable<TToken>
        where TMessage : IMessage
    {
        messageBus.Subscribe<TToken, TMessage>(subscriber, token);
    }

    public static void Subscribe<TToken, TMessage>(
        this object subscriber,
        IViewModelMessageBus messageBus,
        TToken token, Action<TMessage>? handler)
        where TToken : IEquatable<TToken>
        where TMessage : IMessage
    {
        messageBus.Subscribe(subscriber, token, handler);
    }
        
    public static void Unsubscribe<TToken, TMessage>(
        this object subscriber,
        IViewModelMessageBus messageBus,
        TToken token)
        where TToken : IEquatable<TToken>
        where TMessage : IMessage
    {
        messageBus.Unsubscribe<TToken, TMessage>(subscriber, token);
    }
}