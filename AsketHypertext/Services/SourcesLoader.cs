using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AsketHypertext.Services
{
    public class SourcesLoader : ISourcesLoader
    {
        private const string SourcesFolderName = "Sources";

        public IEnumerable<string[]> Load()
        {
            string projectPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (projectPath == null)
            {
                yield break;
            }

            var files = Directory.GetFiles(Path.Combine(projectPath, SourcesFolderName));
            foreach (string file in files)
            {
                yield return File.ReadAllLines(file);
            }
        }
    }
}
