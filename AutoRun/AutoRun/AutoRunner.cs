using System;
using System.Collections.Generic;
using System.Reflection;

namespace AutoRun
{
    public static class AutoRunner
    {
        public static List<Result> Run()
        {
            Assembly assembly = Assembly.GetCallingAssembly();

            List<Result> results = new List<Result>();

            Type[] types = assembly.GetTypes();

            foreach (Type type in types)
            {
                if (type.GetCustomAttributes<RunClassAttribute>() != null)
                {
                    MethodInfo[] methods = type.GetMethods();

                    foreach (MethodInfo method in methods)
                    {
                        if (method.GetCustomAttribute<RunMethodAttribute>() != null && method.IsStatic)
                        {
                            method.Invoke(null,null);
                            results.Add(new Result(type,method));
                        } 
                    }
                }
            }

            return results;
        }
    }
}