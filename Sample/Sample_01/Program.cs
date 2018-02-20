using D.Net;
using D.Net.Log;
using System;

namespace Sample_01
{
    class Program
    {
        static void Main(string[] args)
        {
            LogFac fac = new LogFac();
            fac.CreateFactoryItemInstance("SimpleTxt", typeof(SimpleTextLog), new { logDir = "aaDir", _logPrefix = "preFix", logerName = "SimpleText" });
            fac.CreateFactoryItemInstance("Log4Net", typeof(Log4Net), new { });
            fac.FactoryItems["Log4Net"].doLog("DD", Logger.LoggingLevel.Error, "", "", 1, null);
            dynamic dyFac = fac;
            
            //dyFac.doLog("DD", Logger.LoggingLevel.Error, "", "", 1, null);
            Singleton<SimpleTextLog>.Create(new { logDir = "aaDir", _logPrefix = "preFix", logerName = "SimpleText" });
            
        }
    }
}