using System;
using System.Collections.Generic;
using System.Text;

namespace D.Net.Collection
{
    public class ManagedDict<TKey, TValue> : Dictionary<TKey, TValue>, IDisposable
    {
        public ManagedDict()
        {
        }

        public ManagedDict(int capacity)
            : base(capacity)
        {

        }
        public ManagedDict(IEqualityComparer<TKey> comparer)
            : base(comparer)
        { }

        public ManagedDict(int capacity, IEqualityComparer<TKey> comparer)
            : base(capacity, comparer)
        {

        }

        public ManagedDict(IDictionary<TKey, TValue> dictionary)
            : base(dictionary)
        {

        }
        public ManagedDict(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
            : base(dictionary, comparer)
        {

        }
        public void Dispose()
        {
            try
            {
                this.Clear();
                //GC.SuppressFinalize(values);
                GC.SuppressFinalize(this);
                GarbageManagement.CollectGarbage();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
