using System.Collections.Generic;

namespace AsketHypertext.Models
{
    public class AsketLabel : AsketElement
    {
        public AsketLabel() : base("label")
        {
        }

        public string Value { get; set; }

        protected override IEnumerable<PropertyInfo> GetCustomProperties()
        {
            yield return new PropertyInfo
            {
                PropertyName = "value",
                Setter = value => Value = value
            };
        }
    }
}
