using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AsketHypertext.Models;
using AsketHypertext.Utils.MyNamespace;

namespace AsketHypertext.Views
{
    public class AsketPageView : Page
    {
        private readonly Action<AsketPageView> onNavigating;
        private readonly StackPanel stack;
        private readonly ScrollViewer scrollViewer;

        public AsketPageView(AsketPage model, Action<AsketPageView> onNavigating)
        {
            this.onNavigating = onNavigating;
            Path = model.Path;
            stack = new StackPanel
            {
                Margin = new Thickness(10)
            };
            stack.Children.Add(new Label
            {
                Content = model.Name,
                FontWeight = FontWeights.Bold,
                FontSize = 16,
                HorizontalAlignment = HorizontalAlignment.Center
            });
            foreach (var asketElement in model.Content)
            {
                UIElement elementView = null;
                if (asketElement is AsketLabel labelModel)
                {
                    elementView = new AsketLabelView(labelModel);
                }
                else if (asketElement is AsketLink linkModel)
                {
                    elementView = new AsketLinkView(linkModel, NavigateAction);
                }
                else if (asketElement is AsketPicture pictureModel)
                {
                    elementView = new AsketPictureView(pictureModel);
                }
                else if (asketElement is AsketList listModel)
                {
                    elementView = new AsketListView(listModel);
                }

                if (elementView != null)
                {
                    stack.Children.Add(elementView);
                }
            }
            scrollViewer = new ScrollViewer { Content = stack };
            Content = scrollViewer;
        }

        public string Path { get; }

        public List<AsketPageView> AllPages { get; set; }

        private void NavigateAction(string url)
        {
            if (url.StartsWith("#"))
            {
                var element = VisualTreeHelpers.FindChild<UIElement>(stack, string.Concat(url.Skip(1)));
                scrollViewer.ScrollToVerticalOffset(element.TranslatePoint(new Point(0, 0), stack).Y);
            }
            else
            {
                var nextPage = AllPages.Single(x => x.Path == url);
                onNavigating(nextPage);
                NavigationService?.Navigate(nextPage);
            }
        }
    }
}
