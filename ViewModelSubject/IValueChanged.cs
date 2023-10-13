namespace ViewModelSubject;

public interface IValueChanged<out TValue>
{
    TValue Value { get; }
}