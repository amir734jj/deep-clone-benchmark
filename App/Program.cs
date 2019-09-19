using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using AutoFixture;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var fixture = new Fixture();

            var data = fixture.CreateMany<Model>(50000).ToList();

            Benchmark("BenchmarkBinaryDeepCloneSingle", () => BenchmarkBinaryDeepCloneSingle(data));
            
            Benchmark("BenchmarkBinaryDeepCloneMany", () => BenchmarkBinaryDeepCloneMany(data));
            
            Benchmark("BenchmarkClone", () => BenchmarkClone(data));
        }
        
        private static void Benchmark(string description, Action action)
        {
            var sw = new Stopwatch();
            
            sw.Start();

            action();
            
            sw.Stop();
            
            Console.WriteLine($"{description}: {sw.Elapsed.Seconds} seconds");
        }

        private static IReadOnlyList<Model> BenchmarkBinaryDeepCloneSingle(IReadOnlyList<Model> list)
        {
            return list.Select(x =>
            {
                using (var ms = new MemoryStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(ms, x);
                    ms.Position = 0;

                    return (Model) formatter.Deserialize(ms);
                }
            }).ToList();
        }
        
        private static IReadOnlyList<Model> BenchmarkBinaryDeepCloneMany(IReadOnlyList<Model> list)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, list);
                ms.Position = 0;

                return (IReadOnlyList<Model>) formatter.Deserialize(ms);
            }
        }
        
        private static IReadOnlyList<Model> BenchmarkClone(IReadOnlyList<Model> list)
        {
            return list.Select(x => x.Clone()).Cast<Model>().ToList();
        }
    }
}