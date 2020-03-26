using System;
using System.Collections.Generic;
using System.Linq;

namespace AsketHypertext.Models
{
    public abstract class AsketElement
    {
        protected AsketElement(string type)
        {
            Type = type;
        }

        public string Type { get; }

        public string Id { get; set; }

        protected virtual IEnumerable<PropertyInfo> GetCustomProperties()
        {
            return Enumerable.Empty<PropertyInfo>();
        }

        public IEnumerable<PropertyInfo> GetProperties()
        {
            var customProperties = GetCustomProperties();
            return customProperties.Concat(new[] {new PropertyInfo
            {
                PropertyName = "id",
                Setter = value => Id = value
            }});
        }
    }

    public class PropertyInfo
    {
        public string PropertyName { get; set; }

        public Action<string> Setter { get; set; }
    }
}
