using System;
using System.Dynamic;

namespace D.Net
{
    public class DNetBaseObject : DynamicObject,IDisposable
    {
        public string Name { get; set; } = "NoName";
        public Guid Uid { get; set; } = Guid.NewGuid();
        public Int64 NumID { get; set; } = -1;
        public string Desc { get; set; } = string.Empty;
        public DNetBaseObject(string objName, string desc = "")
        {
            Name = objName;
            Desc = desc;
        }
        public DNetBaseObject() : this("NoName", "")
        {

        }
        public virtual void Dispose()
        {
            
        }

    }
}
