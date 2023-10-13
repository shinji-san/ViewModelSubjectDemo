namespace ViewModelSubjectDemo.Message;

using ViewModelSubject;

public sealed record BoolTestMessage(bool Value) : IValueChanged<bool>;