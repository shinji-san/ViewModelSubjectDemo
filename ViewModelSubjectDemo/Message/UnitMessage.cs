namespace ViewModelSubjectDemo;

using ViewModelSubject;

public sealed record UnitMessage(Unit Value) : IValueChanged<Unit>;