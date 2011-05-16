using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace IntelliRoom
{
    public static class Reflection
    {
        public static object Invoke(MethodInfo method, string[] parametres)
        {
            //creamos una instancia del objeto
            var instance = Activator.CreateInstance(method.ReflectedType);
            
            ParameterInfo[] parametresMethod = method.GetParameters();
            object[] parametresObj = new object[parametresMethod.Length];

            for (int i = 0; i < parametresMethod.Length; i++)
			{
                parametresObj[i] = Cast(parametres[i], parametresMethod[i].ParameterType);
			}            

            var result = method.Invoke(instance, parametresObj);

            return result;
        }

        public static object Cast(object value, Type type)
        {
            return Convert.ChangeType(value, type);
        }

        public static MethodInfo[] SearchMethod(String command)
        {
            MethodInfo[] res = GetAllMethods().Where(x => x.Name.ToLower() == command.ToLower()).ToArray<MethodInfo>();
            if (res.Length == 0)
            {
                return null;
            }
            else
            {
                return res;
            }
        }

        public static MethodInfo[] GetAllMethods()
        {
            List<MethodInfo> methods = new List<MethodInfo>();

            methods.AddRange(new Command().GetType().GetMethods());

            methods.RemoveAll(x => x.Name == "GetType" || x.Name == "GetHashCode" || x.Name == "Equals" || x.Name == "ToString" || x.Name == "Init");

            return methods.ToArray<MethodInfo>();
        }
    }
}
