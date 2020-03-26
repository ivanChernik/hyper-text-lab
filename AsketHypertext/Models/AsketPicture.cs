using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsketHypertext.Models
{
    public class AsketPicture : AsketElement
    {
        public AsketPicture() : base("picture")
        {
        }

        public string Source { get; set; }

        protected override IEnumerable<PropertyInfo> GetCustomProperties()
        {
            yield return new PropertyInfo
            {
                PropertyName = "source",
                Setter = value => Source = value
            };
        }
    }
}
