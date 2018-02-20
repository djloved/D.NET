using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using D.Net.Reflect;
namespace D.Net
{
    public class Singleton<T> // where T //: class, new()
    {
        #region Singleton Instnace of T
        private static T mInstance;
        private static object syncRoot = new Object();
        private static int nCount = 0;
        #endregion

        public static T Inst
        {
            get
            {
                if (mInstance == null)
                {
                    lock (syncRoot)
                    {
                        nCount++;
                        mInstance = (T)Activator.CreateInstance(typeof(T));//  T();                     
                    }
                }
                return mInstance;
            }
        }       
        public static T Create(dynamic v = null)
        {
            lock (syncRoot)
            {
                if (mInstance == null)
                {
                    lock (syncRoot)
                    {
                        nCount++;
                        mInstance = Reflect.AssemblyExtension.CreateObject(typeof(T), v);
                    }
                }
            }
            return mInstance;
        }
    }
}
