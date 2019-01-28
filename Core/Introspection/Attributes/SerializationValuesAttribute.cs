using System;

namespace Core.Introspection.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SerializationValuesAttribute : Attribute
    {
        public string GenericParameterName { get; set; }
        public Type[] PossibleTypes { get; set; }

        public SerializationValuesAttribute(string genericParameterName, Type[] possibleTypes)
        {
            GenericParameterName = genericParameterName;
            PossibleTypes = possibleTypes;
        }
    }
}
