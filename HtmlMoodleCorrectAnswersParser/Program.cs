using Fclp;
using HtmlAgilityPack;

internal class Program
{
    public static string FilePath { get; private set; }

    private static void Main(string[] args)
    {
        Console.WriteLine("HTML Parser for Moodle Answers page");

        var p = new FluentCommandLineParser();

        // specify which property the value will be assigned too.
        p.Setup<string>('f', "file")
           .Callback(value => FilePath = value)
           .Required();

        var result = p.Parse(args);

        var contents = File.ReadAllText(FilePath);

        HtmlDocument htmlSnippet = new HtmlDocument();
        htmlSnippet.LoadHtml(contents);

        List<string> answers = new List<string>();

        foreach (HtmlNode q in htmlSnippet.DocumentNode.SelectNodes("//div[contains(@id,'question-')]"))
        {
            Console.WriteLine($"Found answer '{q.Id}'");

            var qtext = q.SelectSingleNode("*/div[@class='qtext']");
            Console.WriteLine($"    Text: {qtext.InnerText}");

            var qanswer = q.SelectSingleNode("*/div[@class='rightanswer']");
            Console.WriteLine($"    Answer: {qanswer.InnerText}");
            //HtmlAttribute att = link.Attributes["href"];
            //hrefTags.Add(att.Value);
        }
    }
}