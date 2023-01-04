// using System;
// using System.IO;
// using System.Linq;

// namespace CsvParser
// {
//     public class Program
//     {
//         public static void Main(string[] args)
//         {
//             var reader = new StreamReader(new FileStream("Marvel.csv", FileModle.Open));
//             var csvReader = new CsvReader(reader);
//             foreach (var line in csvReader.Lines)
//             {
//                 Console.WriteLine(
//                     line.First(p => p.Key == "Title").Value);
//             }
//         }
//     }
// }
