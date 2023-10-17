namespace ViewModelMessageBusDemo.Message;

using Messaging;

public sealed record OtherIntTestMessage(int Value) : IMessage<int>
{
    object IMessage.Value => Value;
}
