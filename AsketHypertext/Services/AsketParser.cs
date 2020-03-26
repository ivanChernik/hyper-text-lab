using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AsketHypertext.Exceptions;
using AsketHypertext.Models;

namespace AsketHypertext.Services
{
    public class AsketParser : IAsketParser
    {
        private readonly List<AsketElement> allAsketElements;

        public AsketParser()
        {
            allAsketElements = GetEnumerableOfType<AsketElement>().ToList();
        }

        public AsketPage Parse(string[] asketPageLines)
        {
            var page = new AsketPage();
            Assert(asketPageLines[0].TrimEnd() == "$asket");
            Assert(asketPageLines[1].StartsWith("  $info "));
            var infoProperties = ReadProperties(string.Concat(asketPageLines[1].Skip(8)));
            page.Name = infoProperties["name"];
            page.Path = infoProperties["path"];
            Assert(string.Concat(asketPageLines[2].Take(10)).TrimEnd() == "  $content");
            page.Content = new List<AsketElement>();
            for (int i = 3; i < asketPageLines.Length;)
            {
                Assert(asketPageLines[i].StartsWith("    "));
                string elementType = string.Concat(asketPageLines[i].Skip(5).TakeWhile(x => x != ' '));
                var element = (AsketElement)Activator.CreateInstance(allAsketElements
                    .Single(x => x.Type == elementType).GetType());
                if (asketPageLines[i].Trim().Length > elementType.Length)
                {
                    var properties = ReadProperties(string.Concat(asketPageLines[i].TrimStart().Skip(elementType.Length + 2)));
                    var elementProperties = element.GetProperties().ToList();
                    foreach (var property in properties)
                    {
                        elementProperties.Single(x => x.PropertyName == property.Key).Setter(property.Value);
                    }
                }

                if (element is AsketList asketList)
                {
                    var itemsLines = asketPageLines.Skip(i + 1).TakeWhile(x => x.StartsWith("      $item")).ToList();
                    asketList.Children = itemsLines.Select(x =>
                    {
                        var item = new AsketItem();
                        var itemProperties = ReadProperties(string.Concat(x.Skip(12)));
                        var elementProperties = item.GetProperties().ToList();
                        foreach (var property in itemProperties)
                        {
                            elementProperties.Single(y => y.PropertyName == property.Key).Setter(property.Value);
                        }
                        
                        return item;
                    });

                    i += itemsLines.Count;
                }

                page.Content.Add(element);
                i++;
            }

            return page;
        }

        private Dictionary<string, string> ReadProperties(string line)
        {
            var propertiesDict = new Dictionary<string, string>();
            while (line.Any())
            {
                string newPropertyName = string.Concat(line.TakeWhile(x => x != '='));
                line = string.Concat(line.Skip(newPropertyName.Length));
                Assert(string.Concat(line.Take(2))  == "=\"");
                line = string.Concat(line.Skip(2));
                string newPropertyValue = (string.Concat(line.TakeWhile(x => x != '"')));
                line = string.Concat(line.Skip(newPropertyValue.Length));
                Assert(line.First() == '"');
                line = string.Concat(line.Skip(1));
                line = line.TrimStart();
                propertiesDict.Add(newPropertyName, newPropertyValue);
            }
            
            return propertiesDict;
        }
        
        private void Assert(bool condition)
        {
            if (!condition)
            {
                throw new AsketFormatException();
            }
        }

        private static IEnumerable<T> GetEnumerableOfType<T>(params object[] constructorArgs) where T : class
        {
            return Assembly.GetAssembly(typeof(T)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T)))
                .Select(type => (T) Activator.CreateInstance(type, constructorArgs));
        }
    }
}
