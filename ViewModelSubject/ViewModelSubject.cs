namespace ViewModelSubject;

using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

public sealed class ViewModelSubject : IViewModelSubject
{
    private readonly ConcurrentDictionary<ValueType, SynchronizedCollection<object>> subscriberTable = new();
        
    public void Subscribe<TMessage, TValue, TToken>(object subscriber, TToken token)
        where TToken : IEquatable<TToken>
        where TMessage : IValueChanged<TValue>
    {
        this.Subscribe<TMessage, TValue, TToken>(subscriber, token, null);
    }

    public void Subscribe<TMessage, TValue, TToken>(object subscriber, TToken token, Action<TMessage>? handler)
        where TToken : IEquatable<TToken>
        where TMessage : IValueChanged<TValue>
    {
        if (subscriber == null)
        {
            throw new ArgumentNullException(nameof(subscriber));
        }

        ValueType valueType = new(typeof(TMessage), typeof(TToken));
        var subscriptions = Unsafe.As<SynchronizedCollection<Subscription<TToken>>>(this.subscriberTable.GetOrAdd(valueType, new SynchronizedCollection<object>()));
        var newSubscriber = new Subscription<TToken>(subscriber, token, handler);
        subscriptions.Add(newSubscriber);
    }

    public void Unsubscribe<TMessage, TValue, TToken>(object subscriber, TToken token)
        where TToken : IEquatable<TToken>
        where TMessage : IValueChanged<TValue>
    {
        ValueType valueType = new(typeof(TMessage), typeof(TToken));
        if (!this.subscriberTable.TryGetValue(valueType, out var subscriptions))
        {
            return;
        }

        var subscription = subscriptions.FirstOrDefault(currentSubscription =>
        {
            var subscription = currentSubscription as Subscription<TToken>;
            return subscription?.Subscriber == subscriber && subscription.Token.Equals(token);
        });

        if (subscription != null)
        {
            subscriptions.Remove(subscription);
        }
    }

    public void Notify<TMessage, TValue, TToken>(TToken token, TMessage newMessage)
        where TToken : IEquatable<TToken>
        where TMessage : IValueChanged<TValue>
    {
        ValueType valueType = new(typeof(TMessage), typeof(TToken));
        if (!this.subscriberTable.TryGetValue(valueType, out var subscriptions))
        {
            return;
        }

        foreach (Subscription<TToken> subscription in subscriptions)
        {
            if (subscription?.Action is Action<TMessage> action && subscription.Token.Equals(token))
            {
                action(newMessage);
            }
        }
    }
}