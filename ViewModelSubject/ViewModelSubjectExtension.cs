namespace ViewModelSubject;

public static class ViewModelSubjectExtension
{
    public static void Subscribe<TMessage, TValue, TToken>(
        this object subscriber,
        IViewModelSubject subject,
        TToken token)
        where TToken : IEquatable<TToken>
        where TMessage : IValueChanged<TValue>
    {
        subject.Subscribe<TMessage, TValue, TToken>(subscriber, token);
    }

    public static void Subscribe<TMessage, TValue, TToken>(
        this object subscriber,
        IViewModelSubject subject,
        TToken token, Action<TMessage>? handler)
        where TToken : IEquatable<TToken>
        where TMessage : IValueChanged<TValue>
    {
        subject.Subscribe<TMessage, TValue, TToken>(subscriber, token, handler);
    }
        
    public static void Unsubscribe<TMessage, TValue, TToken>(
        this object subscriber,
        IViewModelSubject subject,
        TToken token)
        where TToken : IEquatable<TToken>
        where TMessage : IValueChanged<TValue>
    {
        subject.Unsubscribe<TMessage, TValue, TToken>(subscriber, token);
    }
}