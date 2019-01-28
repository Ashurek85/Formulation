using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Core.Introspection
{
    public static class ClassMetadataLocator
    {

        /// <summary>
        /// Gets information about a property of a type
        /// </summary>
        /// <param name="item"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static Type GetPropertyType(Type item, string propertyName)
        {
            PropertyInfo propertyInfo = item.GetProperty(propertyName);
            if (propertyInfo == null)
                throw new Exception($"GetPropertyType: The type {item.FullName} does not have a property named {propertyName}");
            return propertyInfo.PropertyType;
        }


        /// <summary>
        /// Look for a generic method in a type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="methodName"></param>
        /// <param name="typeArgs"></param>
        /// <param name="argTypes"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static MethodBase GetGenericMethod(Type type, string methodName, Type[] typeArgs, Type[] argTypes, BindingFlags flags)
        {
            int typeArity = typeArgs.Length;
            IEnumerable<MethodInfo> metodos = type.GetMethods().Where(m => m.Name == methodName && m.GetGenericArguments().Length == typeArity)
                                                               .Select(m => m.MakeGenericMethod(typeArgs));

            return Type.DefaultBinder.SelectMethod(flags, metodos.ToArray(), argTypes, null);
        }
    }
}
