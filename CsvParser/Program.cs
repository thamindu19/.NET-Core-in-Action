﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CsvParser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // var sr = new StreamReader(new FileStream("Marvel.csv", FileMode.Open));
            // var csvReader = new CsvReader(sr);
            // foreach (var line in csvReader.Lines)
            //     Console.WriteLine(line.First(p => p.Key == "Title").Value);
            using (var sr = new StreamReader(new FileStream("Marvel.csv", FileMode.Open))) {
                var csvReader = new CsvReader(sr);
                foreach (var line in csvReader.Lines)
                    Console.WriteLine(line.First(p => p.Key == "Title").Value);
            }
        }
    }
}
