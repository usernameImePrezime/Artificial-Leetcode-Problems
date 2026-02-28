using System;
using System.Collections.Generic;
using System.Linq;
namespace ArtColorOptimizer
{
    // brigtness of pigments
    public class Pigment
    {
        public string Name { get; set; }
        public int Luminance { get; set; }
        public string HexCode { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // available pigments
            List<Pigment> palette = new List<Pigment>
            {
                new Pigment { Name = "Deep Blue", Luminance = 45, HexCode = "#00002D" },
                new Pigment { Name = "Sunset Orange", Luminance = 120, HexCode = "#FF5F1F" },
                new Pigment { Name = "Cloud White", Luminance = 240, HexCode = "#F5F5F5" },
                new Pigment { Name = "Forest Green", Luminance = 65, HexCode = "#228B22" },
                new Pigment { Name = "Soft Peach", Luminance = 180, HexCode = "#FFDAB9" },
                new Pigment { Name = "Lemon Yellow", Luminance = 210, HexCode = "#FFF700" },
                new Pigment { Name = "Slate Gray", Luminance = 112, HexCode = "#708090" }
            };
            int targetLuminance = 500;
            Console.WriteLine("--- Digital Art: Palette Harmony Optimizer ---");
            Console.WriteLine($"Searching for 4 pigments with total luminance of: {targetLuminance}\n");
            var optimizer = new PaletteOptimizer();
            var matchedPalettes = optimizer.FindPerfectHarmony(palette, targetLuminance);
            if (matchedPalettes.Count > 0)
            {
                Console.WriteLine($"SUCCESS: Found {matchedPalettes.Count} balanced combinations:");
                foreach (var combination in matchedPalettes)
                {
                    Console.WriteLine($"Palette: {string.Join(" + ", combination)} = {targetLuminance}");
                }
            }
            else
            {
                Console.WriteLine("No perfect 4-color harmony found for this target.");
            }
        }
    }
    public class PaletteOptimizer
    {
        public List<List<int>> FindPerfectHarmony(List<Pigment> pigments, int target)
        {
            var results = new List<List<int>>();
            int[] values = pigments.Select(p => p.Luminance).ToArray();
            int n = values.Length;
            Array.Sort(values);
            for (int i = 0; i < n - 3; i++)
            {
                if (i > 0 && values[i] == values[i - 1]) continue;
                for (int j = i + 1; j < n - 2; j++)
                {
                    if (j > i + 1 && values[j] == values[j - 1]) continue;
                    int left = j + 1;
                    int right = n - 1;
                    while (left < right)
                    {
                        long currentSum = (long)values[i] + values[j] + values[left] + values[right];
                        if (currentSum == target)
                        {
                            results.Add(new List<int> { values[i], values[j], values[left], values[right] });
                            while (left < right && values[left] == values[left + 1]) left++;
                            while (left < right && values[right] == values[right - 1]) right--;
                            left++;
                            right--;
                        }
                        else if (currentSum < target) left++;
                        else right--;
                    }
                }
            }
            return results;
        }
    }
}
