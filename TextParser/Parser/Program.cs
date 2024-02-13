using System.Text;

namespace Parser;

public static class Program
{
    public static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            throw new ArgumentException("there are no args");
        }

        foreach (var arg in args)
        {
            Console.WriteLine("Begin text");
            if (!File.Exists(arg))
            {
                throw new ArgumentException("file dont found");
            }

            var dictionary = new Dictionary<string, int>();

            var text = File.ReadAllText(arg, Encoding.UTF8);
            char[] delimiters = { ' ', '.', ',', ';', ':', '-', '!', '?', '"', '\'', '\n', '\r', '»', ']', '[', '…', '–' };
            var words = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                var key = word.ToLower();
                if (dictionary.ContainsKey(key))
                {
                    dictionary[key]++;
                }
                else
                {
                    dictionary[key] = 1;
                }
            }

            var sortWords = dictionary.OrderByDescending(pair => pair.Value);
            const string file = "text.txt";
            using (var writer = new StreamWriter(file))
            {
                writer.WriteLine(arg, Encoding.UTF8);
                foreach (var pair in sortWords)
                {
                    writer.WriteLine($"{pair.Key} {pair.Value}", Encoding.UTF8);
                }
                writer.WriteLine("end text");
            }
        }
    }
}