using Api.Entities;

namespace Api.Helpers
{
    public static class HasProperty
    {

        public static bool Check<T>(string checkProperty, out string? propertyName)
        {
            propertyName = typeof(T).GetProperties()
                .FirstOrDefault(s => s.Name.ToLower()
                .Equals(checkProperty.Trim().ToLower(), StringComparison.OrdinalIgnoreCase))?.Name;

            return propertyName != null;
        }



        //public static bool Check<T>(string checkProperty, out string? propertyName)
        //{
        //    var formatted = checkProperty.Trim().ToLower();

        //    var properties = typeof(T).GetProperties();

        //    string? propName = string.Empty;

        //    foreach(var prop in properties)
        //    {
        //        if (prop.Name.ToLower().Equals(formatted, StringComparison.OrdinalIgnoreCase))
        //        {
        //            propName = prop.Name;
        //            propertyName = propName;
        //            return true;
        //        }      
        //    }

        //    propertyName = propName;

        //    return false;
        //}
    }
}
