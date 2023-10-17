namespace Messaging;

public interface IViewModelMessageBus
{
    void Subscribe<TToken, TMessage>(object subscriber, TToken token)
        where TToken : IEquatable<TToken>
        where TMessage : IMessage;

    void Subscribe<TToken, TMessage>(object subscriber, TToken token, Action<TMessage>? handler)
        where TToken : IEquatable<TToken>
        where TMessage : IMessage;

    void Unsubscribe<TToken, TMessage>(object subscriber, TToken token)
        where TToken : IEquatable<TToken>
        where TMessage : IMessage;

    void Notify<TToken,TMessage>(TToken token, TMessage message)
        where TToken : IEquatable<TToken>
        where TMessage : IMessage;
}