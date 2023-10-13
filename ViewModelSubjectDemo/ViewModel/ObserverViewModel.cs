namespace ViewModelSubjectDemo;

using ViewModelSubject;

public class ObserverViewModel : IDisposable
{
    private readonly IViewModelSubject subject;
    private readonly int number;

    public ObserverViewModel(IViewModelSubject subject, int number)
    {
        this.subject = subject;
        this.number = number;
        this.Subscribe<BoolTestMessage, bool, string>(subject, SubjectViewModel.ChannelIdentifier, this.Receive);
        this.Subscribe<IntTestMessage, int, string>(subject, SubjectViewModel.ChannelIdentifier, this.Receive);
        this.Subscribe<UnitMessage, Unit, string>(subject, SubjectViewModel.ChannelIdentifier, u => this.Receive(u.Value));
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
        this.Unsubscribe<BoolTestMessage, bool, string>(this.subject, SubjectViewModel.ChannelIdentifier);
        this.Unsubscribe<IntTestMessage, int, string>(this.subject, SubjectViewModel.ChannelIdentifier);
    }
}