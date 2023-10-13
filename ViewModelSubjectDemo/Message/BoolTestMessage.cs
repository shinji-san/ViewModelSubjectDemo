namespace ViewModelSubjectDemo;

using ViewModelSubject;

public sealed record BoolTestMessage(bool Value) : IValueChanged<bool>;