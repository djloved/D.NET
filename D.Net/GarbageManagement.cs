using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace D.Net
{
    public static class GarbageManagement
    {
        public static void Release(object o)
        {
            if (o != null)
            {
                Marshal.FinalReleaseComObject(o);
            }
        }
        public static void ReleaseCOMObj(this object o)
        {
            if (o != null)
            {
                Marshal.FinalReleaseComObject(o);
            }
        }
        public static void CollectGarbage()
        {
            try
            {
                GC.GetTotalMemory(false);
                GC.Collect();
                GC.GetTotalMemory(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
