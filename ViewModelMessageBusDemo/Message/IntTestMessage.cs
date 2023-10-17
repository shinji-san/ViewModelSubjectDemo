namespace ViewModelMessageBusDemo.Message;

using Messaging;

public sealed record IntTestMessage(int Value) : IMessage<int>
{
    object IMessage.Value => ((IMessage)this).Value;
}