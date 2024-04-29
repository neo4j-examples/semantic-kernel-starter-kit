namespace Neo4jExamples.SemanticKernel;

public static class LoadForm10KSamples
{
    public static async Task<List<EdgarForm10KJson>> LoadAsync()
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "data", "single", "form10k",
            "0000950170-23-027948.json");

        var singleForm = await EdgarForm10KJson.LoadAsync(filePath);
        
        if (singleForm != null)
            return [singleForm];

        return [];
    }
}