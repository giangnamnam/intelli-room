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
                if (parametresObj[i] == null)
                {
                    return "Fallo en casteo";
                }
			}            

            var result = method.Invoke(instance, parametresObj);

            return result;
        }

        public static object Cast(object value, Type type)
        {
            try
            {
                return Convert.ChangeType(value, type);
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public static MethodInfo[] SearchMethod(String command)
        {
            MethodInfo[] res = GetMethods().Where(x => x.Name.ToLower() == command.ToLower()).ToArray<MethodInfo>();
            if (res.Length == 0)
            {
                return null;
            }
            else
            {
                return res;
            }
        }

        public static MethodInfo[] GetMethods()
        {
            List<MethodInfo> methods = new List<MethodInfo>();

            methods.AddRange(new Command().GetType().GetMethods());

            methods.RemoveAll(x => x.Name == "GetType" || x.Name == "GetHashCode" || x.Name == "Equals" || x.Name == "ToString" || x.Name == "Init");

            return methods.ToArray<MethodInfo>();
        }

        public static MethodInfo[] SearchSpeakMethod(String command)
        {
            MethodInfo[] res = GetSpeakMethods().Where(x => x.Name.ToLower() == command.ToLower()).ToArray<MethodInfo>();
            if (res.Length == 0)
            {
                return null;
            }
            else
            {
                return res;
            }
        }

        public static MethodInfo[] GetSpeakMethods()
        {
            List<MethodInfo> methods = new List<MethodInfo>();

            //los metodos de comando de voz mas los demas metodos
            methods.AddRange(new SpeakCommand().GetType().GetMethods());
            methods.AddRange(new Command().GetType().GetMethods());

            methods.RemoveAll(x => x.Name == "GetType" || x.Name == "GetHashCode" || x.Name == "Equals" || x.Name == "ToString" || x.Name == "Init");

            return methods.ToArray<MethodInfo>();
        }
    }
}
