using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using D.Net.Collection;

namespace D.Net.Reflect
{
    public static class AssemblyExtension
    {
        public static object RunMethod(this Type t, string strMethod, object objInstance, object[] aobjParams, BindingFlags eFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        {
            MethodInfo m;
            try
            {
                m = t.GetMethod(strMethod, eFlags);
                ParameterInfo[] pInfo = m.GetParameters();
                if (m == null)
                {
                    throw new ArgumentException("There is no method '" + strMethod + "' for type '" + t.ToString() + "'.");
                }
                object objRet = m.Invoke(objInstance, aobjParams);
                return objRet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } //end of method 
        public static bool IsCompatible(this ParameterInfo[] pis, object[] parameters)
        {
            try
            {
                var mandatoryItems = from o in pis
                                     where o.IsOptional == false
                                     select o;
                if (mandatoryItems.Count() > parameters.Count())
                    return false;
                for (int i = 0; i < pis.Count(); i++)
                {
                    object targetParam = null;

                    if (parameters.Count() > i)
                        targetParam = parameters[i];                 
                    else
                    {
                        if (pis[i].HasDefaultValue)
                        {
                            targetParam = pis[i].DefaultValue;
                        }
                    }
                    if (targetParam == null)
                        return false;

                    if (! pis[i].ParameterType.IsAssignableFrom(targetParam.GetType()) )
                        return false;
                }
                return true;

            }
            catch (Exception ex)
            {

                
            }
            return false;
        }
        
        public static bool IsCompatibleWithDynamic(this ParameterInfo[] pis, Dictionary<string, object> parameters =null)
        {
            try
            {
//                IDictionary<string, object> dic = parameters.ToDictionary();

                var mandatoryItems = from o in pis  where o.IsOptional == false select o;
                if (mandatoryItems.Count() ==0 && ( parameters==null|| parameters.Values.Count() ==0))
                {
                    return true;
                }
                foreach(ParameterInfo itm in mandatoryItems)
                {
                    if (itm.HasDefaultValue)
                        continue;
                    if (itm.IsOptional)
                        continue;
                    if (!parameters.ContainsKey(itm.Name))
                        return false;
                    if (!itm.ParameterType.IsAssignableFrom(parameters[itm.Name].GetType() ))
                        return false;
                }              
                return true;

            }
            catch (Exception ex)
            {


            }
            return false;
        }
        public static object[] FillParameters(this ParameterInfo[] pis , Dictionary<string,object> paramDic)
        {
            List<object> values = new List<object>();
            var infos = from o in pis
                        orderby o.Position
                        select o;
            foreach(ParameterInfo info in infos)
            {
                object passedValue = null;
                if (paramDic.ContainsKey(info.Name))
                    passedValue = paramDic[info.Name];
                if (passedValue == null && info.HasDefaultValue)
                    passedValue = info.DefaultValue;
                values.Add(passedValue);
            }

            return values.ToArray();
        }
        //public static object CreateObject(Type type, object parameters, BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
        //{
        //    return type.CreateObject(parameters, flags);
        //}
        public static object CreateObject(this Type type, object parameters, BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
        {
            Dictionary<string, object> dicParam = parameters.ToDictionary();
            foreach (var ctor in type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                try
                {
                    ParameterInfo[] pi = ctor.GetParameters();
                    if (pi.IsCompatibleWithDynamic(dicParam))
                    {
                        return ctor.Invoke(pi.FillParameters(dicParam));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return null;
        }
        ////public static dynamic Compile(this SyntaxTree[] sources, string assemblyFileName = null, string[] references = null, Stream compiledStream = null, Action<object> logHandler = null)
        //{
        //    EmitResult emitResult = null;
        //    try
        //    {
        //        if (assemblyFileName == null) assemblyFileName = "gen" + Guid.NewGuid().ToString().Replace("-", "");// + ".dll";
        //        var assemblyPath = Path.GetDirectoryName(typeof(object).Assembly.Location);
        //        List<MetadataReference> refList = new List<MetadataReference>();
        //        refList.Add(MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "mscorlib.dll")));
        //        refList.Add(MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.dll")));
        //        refList.Add(MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Core.dll")));
        //        refList.Add(MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "Microsoft.CSharp.dll")));

        //        if (references != null)
        //            foreach (var f in references)
        //            {
        //                refList.Add(MetadataReference.CreateFromFile(f));
        //            }

        //        var compilation = CSharpCompilation.Create(assemblyFileName,
        //            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary),
        //            syntaxTrees: sources,
        //            references: refList.ToArray());

        //        emitResult = compilation.Emit(compiledStream);
        //        logHandler?.Invoke(emitResult);
        //        //loadedAssembly = Assembly.Load(ms.ToArray());
        //    }
        //    catch (Exception ex)
        //    {
        //        logHandler?.Invoke(ex);
        //    }
        //    return new { emitResult = emitResult };
        //}

        //public static SyntaxTree[] ToSyntaxTrees(this string[] sources)
        //{
        //    return (from source in sources select CSharpSyntaxTree.ParseText(source)).ToArray();
        //}
        //public static SyntaxTree[] FileToSyntaxTress(this string[] sources)
        //{
        //    return (from source in sources select CSharpSyntaxTree.ParseText(File.ReadAllText(source), path: source)).ToArray();
        //}
    }
}
