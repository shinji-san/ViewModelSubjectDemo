namespace ViewModelMessageBusDemo.ViewModel;

using Message;
using Messaging;

public sealed class SubjectViewModel : IDisposable
{
    public const string ChannelIdentifier = nameof(SubjectViewModel);
    private readonly IViewModelMessageBus messageBus;
    
    public SubjectViewModel(IViewModelMessageBus messageBus)
    {
        this.messageBus = messageBus;
        this.Subscribe<string, BoolTestMessage>(messageBus, ChannelIdentifier);
        this.Subscribe<string, IntTestMessage>(messageBus, ChannelIdentifier, this.Receive);
        this.Subscribe<string, OtherIntTestMessage>(messageBus, ChannelIdentifier);
        this.Subscribe<string, UnitMessage>(messageBus, ChannelIdentifier);
    }

    public void Send(bool newValue)
    {
        this.messageBus.Notify(ChannelIdentifier, new BoolTestMessage(newValue));
    }
    
    public void Send(int newValue)
    {
        this.messageBus.Notify(ChannelIdentifier, new IntTestMessage(newValue));
    }
    
    public void SendOther(int newValue)
    {
        this.messageBus.Notify(ChannelIdentifier, new OtherIntTestMessage(newValue));
    }
    
    public void Send()
    {
        this.messageBus.Notify(ChannelIdentifier, new UnitMessage(Unit.Default));
    }
    
    private void Receive(IntTestMessage newMessage)
    {
        Console.WriteLine($"Subject: {newMessage}");
    }

    public void Dispose()
    {
        this.Unsubscribe<string, BoolTestMessage>(this.messageBus, ChannelIdentifier);
        this.Unsubscribe<string, IntTestMessage>(this.messageBus, ChannelIdentifier);
    }
}