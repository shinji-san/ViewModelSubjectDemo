namespace ViewModelMessageBusDemo.Message;

using Messaging;

public sealed record BoolTestMessage(bool Value) : IMessage<bool>
{
    object IMessage.Value => Value;
}
