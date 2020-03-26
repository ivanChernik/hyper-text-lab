using System;
using System.Windows.Controls;
using System.Windows.Documents;
using AsketHypertext.Models;
using AsketHypertext.Utils;

namespace AsketHypertext.Views
{
    public class AsketLinkView : Label
    {
        public AsketLinkView(AsketLink model, Action<string> navigateAction)
        {
            Name = model.Id;
            var hyperlink = new Hyperlink();
            hyperlink.Inlines.Add(model.Text);
            hyperlink.Command = new Command(() =>
            {
                navigateAction(model.Url);
            });
            Content = hyperlink;
        }
    }
}
