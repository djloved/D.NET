using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Reflection;
using D.Net.Reflect;
using D.Net.Collection;
using System.Runtime.CompilerServices;

//Is it?
//test
//TEST2

namespace D.Net
{
    public class Factory<T> : DNetBaseObject
    {
        public Dictionary<string, T> FactoryItems { get; set; } = new Dictionary<string, T>(StringComparer.CurrentCultureIgnoreCase);
        public delegate bool OnDynamicCall(InvokeMemberBinder binder, object[] args, out object result);
        public event OnDynamicCall OnDynamicCallEvents;
        public Factory(string objName, string desc = "")
            : base(objName, desc)
        {

        }
        public override void Dispose()
        {
            foreach (var v in FactoryItems)
            {
                //sv.Value.Dispose();
            }
            FactoryItems.Clear();
            base.Dispose();
        }       
        public bool CreateFactoryItemInstance(string factoryItemName, Type instanceType, dynamic parameters = null)
        {
            if (FactoryItems.ContainsKey(factoryItemName))
            {
                return false;
            }
            else
            {
                try
                {
                    object newObject = AssemblyExtension.CreateObject(instanceType,parameters);
                    if (newObject != null)
                    {
                        FactoryItems[factoryItemName] = (T)newObject;
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }                
            }
            return false;
        }
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)//, [CallerMemberName] string callerName = "", [CallerFilePath] string callerPath = "", [CallerLineNumber] int callerLineNum = 0)
        {
            if (OnDynamicCallEvents != null)
                OnDynamicCallEvents(binder, args, out result);
            return base.TryInvokeMember(binder, args, out result);
        }


    }
}
