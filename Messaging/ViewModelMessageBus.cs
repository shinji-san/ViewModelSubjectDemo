namespace Messaging;

using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow;

public sealed class ViewModelMessageBus : IViewModelMessageBus
{
    private readonly ConcurrentDictionary<MessageTypeKey, SynchronizedCollection<object>> subscriberTable = new();
    
    public void Subscribe<TToken, TMessage>(object subscriber, TToken token)
        where TToken : IEquatable<TToken>
        where TMessage : IMessage
    {
        this.Subscribe<TToken,TMessage>(subscriber, token, null);
    }

    public void Subscribe<TToken, TMessage>(object subscriber, TToken token, Action<TMessage>? handler)
        where TToken : IEquatable<TToken>
        where TMessage : IMessage
    {
        if (subscriber == null)
        {
            throw new ArgumentNullException(nameof(subscriber));
        }

        MessageTypeKey messageTypeKey = new(typeof(TMessage), typeof(TToken));
        var subscriptions = Unsafe.As<SynchronizedCollection<Subscription<TToken>>>(this.subscriberTable.GetOrAdd(messageTypeKey, new SynchronizedCollection<object>()));
        var actionBlock = new ActionBlock<Action>(action => action.Invoke());
        var newSubscriber = new Subscription<TToken>(subscriber, token, handler, actionBlock);
        subscriptions.Add(newSubscriber);
    }

    public void Unsubscribe<TToken, TMessage>(object subscriber, TToken token)
        where TToken : IEquatable<TToken>
        where TMessage : IMessage
    {
        MessageTypeKey messageTypeKey = new(typeof(TMessage), typeof(TToken));
        if (!this.subscriberTable.TryGetValue(messageTypeKey, out var subscriptions))
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

    public void Notify<TToken, TMessage>(TToken token, TMessage message)
        where TToken : IEquatable<TToken>
        where TMessage : IMessage
    {
        MessageTypeKey messageTypeKey = new(typeof(TMessage), typeof(TToken));
        if (!this.subscriberTable.TryGetValue(messageTypeKey, out var subscriptions))
        {
            return;
        }

        foreach (Subscription<TToken> subscription in subscriptions)
        {
            if (subscription?.Handler is Action<TMessage> action && subscription.Token.Equals(token))
            {
                subscription.ActionBlock.Post(() => action.Invoke(message));
            }
        }
    }
}
