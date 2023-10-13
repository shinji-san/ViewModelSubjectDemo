namespace ViewModelSubject;

public record Subscription<TToken>(object Subscriber, TToken Token, Delegate? Action) where TToken : IEquatable<TToken>;