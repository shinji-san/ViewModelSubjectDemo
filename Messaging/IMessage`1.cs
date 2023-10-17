namespace Messaging;

public interface IMessage<out TValue> : IMessage
{
    new TValue Value { get; }
}
