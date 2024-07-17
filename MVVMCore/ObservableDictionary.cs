using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;

namespace MVVMCore;

public class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, INotifyCollectionChanged where TKey: notnull
{
    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    protected readonly Dictionary<TKey, TValue> Collection;

    public ObservableDictionary() 
    {
        Collection = new();    
    }

    public TValue this[TKey key]
    {
        get => Collection[key];
        set
        {
            Collection[key] = value;
            NotifyCollectionChanged(NotifyCollectionChangedAction.Replace);
        }
    }

    public void NotifyCollectionChanged(NotifyCollectionChangedAction action)
    {
        CollectionChanged?.Invoke(this, new(action, Collection));
    }

    public int Count => Collection.Count;

    public ICollection<TKey> Keys => Collection.Keys;

    public ICollection<TValue> Values => Collection.Values;

    public bool IsReadOnly => false;

    public void Add(TKey key, TValue value)
    {
        Collection.Add(key, value);
        NotifyCollectionChanged(NotifyCollectionChangedAction.Add);
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
        Collection.Add(item.Key, item.Value);
        NotifyCollectionChanged(NotifyCollectionChangedAction.Add);
    }

    public void Clear()
    {
        Collection.Clear();
        NotifyCollectionChanged(NotifyCollectionChangedAction.Remove);
    }

    public bool Contains(KeyValuePair<TKey, TValue> item) => Collection.Contains(item);

    public bool ContainsKey(TKey key) => Collection.ContainsKey(key);

    public bool Remove(TKey key)
    {
        if (Collection.Remove(key))
        {
            NotifyCollectionChanged(NotifyCollectionChangedAction.Remove);
            return true;
        }
        return false;
    }

    public bool Remove(KeyValuePair<TKey, TValue> item) => Remove(item.Key);

    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value) => Collection.TryGetValue(key, out value);
    
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        foreach (KeyValuePair<TKey, TValue> item in Collection)
        {
            yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        
    }
}
