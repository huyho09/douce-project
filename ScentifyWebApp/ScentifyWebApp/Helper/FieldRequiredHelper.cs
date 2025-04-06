using System.ComponentModel.DataAnnotations;

namespace ScentifyWebApp.Helper
{
    public static class FieldRequiredHelper
    {
        public static bool AreFieldsRequired(object obj)
        {
            var type = obj.GetType();

            foreach (var property in type.GetProperties())
            {
                // Check if the property has the Required attribute
                var requiredAttribute = (RequiredAttribute)Attribute.GetCustomAttribute(property, typeof(RequiredAttribute));

                if (requiredAttribute != null)
                {
                    var value = property.GetValue(obj);

                    // Check if the value is null or an empty string
                    if (value == null || (value is string && string.IsNullOrEmpty((string)value)))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
