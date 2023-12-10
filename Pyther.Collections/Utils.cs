using System.Reflection;

namespace Pyther.Collections;

internal static class Utils
{
    internal static T GetStaticPropertyValue<T>(string name)
    {
        FieldInfo field = typeof(T).GetField(name, BindingFlags.Static | BindingFlags.Public) ?? throw new InvalidOperationException($"Field {name} not found!");
        return (T)field!.GetValue(null)!;
    }
}