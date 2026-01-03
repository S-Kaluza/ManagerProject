using System.Reflection;
using Application.Abstract;
using Microsoft.AspNetCore.Builder;

namespace ProjDependencyInjection.Extensions;

public static class WebApplicationExtension
{
    public static void RegisterEndpoints(this WebApplication webApplication)
    {
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        assemblies.ForEachImplementation<IEndpoints>(x =>
        {
            x.Register(webApplication);
        });
    }

    public static IEnumerable<Type> GetInterfaceImplementations(this Assembly assembly, Type interfaceType)
    {
        return assembly.GetTypes().Where(x => x != null && !x.IsAbstract && !x.IsInterface && interfaceType.IsAssignableFrom(x));
    }

    public static void ForEachImplementation<T>(this IEnumerable<Assembly> assemblies, Action<T> callback)
    {
        Type baseType = typeof(T);
        foreach (Assembly assembly in assemblies)
        {
            List<Type> implementations = assembly.GetInterfaceImplementations(baseType).ToList();
            foreach (Type implementation in implementations)
            {
                T? instance = (T?)Activator.CreateInstance(implementation);
                if (instance is not null)
                {
                    callback(instance);
                }
            }
        }
    }
}