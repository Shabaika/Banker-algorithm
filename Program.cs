using System;
using System.Collections.Generic;

namespace lab4
{
    class Program
    {
        public struct Process
        {
            public string name;
            public List<int> useResources;
            public List<int> maxResources;

            public Process(string name, List<int> useResources, List<int> maxResources)
            {
                this.name = name;
                this.useResources = useResources;
                this.maxResources = maxResources;
            }
        }

        public static List<int> FreeResources(List<Process> process, List<int> freeResources, int k)
        {
            for(int i=0; i < freeResources.Count; i++)
            {
                freeResources[i] += process[k].useResources[i];
            }
            return freeResources;
        }

        static int Main(string[] args)
        {
            List<Process> process = new List<Process>();
            process.Add(new Process("A", new List<int>{2, 0, 0, 0}, new List<int> {2, 0, 2, 2}));
            process.Add(new Process("B", new List<int> {2, 2, 0, 0 }, new List<int> {2, 3, 2, 2}));
            process.Add(new Process("C", new List<int> {0, 2, 2, 0 }, new List<int> {2, 4, 2, 4}));
            process.Add(new Process("D", new List<int> {0, 0, 2, 2 }, new List<int> {1, 0, 2, 4}));

            Console.WriteLine("Процесс Предоставлено ресурсов Максимальная потребность");
            Console.WriteLine("             R1 R2 R3 R4             R1 R2 R3 R4       ");
            for (int i=0; i<process.Count; i++)
            {
                Console.Write(process[i].name + "            ");
                for (int j=0; j<process[i].useResources.Count; j++)
                {
                    Console.Write(process[i].useResources[j]+ "  ");
                }
                Console.Write("            ");
                for (int j = 0; j < process[i].maxResources.Count; j++)
                {
                    Console.Write(process[i].maxResources[j] + "  ");
                }
                Console.WriteLine();
            }

            List<int> freeResources = new List<int> {4, 4, 4, 4};
            List<string> query = new List<string>();

            int c = process.Count;

            bool check = false;
            for (int i = 0; i < process.Count; i++)
            {
                for (int j = 0; j < process[i].useResources.Count; j++)
                {
                    freeResources[j] -= process[i].useResources[j];
                    if (freeResources[j] < 0)
                    {
                        Console.Write("Использовано больше ресурсов, чем может быть. Опасное состояние.");
                        check = true;
                    }
                    if (check) return 0;
                }
            }

            Console.Write(Environment.NewLine+ "Свободных ресурсов: ");
            for (int j = 0; j < freeResources.Count; j++)
            {
                 Console.Write(freeResources[j]+" ");
            }
            Console.WriteLine();

            check = false;
            bool equalSum = true;
            for(int i = 0; i < process.Count; i++)
            {
                for (int j = 0; j < process[i].useResources.Count; j++)
                {
                    equalSum = true;
                    if (process[i].useResources[j] + freeResources[j] < process[i].maxResources[j])
                    {
                        equalSum = false;
                        break;
                    }
                   
                }
                if (equalSum)
                {
                    query.Add(process[i].name);
                    freeResources = FreeResources(process, freeResources, i);
                    process.RemoveAt(i);
                    i = -1;
                }
            }

            if(query.Count==c) Console.WriteLine(Environment.NewLine + "Состояние безопасное");
            else Console.WriteLine(Environment.NewLine + "Состояние опасное");

            if (query.Count == 0) query.Add("Z");

            Console.Write("Последовательность выполнения процессов:");
            for (int j = 0; j < query.Count; j++)
            {
                Console.Write(query[j]);
            }

            return 0;
        }
    }
}
