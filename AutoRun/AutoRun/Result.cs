using System;
using System.Reflection;

namespace AutoRun
{
    public class Result
    {
        public Type ClassType { get; private set; }
        public MethodInfo Method { get; private set; }

        public Result(Type classType, MethodInfo method)
        {
            ClassType = classType;
            Method = method;
        }

        public sealed override string ToString()
        {
            return string.Format("Classe: {0}, Método: {1}", ClassType.Name, Method.Name);
        }
    }
}