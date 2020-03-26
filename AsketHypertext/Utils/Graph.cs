using System.Collections.Generic;

namespace AsketHypertext.Utils
{
    public class Graph
    {
        private readonly int size;
        private readonly LinkedList<int>[] adj;

        public Graph(int size)
        {
            this.size = size;
            adj = new LinkedList<int>[size];
            for (int i = 0; i < size; ++i)
            {
                adj[i] = new LinkedList<int>();
            }
        }

        public void AddEdge(int v, int w)
        {
            adj[v].AddFirst(w);
        }

        public bool IsCyclic()
        {
            var visited = new bool[size];
            var recStack = new bool[size];
            for (int i = 0; i < size; i++)
            {
                if (IsCyclicUtil(i, visited, recStack))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsCyclicUtil(int ver, bool[] visited, bool[] recStack)
        {
            visited[ver] = true;
            recStack[ver] = true;

            var lnk = adj[ver];

            foreach (var item in lnk)
            {
                if (visited[item] == false && IsCyclicUtil(item, visited, recStack)
                    || recStack[item])
                {
                    return true;
                }
            }

            recStack[ver] = false;
            return false;
        }
    }
}
