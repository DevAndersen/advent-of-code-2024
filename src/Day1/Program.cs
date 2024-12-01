using System.Text.RegularExpressions;

string[] input = File.ReadAllLines("input");

List<int> left = new List<int>(input.Length);
List<int> right = new List<int>(input.Length);

Regex regex = new Regex(@"(\d+)\s+(\d+)", RegexOptions.Compiled);

foreach (string line in input)
{
    Match match = regex.Match(line);
    if (!match.Success)
    {
        throw new InvalidDataException("Line did not match expected format");
    }

    if (!int.TryParse(match.Groups[1].Value, out int leftNumber))
    {
        throw new InvalidDataException("Left number was not a valid integer");
    }

    if (!int.TryParse(match.Groups[2].Value, out int rightNumber))
    {
        throw new InvalidDataException("Right number was not a valid integer");
    }

    left.Add(leftNumber);
    right.Add(rightNumber);
}

left = left.Order().ToList();
right = right.Order().ToList();

long totalDiff = 0;

for (int i = 0; i < input.Length; i++)
{
    int leftNumber = left[i];
    int rightNumber = right[i];
    totalDiff += Math.Abs(leftNumber - rightNumber);
}

Console.WriteLine($"Answer to part 1: {totalDiff}");

Dictionary<int, int> rightListNumberFrequencies = new Dictionary<int, int>();
foreach (int number in right)
{
    if (rightListNumberFrequencies.TryGetValue(number, out int frequency))
    {
        rightListNumberFrequencies[number] = frequency + 1;
    }
    else
    {
        rightListNumberFrequencies[number] = 1;
    }
}

int similarityScore = 0;

foreach (int number in left)
{
    similarityScore += number * rightListNumberFrequencies.GetValueOrDefault(number, 0);
}

Console.WriteLine($"Answer to part 2: {similarityScore}");
