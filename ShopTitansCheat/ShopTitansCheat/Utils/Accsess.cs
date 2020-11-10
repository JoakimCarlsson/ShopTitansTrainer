using System;
using System.Reflection;

namespace ShopTitansCheat.Utils
{
    internal static class Access
    {
        internal static T GetPrivateField<T>(this object obj, string name)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = obj.GetType();
            FieldInfo field = type.GetField(name, flags);
            return (T)field.GetValue(obj);
        }
        internal static void SetPrivateField(this object obj, string name, object value)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = obj.GetType();
            FieldInfo field = type.GetField(name, flags);
            field.SetValue(obj, value);
        }
        internal static void CallPrivateMethod(this object obj, string name, params object[] param)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = obj.GetType();
            MethodInfo method = type.GetMethod(name, flags);
            method.Invoke(obj, param);
        }

        public static T GetPrivateFieldTest<T>(object obj, string propertyName)
        {
            return (T)obj.GetType()
                .GetField(propertyName, BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(obj);
        }
    }
}
