namespace ViewModelSubjectDemo;

using ViewModelSubject;

public class SubjectViewModel : IDisposable
{
    public const string ChannelIdentifier = nameof(SubjectViewModel);
    private readonly IViewModelSubject subject;
    
    public SubjectViewModel(IViewModelSubject subject)
    {
        this.subject = subject;
        this.Subscribe<BoolTestMessage, bool, string>(subject, ChannelIdentifier);
        this.Subscribe<IntTestMessage, int, string>(subject, ChannelIdentifier, this.Receive);
        this.Subscribe<UnitMessage, Unit, string>(subject, ChannelIdentifier);
    }

    public void Send(bool newValue)
    {
        this.subject.Notify<BoolTestMessage, bool, string>("s", new BoolTestMessage(newValue));
    }
    
    public void Send(int newValue)
    {
        this.subject.Notify<IntTestMessage, int, string>("s", new IntTestMessage(newValue));
    }
    
    public void Send()
    {
        this.subject.Notify<UnitMessage, Unit, string>("s", new UnitMessage(Unit.Default));
    }
    
    private void Receive(IntTestMessage newMessage)
    {
        Console.WriteLine($"Subject: {newMessage}");
    }

    public void Dispose()
    {
        this.Unsubscribe<BoolTestMessage, bool, string>(this.subject, ChannelIdentifier);
        this.Unsubscribe<IntTestMessage, int, string>(this.subject, ChannelIdentifier);
    }
}