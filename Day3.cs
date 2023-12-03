using System.Text;
using Xunit;

namespace AdventOfCode2023;

public class Day3
{
    private readonly List<char> Numbers = new List<char>()
    {
        '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
    };

    public void Part1()
    {
        var input = File.ReadAllLines($@"{Directory.GetCurrentDirectory()}\Day3.txt", Encoding.UTF8).ToList();

        var answer = 0;

        // Surround the input in . to prevent out of bounds
        input = input.Prepend(new string('.', input.First().Length)).ToList();
        input = input.Append(new string('.', input.First().Length)).ToList();
        for (var i = 0; i < input.Count(); i++)
        {
            input[i] = input[i].Insert(0, "."); // beginning
            input[i] += "."; // end
        }

        int? _skipTo = null;

        for (var y = 0; y < input.Count(); y++) // y coordinate
        {
            for (var x = 0; x < input[y].Count(); x++) // x coordinate
            {
                if (_skipTo is not null && x != _skipTo)
                {
                    continue;
                }

                if (_skipTo is not null && x == _skipTo)
                {
                    _skipTo = null;
                }

                if (Numbers.Contains(input[y][x])) // if (is a number)
                {
                    // Get adjacent elements
                    var topLeft = input[y - 1][x - 1];
                    var topMiddle = input[y - 1][x];
                    var topRight = input[y - 1][x + 1];
                    var left = input[y][x - 1];
                    var right = input[y][x + 1];
                    var bottomLeft = input[y + 1][x - 1];
                    var bottomMiddle = input[y + 1][x];
                    var bottomRight = input[y + 1][x + 1];

                    // Check if there is an adjacent symbol
                    if (IsASymbol(topLeft)
                        || IsASymbol(topMiddle)
                        || IsASymbol(topRight)
                        || IsASymbol(left)
                        || IsASymbol(right)
                        || IsASymbol(bottomLeft)
                        || IsASymbol(bottomMiddle)
                        || IsASymbol(bottomRight))
                    {
                        // Get the number we're on
                        var digitsBefore = "";
                        var goingBack = true;
                        var i = x;
                        while (goingBack)
                        {
                            i -= 1;

                            // If element behind is number
                            if (Numbers.Contains(input[y][i]))
                            {
                                digitsBefore += input[y][i];
                            }
                            else
                            {
                                goingBack = false;
                            }
                        }

                        var digitsAfter = "";
                        var goingForward = true;
                        i = x;
                        while (goingForward)
                        {
                            i += 1;

                            // If element ahead is number
                            if (Numbers.Contains(input[y][i]))
                            {
                                digitsAfter += input[y][i];
                            }
                            else
                            {
                                _skipTo = i; // reached the end of the number, so skip to this index
                                goingForward = false;
                            }
                        }

                        if (digitsBefore.Length > 0)
                        {
                            var newDigitsBefore = digitsBefore.ToCharArray();
                            Array.Reverse(newDigitsBefore);
                            digitsBefore = new string(newDigitsBefore);
                        }

                        var thisNumber = int.Parse($"{digitsBefore}{input[y][x]}{digitsAfter}");

                        answer += thisNumber;
                    }
                }
            }
        }

        Console.WriteLine($"Day 3, part 1: {answer}");
    }

    private bool IsASymbol(char element)
    {
        return !Numbers.Contains(element) && element != '.';
    }

    public void Part2()
    {
        var input = File.ReadAllLines($@"{Directory.GetCurrentDirectory()}\Day3.txt", Encoding.UTF8).ToList();

        var answer = 0;

        // Surround the input in . to prevent out of bounds
        input = input.Prepend(new string('.', input.First().Length)).ToList();
        input = input.Append(new string('.', input.First().Length)).ToList();
        for (var i = 0; i < input.Count(); i++)
        {
            input[i] = input[i].Insert(0, "."); // beginning
            input[i] += "."; // end
        }

        for (var y = 0; y < input.Count(); y++) // y coordinate
        {
            for (var x = 0; x < input[y].Count(); x++) // x coordinate
            {
                if (input[y][x] == '*')
                {
                    // Get adjacent elements
                    var topLeft = input[y - 1][x - 1];
                    var topMiddle = input[y - 1][x];
                    var topRight = input[y - 1][x + 1];
                    var left = input[y][x - 1];
                    var right = input[y][x + 1];
                    var bottomLeft = input[y + 1][x - 1];
                    var bottomMiddle = input[y + 1][x];
                    var bottomRight = input[y + 1][x + 1];

                    // Check for numbers, get extract them if they exist
                    var numbers = new List<int>();

                    // Top
                    if (IsANumber(topLeft))
                    {
                        numbers.Add(ExtractNumber(input[y - 1], x - 1));
                    }
                    if (IsANumber(topMiddle) && !IsANumber(topLeft))
                    {
                        numbers.Add(ExtractNumber(input[y - 1], x));
                    }
                    if (IsANumber(topRight) && !IsANumber(topMiddle))
                    {
                        numbers.Add(ExtractNumber(input[y - 1], x + 1));
                    }

                    // Middle
                    if (IsANumber(left))
                    {
                        numbers.Add(ExtractNumber(input[y], x - 1));
                    }
                    if (IsANumber(right))
                    {
                        numbers.Add(ExtractNumber(input[y], x + 1));
                    }

                    // Bottom
                    if (IsANumber(bottomLeft))
                    {
                        numbers.Add(ExtractNumber(input[y + 1], x - 1));
                    }
                    if (IsANumber(bottomMiddle) && !IsANumber(bottomLeft))
                    {
                        numbers.Add(ExtractNumber(input[y + 1], x));
                    }
                    if (IsANumber(bottomRight) && !IsANumber(bottomMiddle))
                    {
                        numbers.Add(ExtractNumber(input[y + 1], x + 1));
                    }

                    if (numbers.Count == 2)
                    {
                        answer += numbers[0] * numbers[1];
                    }
                }
            }
        }

        Console.WriteLine($"Day 3, part 2: {answer}");
    }

    private bool IsANumber(char element)
    {
        return Numbers.Contains(element);
    }

    private int ExtractNumber(string row, int startIndex)
    {
        // Get the number
        var digitsBefore = "";
        var goingBack = true;
        var i = startIndex;
        while (goingBack)
        {
            i -= 1;

            // If element behind is number
            if (Numbers.Contains(row[i]))
            {
                digitsBefore += row[i];
            }
            else
            {
                goingBack = false;
            }
        }

        var digitsAfter = "";
        var goingForward = true;
        i = startIndex;
        while (goingForward)
        {
            i += 1;

            // If element ahead is number
            if (Numbers.Contains(row[i]))
            {
                digitsAfter += row[i];
            }
            else
            {
                goingForward = false;
            }
        }

        if (digitsBefore.Length > 0)
        {
            var newDigitsBefore = digitsBefore.ToCharArray();
            Array.Reverse(newDigitsBefore);
            digitsBefore = new string(newDigitsBefore);
        }

        var thisNumber = int.Parse($"{digitsBefore}{row[startIndex]}{digitsAfter}");

        return thisNumber;
    }
}