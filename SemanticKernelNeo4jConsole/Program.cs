using Microsoft.KernelMemory;
using Neo4j.KernelMemory.MemoryStorage;
using Neo4j.Driver;

using dotenv.net;

var env = DotEnv.Fluent()
    .WithEnvFiles("../.env")
    .Read();

var neo4jConfig = new Neo4jConfig
{
    Uri = env["NEO4J_URI"],
    Username = env["NEO4J_USERNAME"],
    Password = env["NEO4J_PASSWORD"]
};

var kernelMemory = new KernelMemoryBuilder()
    .WithOpenAIDefaults(env["OPENAI_API_KEY"])
    .WithNeo4j(neo4jConfig)
    .Build<MemoryServerless>();

// First, provide some text to the Kernel Memory, which will be indexed and stored in Neo4j
await kernelMemory.ImportTextAsync("""
The "Hello, World!" program, often attributed to Brian Kernighan's work in the 1970s, serves as the quintessential introduction to programming languages, demonstrating basic syntax with a simple output function. Originating as a test phrase in Bell Laboratories for the B programming language, it has evolved into a universal starter program for beginners in coding, symbolizing the initiation into software development. Its simplicity makes it an ideal tool for education and system testing, illustrating the minimal requirements to execute a program across various computing environments. As a cultural staple in the tech community, "Hello, World!" represents both a rite of passage for new programmers and the universal joy of creating with code. This tradition showcases the evolution of programming languages and the shared beginnings of developers worldwide.
""", 
    documentId: "HelloWorld");

// Now ask a question
var question = "Who wrote the first Hello World?";

var answer = await kernelMemory.AskAsync(question);

Console.WriteLine($"Question: {question}\n\nAnswer: {answer.Result}");
