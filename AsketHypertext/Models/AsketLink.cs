using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsketHypertext.Models
{
    public class AsketLink : AsketElement
    {
        public AsketLink() : base("link")
        {
        }

        public string Text { get; set; }

        public string Url { get; set; }

        protected override IEnumerable<PropertyInfo> GetCustomProperties()
        {
            yield return new PropertyInfo
            {
                PropertyName = "text",
                Setter = value => Text = value
            };
            yield return new PropertyInfo
            {
                PropertyName = "url",
                Setter = value => Url = value
            };
        }
    }
}
