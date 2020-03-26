using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AsketHypertext.Models;

namespace AsketHypertext.Views
{
    public class AsketListView : StackPanel
    {
        public AsketListView(AsketList model)
        {
            Name = model.Id;
            Margin = new Thickness(10, 0, 0, 0);
            var items = model.Children.ToList();
            for(int i = 0; i < items.Count; i++)
            {
                Children.Add(new Label {Content = $"{i + 1}. {items[i].Value}"});
            }
        }
    }
}
