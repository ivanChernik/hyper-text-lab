using System.Collections.Generic;

namespace AsketHypertext.Models
{
    public class AsketItem : AsketElement
    {
        public AsketItem() : base("item")
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
