namespace ViewModelSubject;

public interface IViewModelSubject
{
    void Subscribe<TMessage, TValue, TToken>(object subscriber, TToken token)
        where TToken : IEquatable<TToken>
        where TMessage : IValueChanged<TValue>;

    void Subscribe<TMessage, TValue, TToken>(object subscriber, TToken token, Action<TMessage>? handler)
        where TToken : IEquatable<TToken>
        where TMessage : IValueChanged<TValue>;

    void Unsubscribe<TMessage, TValue, TToken>(object subscriber, TToken token)
        where TToken : IEquatable<TToken>
        where TMessage : IValueChanged<TValue>;

    void Notify<TMessage, TValue, TToken>(TToken token, TMessage newMessage)
        where TToken : IEquatable<TToken>
        where TMessage : IValueChanged<TValue>;
}