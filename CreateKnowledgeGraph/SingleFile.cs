namespace Neo4jExamples.SemanticKernel;

public static class SingleFile
{
    public static Task<EdgarForm10KJson?> Load()
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "data", "single", "form10k",
            "0000950170-23-027948.json");

        return EdgarForm10KJson.LoadAsync(filePath);
    }
}