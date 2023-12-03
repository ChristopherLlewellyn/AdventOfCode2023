using System;
using System.Text;
using Xunit;

namespace AdventOfCode2023;

public class Day2
{
    public void Part1()
    {
        string[] input = File.ReadAllLines($@"{Directory.GetCurrentDirectory()}\Day2.txt", Encoding.UTF8);

        var reds = 12;
        var greens = 13;
        var blues = 14;

        var answer = 0;

        foreach (var line in input)
        {
            int gameId = int.Parse(line.Replace("Game ", "").Split(':')[0]);
            var isValidGame = true;

            var game = line.Split(':').Last().Replace(" ", "")
                .Replace("red", "r")
                .Replace("green", "g")
                .Replace("blue", "b");

            var rounds = game.Split(';');

            foreach (var round in rounds)
            {
                var scores = new Dictionary<char, int>()
                {
                    { 'r', 0 },
                    { 'g', 0 },
                    { 'b', 0 }
                };

                var colouredBlockGroups = round.Split(',');

                foreach (var group in colouredBlockGroups)
                {
                    int amount = int.Parse(group.Remove(group.Length - 1, 1));
                    char colour = group.Last();
                    scores[colour] += amount;
                }

                if (scores['r'] > reds || scores['g'] > greens || scores['b'] > blues)
                {
                    isValidGame = false;
                }

                if (!isValidGame)
                {
                    break;
                }
            }

            if (isValidGame)
            {
                answer += gameId;
            }
        }

        Console.WriteLine($"Day 2, part 1: {answer}");
    }

    public void Part2()
    {
        string[] input = File.ReadAllLines($@"{Directory.GetCurrentDirectory()}\Day2.txt", Encoding.UTF8);

        var answer = 0;

        foreach (var line in input)
        {
            int gameId = int.Parse(line.Replace("Game ", "").Split(':')[0]);

            var game = line.Split(':').Last().Replace(" ", "")
                .Replace("red", "r")
                .Replace("green", "g")
                .Replace("blue", "b");

            var rounds = game.Split(';');

            var neededReds = 0;
            var neededGreens = 0;
            var neededBlues = 0;

            foreach (var round in rounds)
            {
                var scores = new Dictionary<char, int>()
                {
                    { 'r', 0 },
                    { 'g', 0 },
                    { 'b', 0 }
                };

                var colouredBlockGroups = round.Split(',');

                foreach (var group in colouredBlockGroups)
                {
                    int amount = int.Parse(group.Remove(group.Length - 1, 1));
                    char colour = group.Last();
                    scores[colour] += amount;
                }

                if (neededReds < scores['r']) neededReds = scores['r'];
                if (neededBlues < scores['b']) neededBlues = scores['b'];
                if (neededGreens < scores['g']) neededGreens = scores['g'];
            }

            answer += neededReds * neededGreens * neededBlues;
        }

        Console.WriteLine($"Day 2, part 2: {answer}");
    }

    [Fact]
    public void Test() 
    { 

    }
}