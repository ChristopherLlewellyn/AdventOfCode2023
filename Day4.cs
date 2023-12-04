using System.Text;
using Xunit;

namespace AdventOfCode2023;

public class Day4
{
    public void Part1()
    {
        var input = File.ReadAllLines($@"{Directory.GetCurrentDirectory()}\Day4.txt", Encoding.UTF8).ToList();

        var answer = 0;

        foreach (var line in input)
        {
            var card = line.Split(':')[1].Trim();

            var winningNumbers = card.Split('|')[0].Trim().Split(' ').Where(x => x.Length > 0);
            var numbers = card.Split('|')[1].Trim().Split(' ').Where(x => x.Length > 0);

            var points = 0;
            var hasOneCard = false;

            foreach (var number in numbers)
            {
                if (winningNumbers.Contains(number))
                {
                    if (hasOneCard)
                    {
                        points = points * 2;
                    }
                    else
                    {
                        points = 1;
                        hasOneCard = true;
                    }
                }
            }

            answer += points;
        }

        Console.WriteLine($"Day 4, part 1: {answer}");
    }

    private Dictionary<int, int> CardCache = new Dictionary<int, int>();

    public void Part2()
    {
        var input = File.ReadAllLines($@"{Directory.GetCurrentDirectory()}\Day4.txt", Encoding.UTF8).ToList();

        var answer = input.Count; // We include our starting number of cards in the answer

        for (var i = 0; i < input.Count; i++)
        {
            var cardNumber = i + 1;
            answer += ProcessCard(input[i], cardNumber, input);
        }

        Console.WriteLine($"Day 4, part 2: {answer}");
    }

    private int ProcessCard(string line, int cardNumber, List<string> startingCards)
    {
        var card = line.Split(':')[1].Trim();
        var winningNumbers = card.Split('|')[0].Trim().Split(' ').Where(x => x.Length > 0);
        var numbers = card.Split('|')[1].Trim().Split(' ').Where(x => x.Length > 0);

        var matchingNumbers = 0;

        foreach (var number in numbers)
        {
            if (winningNumbers.Contains(number))
            {
                matchingNumbers++;
            }
        }

        // Copy the next x cards where x = number of matching numbers
        // starting from the current card number
        var cardCopies = matchingNumbers;
        for (var i = cardNumber; i < cardNumber + matchingNumbers; i++) 
        {
            var nextCardNumber = i + 1;
            if (nextCardNumber > startingCards.Count) break;
            
            if (CardCache.ContainsKey(nextCardNumber))
            {
                cardCopies += CardCache[nextCardNumber];
            }
            else
            {
                var cardsGenerated = ProcessCard(startingCards[nextCardNumber - 1], nextCardNumber, startingCards);
                CardCache[nextCardNumber] = cardsGenerated;
                cardCopies += cardsGenerated;
            }
        }

        return cardCopies;
    }

    [Fact]
    public void Test()
    {

    }
}