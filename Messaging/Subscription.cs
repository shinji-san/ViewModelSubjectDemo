namespace Messaging;

using System.Threading.Tasks.Dataflow;

public record Subscription<TToken>(object Subscriber, TToken Token, Delegate? Handler, ActionBlock<Action> ActionBlock)
    where TToken : IEquatable<TToken>;