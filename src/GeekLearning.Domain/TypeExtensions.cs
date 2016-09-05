namespace GeekLearning.Domain
{
    using System;
    using System.Reflection;

    public static class TypeExtensions
    {
        public static bool IsSubclassOfRawGeneric(this Type typeToCheck, Type genericType)
        {
            while (typeToCheck != null && typeToCheck != typeof(object))
            {
                var current = typeToCheck.IsConstructedGenericType ? typeToCheck.GetGenericTypeDefinition() : typeToCheck;
                if (genericType == current)
                {
                    return true;
                }

                typeToCheck = typeToCheck.GetTypeInfo().BaseType;
            }

            return false;
        }
    }

}
