using System;
using System.Reflection;

namespace SaiyanTails.Utils {
    internal static class ReflectionHelpers {
        internal const BindingFlags ALL = (BindingFlags)(-1);

        internal static T Invoke<T>(this object obj, string methodName, Type[] types, params object[] args) => (T)obj.GetType().GetMethod(methodName, ALL, types).Invoke(obj, args);

        internal static T Invoke<T>(this object obj, string methodName, params object[] args) => (T)obj.GetType().GetMethod(methodName, ALL).Invoke(obj, args);

        internal static T Invoke<T>(this object obj, string methodName) => (T)obj.GetType().GetMethod(methodName, ALL).Invoke(obj, null);

        internal static void Invoke(this object obj, string methodName, Type[] types, params object[] args) => obj.GetType().GetMethod(methodName, ALL, types).Invoke(obj, args);

        internal static void Invoke(this object obj, string methodName, params object[] args) => obj.GetType().GetMethod(methodName, ALL).Invoke(obj, args);

        internal static void Invoke(this object obj, string methodName) => obj.GetType().GetMethod(methodName, ALL).Invoke(obj, null);

        internal static T Invoke<T>(this Type obj, string methodName, Type[] types, params object[] args) => (T)obj.GetMethod(methodName, ALL, types).Invoke(null, args);

        internal static T Invoke<T>(this Type obj, string methodName, params object[] args) => (T)obj.GetMethod(methodName, ALL).Invoke(null, args);

        internal static T Invoke<T>(this Type obj, string methodName) => (T)obj.GetMethod(methodName, ALL).Invoke(null, null);

        internal static void Invoke(this Type obj, string methodName, Type[] types, params object[] args) => obj.GetMethod(methodName, ALL, types).Invoke(null, args);

        internal static void Invoke(this Type obj, string methodName, params object[] args) => obj.GetMethod(methodName, ALL).Invoke(null, args);

        internal static void Invoke(this Type obj, string methodName) => obj.GetMethod(methodName, ALL).Invoke(null, null);

        internal static T Invoke<T>(this TypeInfo obj, string methodName, Type[] types, params object[] args) => (T)obj.AsType().GetMethod(methodName, ALL, types).Invoke(null, args);

        internal static T Invoke<T>(this TypeInfo obj, string methodName, params object[] args) => (T)obj.AsType().GetMethod(methodName, ALL).Invoke(null, args);

        internal static T Invoke<T>(this TypeInfo obj, string methodName) => (T)obj.AsType().GetMethod(methodName, ALL).Invoke(null, null);

        internal static void Invoke(this TypeInfo obj, string methodName, Type[] types, params object[] args) => obj.AsType().GetMethod(methodName, ALL, types).Invoke(null, args);

        internal static void Invoke(this TypeInfo obj, string methodName, params object[] args) => obj.AsType().GetMethod(methodName, ALL).Invoke(null, args);

        internal static void Invoke(this TypeInfo obj, string methodName) => obj.AsType().GetMethod(methodName, ALL).Invoke(null, null);
    }
}
