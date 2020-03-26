using System.Collections.Generic;

namespace AsketHypertext.Models
{
    public class AsketPage
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public List<AsketElement> Content { get; set; }
    }
}
