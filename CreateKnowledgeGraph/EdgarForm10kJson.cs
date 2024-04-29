using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Neo4jExamples.SemanticKernel;

public sealed class EdgarForm10KJson
{
    [JsonPropertyName("item1")] public required string Item1 { get; set; }

    [JsonPropertyName("item1a")] public required string Item1A { get; set; }

    [JsonPropertyName("item7")] public required string Item7 { get; set; }

    [JsonPropertyName("item7a")] public required string Item7A { get; set; }

    [JsonPropertyName("cik")] public required string Cik { get; set; }

    [JsonPropertyName("cusip6")] public required string Cusip6 { get; set; }

    [JsonPropertyName("cusip")] public required string[] Cusip { get; set; }

    [JsonPropertyName("names")] public required string[] Names { get; set; }

    [JsonPropertyName("source")] public required Uri Source { get; set; }

    private string SampleText(string text)
    {
        var sampleText = text != null ? text.Substring(0, Math.Min(text.Length, 80)) : "";
        return Regex.Replace(sampleText, @"\s+", " ");
    }

    private string ArrayToString(string[]? array)
    {
        return array != null ? string.Join(", ", array) : "";
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("Form 10-K: ")
            .AppendLine(Source.ToString());
        sb.Append(nameof(Names)).Append(": ").AppendLine(ArrayToString(Names));
        AppendSampledPropertyToStringBuilder(sb, Item1);
        AppendSampledPropertyToStringBuilder(sb, Item1A);
        AppendSampledPropertyToStringBuilder(sb, Item7);
        AppendSampledPropertyToStringBuilder(sb, Item7A);
        sb.Append(nameof(Cik)).Append(": ").AppendLine(Cik);
        sb.Append(nameof(Cusip6)).Append(": ").AppendLine(Cusip6);
        sb.Append(nameof(Cusip)).Append(": ").AppendLine(ArrayToString(Cusip));
        return sb.ToString();
    }

    private void AppendSampledPropertyToStringBuilder(StringBuilder sb, string s,
        [CallerArgumentExpression("s")] string? parameterName = default)
    {
        sb.Append(parameterName).Append(": ").AppendLine(SampleText(s));
    }

    public static async Task<EdgarForm10KJson?> LoadAsync(string filePath)
    {
        await using var fs = new FileStream(filePath, FileMode.Open);
        return await JsonSerializer.DeserializeAsync<EdgarForm10KJson>(fs);
    }
}