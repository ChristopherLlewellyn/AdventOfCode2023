using System.Text;

namespace AdventOfCode2023;

public class Day1
{
    public void Part1()
    {
        string[] input = File.ReadAllLines($@"{Directory.GetCurrentDirectory()}\Day1.txt", Encoding.UTF8);

        var answer = 0;
        var digits = new [] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        foreach (var line in input)
        {
            var first = line.First(x => digits.Contains(x));
            var last = line.Last(x => digits.Contains(x));

            var combined = first.ToString() + last.ToString();

            answer += int.Parse(combined);
        }

        Console.WriteLine($"Day 1, part 1: {answer}");
    }

    public void Part2()
    {
        string[] input = File.ReadAllLines($@"{Directory.GetCurrentDirectory()}\Day1.txt", Encoding.UTF8);
        
        var answer = 0;
        var map = new Dictionary<string, int>()
        {
            { "0", 0 },
            { "1", 1 },
            { "2", 2 },
            { "3", 3 },
            { "4", 4 },
            { "5", 5 },
            { "6", 6 },
            { "7", 7 },
            { "8", 8 },
            { "9", 9 },
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 }
        };

        foreach (var line in input)
        {
            var first = -1;
            var last = -1;

            for (var i = 0; i < line.Count(); i++)
            {
                if (first > -1) break;

                if (map.ContainsKey(line[i].ToString()))
                {
                    first = map[line[i].ToString()];
                    break;
                }

                for (var j = i; j < line.Count(); j++)
                {
                    var word = line.Substring(i, j - i);
                    if (map.ContainsKey(word))
                    {
                        first = map[word];
                        break;
                    }
                }
            }

            for (var i = line.Count() - 1; i > 0; i--)
            {
                if (last > -1) break;

                if (map.ContainsKey(line[i].ToString()))
                {
                    last = map[line[i].ToString()];
                    break;
                }

                for (var j = i; j > 0; j--)
                {
                    var word = line.Substring(j, i - (j - 1));
                    if (map.ContainsKey(word))
                    {
                        last = map[word];
                        break;
                    }
                }
            }

            if (last == -1)
            {
                last = first;
            }

            answer += int.Parse(first.ToString() + last.ToString());
        }

        Console.WriteLine($"Day 1, part 2: {answer}");
    }
}
