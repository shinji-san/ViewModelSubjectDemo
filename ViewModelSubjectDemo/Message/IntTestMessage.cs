namespace ViewModelSubjectDemo;

using ViewModelSubject;

public sealed record IntTestMessage(int Value) : IValueChanged<int>;