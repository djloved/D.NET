using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;


namespace D.Net.Collection
{
    public class DynamicDictionary : DynamicObject, IDisposable
    {
        // The inner dictionary.
        public Dictionary<string, object> values = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        // This property returns the number of elements 
        // in the inner dictionary. 
        public int Count
        {
            get
            {
                return values.Count;
            }
        }
        // If you try to get a value of a property  
        // not defined in the class, this method is called. 

        public DynamicDictionary()
        {

        }
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            // Converting the property name to lowercase 
            // so that property names become case-insensitive. 
            //string name = binder.Name.ToLower();

            // If the property name is found in a dictionary, 
            // set the result parameter to the property value and return true. 
            // Otherwise, return false. 
            // return values.TryGetValue(binder.Name, out result);
            result = this[binder.Name];
            return true;
        }

        // If you try to set a value of a property that is 
        // not defined in the class, this method is called. 
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            // Converting the property name to lowercase 
            // so that property names become case-insensitive.
            values[binder.Name] = value;

            // You can always add a value to a dictionary, 
            // so this method always returns true. 
            return true;
        }
        /// <summary>
        /// If a property value is a delegate, invoke it
        /// </summary>     
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            string name = binder.Name;//.ToLower();
            if (values.ContainsKey(name) && values[name] is Delegate)
            {
                result = (values[name] as Delegate).DynamicInvoke(args);
                return true;
            }
            else
            {
                return base.TryInvokeMember(binder, args, out result);
            }
        }

        /// <summary>
        /// Return all dynamic member names
        /// </summary>
        /// <returns>
        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return values.Keys;
        }
        public object this[string key]
        {
            get
            {
                if (!values.ContainsKey(key))
                    return null;
                return values[key];
            }
            set
            {
                values[key] = value;
            }
        }
        public bool ContainsKey(string keyName)
        {
            return values.ContainsKey(keyName);
        }  
        
        public DynamicDictionary Clone()
        {
            DynamicDictionary dic = new DynamicDictionary();
            foreach (var v in values)
            {
                dic.values[v.Key] = v.Value;
            }
            return dic;
        }
        public virtual void Dispose()
        {
            values.Clear();
            try
            {
                //values = null;                
                GC.SuppressFinalize(values);
                GC.SuppressFinalize(this);
                GarbageManagement.CollectGarbage();
            }
            catch (Exception ex)
            {
                //Singleton<LogMan>.Inst.doLog(ex);
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var v in values)
            {
                sb.AppendFormat("{0} : {1} ", v.Key, v.Value);
                sb.AppendLine();
            }

            return sb.ToString();
        }

        public void DummyStop(object info = null)
        {
            //((dynamic)this).accgInputParameter = "aa";
        }        

        public void Merge(DynamicDictionary dest)
        {
            foreach (var o in dest.values)
            {
                values[o.Key] = o.Value;
            }
        }
    }
}
