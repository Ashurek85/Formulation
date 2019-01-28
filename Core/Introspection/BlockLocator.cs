using Core.Blocks;
using Core.Blocks.Elementals;
using Core.Introspection.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Introspection
{
    public static class BlockLocator
    {
        public static IEnumerable<Type> GetBlockImplementations()
        {
            List<Type> typedImplementations = new List<Type>();
            // Type (Abstract or Interface) and possible implementations
            Dictionary<Type, List<Type>> typesImplementations = new Dictionary<Type, List<Type>>();

            // Get types to be processed
            IEnumerable<Type> objetiveTypes = typeof(BlockLocator).Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(BaseBlock)) && !t.IsAbstract);
            foreach (Type type in objetiveTypes)
            {
                if (((System.Reflection.TypeInfo)type).GenericTypeParameters.Length > 0)
                {
                    // If there are generic parameters, the types must be resolved
                    typedImplementations.AddRange(ParameterizeType(type, typesImplementations));
                }
                else
                    typedImplementations.Add(type);
            }

            return typedImplementations;
        }

        private static IEnumerable<Type> ParameterizeType(Type type, Dictionary<Type, List<Type>> typesImplementations)
        {
            List<Type> possibleTypes = new List<Type>();

            List<SerializationValuesAttribute> attributes = type.GetCustomAttributes(typeof(SerializationValuesAttribute), false)
                                                                .Cast<SerializationValuesAttribute>().ToList();

            List<List<Type>> groupingTypes = new List<List<Type>>();

            foreach (Type parameterType in ((System.Reflection.TypeInfo)type).GenericTypeParameters)
            {
                SerializationValuesAttribute attribute = attributes.FirstOrDefault(a => a.GenericParameterName == parameterType.Name);
                if (attribute == null)
                    throw new Exception($"The parameter {parameterType.Name} of type {type.FullName} does not have its corresponding class attribute for serialization: {typeof(SerializationValuesAttribute).FullName}");

                // Implementation for current parameter
                List<Type> currentTypeImplementations = new List<Type>();

                foreach(Type possibleType in attribute.PossibleTypes)
                {
                    if (typesImplementations.Keys.Contains(possibleType))
                    {
                        // Cache
                        currentTypeImplementations.AddRange(typesImplementations[possibleType]);
                    }
                    else
                    {
                        if (possibleType.IsClass && possibleType.IsAbstract)
                        {
                            List<Type> inheritedTypes = typeof(BaseFormula).Assembly.GetTypes()
                                                                                  .Where(t => t.IsSubclassOf(possibleType) && !t.IsAbstract && t.IsClass).ToList();
                            // Add to cache
                            typesImplementations.Add(possibleType, inheritedTypes);
                            // Add to implementations
                            currentTypeImplementations.AddRange(inheritedTypes.Except(currentTypeImplementations));                            
                        }
                        else if (possibleType.IsInterface)
                        {
                            List<Type> interfaceTypes = typeof(BaseFormula).Assembly.GetTypes()
                                                                                    .Where(t => possibleType.IsAssignableFrom(t) && !t.IsAbstract && t.IsClass).ToList();
                            // Add to cache
                            typesImplementations.Add(possibleType, interfaceTypes);
                            // Add to implementations
                            currentTypeImplementations.AddRange(interfaceTypes.Except(currentTypeImplementations));
                        }
                        else
                        {
                            if (!currentTypeImplementations.Contains(possibleType))
                                currentTypeImplementations.Add(possibleType);
                        }
                    }
                }

                groupingTypes.Add(currentTypeImplementations);
            }

            IEnumerable<IEnumerable<Type>> combinations = groupingTypes.CartesianProduct();

            foreach (IEnumerable<Type> parameterTypes in combinations)
            {
                try
                {
                    possibleTypes.Add(type.MakeGenericType(parameterTypes.ToArray()));
                }
                catch (ArgumentException)
                {
                    // Not all combinations have to be valid due to the internal restrictions of type
                    // Ignore situation
                }
            }

            return possibleTypes;
        }

        public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<IEnumerable<T>> sequences)
        {
            IEnumerable<IEnumerable<T>> emptyProduct = new[] { Enumerable.Empty<T>() };
            return sequences.Aggregate(emptyProduct,
                                       (accumulator, sequence) =>
                                       from acc in accumulator
                                       from item in sequence
                                       select acc.Concat(new[] { item }));
        }
    }
}
