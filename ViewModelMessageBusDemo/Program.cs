using Messaging;
using ViewModelMessageBusDemo.ViewModel;

IViewModelMessageBus messageBus = new ViewModelMessageBus();
SubjectViewModel? subjectViewModel = null;
ObserverViewModel? observerViewModel1 = null;
ObserverViewModel? observerViewModel2 = null;
try
{
    subjectViewModel = new(messageBus);
    observerViewModel1 = new(messageBus, 1);
    observerViewModel2 = new(messageBus, 2);
    
    subjectViewModel.SendOther(7777);

    subjectViewModel.Send(true);
    subjectViewModel.Send(false);

    subjectViewModel.Send(1);
    subjectViewModel.Send(2);

    subjectViewModel.Send();
    Console.ReadLine();
}
finally
{
    subjectViewModel?.Dispose();
    observerViewModel1?.Dispose();
    observerViewModel2?.Dispose();
}

