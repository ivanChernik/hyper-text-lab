using System.Windows;
using System.Windows.Controls;
using AsketHypertext.Models;

namespace AsketHypertext.Views
{
    public class AsketLabelView : TextBlock
    {
        public AsketLabelView(AsketLabel model)
        {
            Name = model.Id;
            Text = model.Value;
            TextWrapping = TextWrapping.WrapWithOverflow;
        }
    }
}
