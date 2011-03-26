using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace IntelliRoom
{
    public static class Reflection
    {
        //metodo cast
        public static string Invoke(MethodInfo method, object[] parametres)
        {
            var instance = Activator.CreateInstance(method.ReflectedType);

            var result = method.Invoke(instance, parametres);
            if (result == null)
                return "No devuelve nada";
            else
                return result.ToString();
        }

        public static object Cast(string value, Type type)
        {
            return null;
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
            //modulo que carga metodos y tal (nueva clase??)
            List<MethodInfo> methods = new List<MethodInfo>();

            methods.AddRange(new Command().GetType().GetMethods());

            methods.RemoveAll(x => x.Name == "GetType" || x.Name == "GetHashCode" || x.Name == "Equals" || x.Name == "ToString");

            return methods.ToArray<MethodInfo>();
        }
    }
}
