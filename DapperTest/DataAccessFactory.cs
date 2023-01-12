using System;
using System.Linq;
using System.Reflection;

// static class DataAccessFactory {
//     internal static Type scmContextType = null;
//     internal static Type ScmContextType {
//         get { return scmContextType; }
//         set {

//         if (!value.GetTypeInfo().ImplementedInterfaces.
//         Contains(typeof(IScmContext))) {
//             throw new ArgumentException(
//             $"{value.GetTypeInfo().FullName} doesn't implement IScmContext");
//             }
//         scmContextType = value;
//         }
//     }
//     internal static IScmContext GetScmContext() {
//         if (scmContextType == null) {
//         throw new ArgumentNullException("ScmContextType not set");
//         }
//         return Activator.CreateInstance(scmContextType)
//         as IScmContext;
//     }
// }

using System.Collections.Generics;

static class DataAccessFactory {
    internal static Dictionary<Type, object> implementations =
    new Dictionary<Type, object>();
    internal static void AddImplementation<T>(T t) where T : class {
    implementations.Add(typeof(T), t);
    }
    internal static T GetImplementation<T>() where T : class {
        return implementations[typeof(T)] as T;
    }
}