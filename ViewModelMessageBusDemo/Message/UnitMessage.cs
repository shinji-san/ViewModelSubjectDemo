namespace ViewModelMessageBusDemo.Message;

using Messaging;

public sealed record UnitMessage(Unit Value) : IMessage<Unit>
{
    object IMessage.Value => Value;
}
