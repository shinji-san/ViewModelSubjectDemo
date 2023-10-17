namespace ViewModelMessageBusDemo.ViewModel;

using Message;
using Messaging;

public sealed class ObserverViewModel : IDisposable
{
    private readonly IViewModelMessageBus messageBus;
    private readonly int number;

    public ObserverViewModel(IViewModelMessageBus messageBus, int number)
    {
        this.messageBus = messageBus;
        this.number = number;
        this.Subscribe<string, BoolTestMessage>(messageBus, SubjectViewModel.ChannelIdentifier, this.Receive);
        this.Subscribe<string, IntTestMessage>(messageBus, SubjectViewModel.ChannelIdentifier, this.Receive);
        this.Subscribe<string, UnitMessage>(messageBus, SubjectViewModel.ChannelIdentifier, u => this.Receive(u.Value));
        this.Subscribe<string, OtherIntTestMessage>(messageBus, SubjectViewModel.ChannelIdentifier, this.Receive);
    }
    
    private void Receive(OtherIntTestMessage newMessage)
    {
        Console.WriteLine($"Observer{this.number}: {newMessage}");
    }
    
    private void Receive(Unit newValue)
    {
        Console.WriteLine($"Observer{this.number}: {newValue}");
    }
    
    private void Receive(BoolTestMessage newMessage)
    {
        Console.WriteLine($"Observer{this.number}: {newMessage}");
    }
    
    private void Receive(IntTestMessage newMessage)
    {
        Console.WriteLine($"Observer{this.number}: {newMessage}");
    }
    
    public void Dispose()
    {
        this.Unsubscribe<string, BoolTestMessage>(this.messageBus, SubjectViewModel.ChannelIdentifier);
        this.Unsubscribe<string, IntTestMessage>(this.messageBus, SubjectViewModel.ChannelIdentifier);
    }
}