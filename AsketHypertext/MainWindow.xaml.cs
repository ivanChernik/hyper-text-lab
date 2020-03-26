using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using AsketHypertext.Models;
using AsketHypertext.Services;
using AsketHypertext.Utils;
using AsketHypertext.Views;
using Autofac;

namespace AsketHypertext
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string[], AsketPageView> pagesDict;
        private List<AsketPage> pagesModels;

        public MainWindow()
        {
            InitializeComponent();
            LoadSource();
        }

        private void LoadSource()
        {
            var sourcesLoader = App.Container.Resolve<ISourcesLoader>();
            var sources = sourcesLoader.Load().ToList();
            var parser = App.Container.Resolve<IAsketParser>();
            pagesDict = new Dictionary<string[], AsketPageView>();
            pagesModels = new List<AsketPage>();
            foreach (var source in sources)
            {
                var model = parser.Parse(source);
                pagesModels.Add(model);
                var page = new AsketPageView(model, OnNavigatingTo);
                pagesDict.Add(source, page);
            }
            foreach (var page in pagesDict.Values)
            {
                page.AllPages = pagesDict.Values.ToList();
            }

            if (IsCyclic())
            {
                CyclicLabel.Content = "Cyclic";
                CyclicLabel.Foreground = Brushes.LimeGreen;
            }
            else
            {
                CyclicLabel.Content = "Not Cyclic";
                CyclicLabel.Foreground = Brushes.Red;
            }
            var firstPage = pagesDict.First();
            ContentFrame.Content = firstPage.Value;
            MarkupLabel.Text = string.Join(Environment.NewLine, firstPage.Key);
        }

        private bool IsCyclic()
        {
            var graph = new Graph(pagesModels.Count);
            for (int i = 0; i < pagesModels.Count; i++)
            {
                var linkedPages = GetLinkedPages(pagesModels[i]).ToList();
                if (!linkedPages.Any())
                {
                    continue;
                }

                foreach (var linkedPage in linkedPages)
                {
                    graph.AddEdge(i, pagesModels.IndexOf(linkedPage));
                }
            }

            return graph.IsCyclic();
        }

        private IEnumerable<AsketPage> GetLinkedPages(AsketPage page)
        {
            return page.Content
                .Where(x => x is AsketLink).Cast<AsketLink>()
                .Select(x => pagesModels.FirstOrDefault(y => y.Path == x.Url))
                .Where(x => x != null)
                .Distinct();
        }

        private void OnNavigatingTo(AsketPageView nextPage)
        {
            MarkupLabel.Text = string.Join(Environment.NewLine, pagesDict.First(x => x.Value.Equals(nextPage)).Key);
        }
    }
}
