using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

using Neo4j.Driver;

namespace Neo4jExamples.SemanticKernel;

public class Program
{
    
    public static ILoggerFactory loggerFactory = LoggerFactory.Create(x => x.AddConsole().SetMinimumLevel(LogLevel.Critical));

    public static IDriver neo4jDriver = DriverFactory.BuildDriver(loggerFactory);

    public static async Task ShowForm10K(int limit = 10)
    {
        var form10Ks = await LoadForm10KSamples.LoadAsync();
        foreach (var form10K in form10Ks.Take(limit))
        {
            Console.WriteLine(new string('-', 80));
            Console.WriteLine(form10K);
        }
    }

    public static async Task ShowForm13s(string fileName, int limit = 10)
    {
        var form13s = await EdgarForm13Row.LoadAsync(fileName);
        foreach (var form13 in form13s.Take(limit))
        {
            Console.WriteLine(new string('-', 80));
            Console.WriteLine(form13);
        }
    }

    public static async Task<bool> CheckNeo4jConnection()
    {   

        try {

            await neo4jDriver.VerifyConnectivityAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error connecting to Neo4j: " + e);
            return false;
        }

        Console.WriteLine("Neo4j is ready at {0}", Config.Neo4jUri);

        return true;
    }

    public static async Task LoadCypherFromURL(string url, Dictionary<string, object> queryParameters)
    {
        Console.WriteLine("Loading cypher from URL: " + url);

        // read the cypher from the URL
        var cypher = await new HttpClient().GetStringAsync(url);

        // strip out the comments
        cypher = Regex.Replace(cypher, @"//.*", "");

        // strip out multiline comments
        cypher = Regex.Replace(cypher, @"/\*\*.*?\*/", "", RegexOptions.Singleline);
        
        // split the cypher into individual statements
        var statements = cypher.Split(";", StringSplitOptions.RemoveEmptyEntries);

        // trim whitespace from each statement
        statements = statements.Select(x => x.Trim()).ToArray();

        // filter out client-side commands that start with ':'
        statements = statements.Where(x => !x.Trim().StartsWith(":")).ToArray();
        
        foreach (var statement in statements)
        {
            await neo4jDriver.ExecutableQuery(statement)
                .WithParameters(queryParameters)
                .ExecuteAsync();
        }
        Console.WriteLine("Done.");
    }

    public static async Task<int> Main()
    {
        var Neo4jIsReady = await CheckNeo4jConnection();

        if (!Neo4jIsReady)
        {
            return 1;
        }

        await LoadCypherFromURL(
            "https://raw.githubusercontent.com/neo4j-examples/sec-edgar-notebooks/main/notebooks/kg-construction/kg-construction.cypher",
            new Dictionary<string, object> { 
                { "openAiApiKey", Config.ApiKey },
                { "baseURL", "https://raw.githubusercontent.com/neo4j-examples/sec-edgar-notebooks/main/data/sample/"}
            }
        );

        return 0;
    }
}