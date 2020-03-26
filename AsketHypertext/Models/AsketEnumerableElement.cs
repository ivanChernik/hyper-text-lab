using System.Collections.Generic;

namespace AsketHypertext.Models
{
    public abstract class AsketEnumerableElement<T> : AsketElement
    {
        protected AsketEnumerableElement(string type) : base(type)
        {
        }

        public IEnumerable<T> Children { get; set; }
    }
}
