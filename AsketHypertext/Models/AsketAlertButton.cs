using System.Collections.Generic;

namespace AsketHypertext.Models
{
    public class AsketAlertButton : AsketElement
    {
        public AsketAlertButton() : base("alert-button")
        {
        }

        public string Text { get; set; }

        public string Message { get; set; }

        protected override IEnumerable<PropertyInfo> GetCustomProperties()
        {
            yield return new PropertyInfo
            {
                PropertyName = "text",
                Setter = value => Text = value
            };
            yield return new PropertyInfo
            {
                PropertyName = "message",
                Setter = value => Message = value
            };
        }
    }
}
