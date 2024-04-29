using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace Neo4jExamples.SemanticKernel;

public sealed class EdgarForm13Row
{
    [Name("source")] public required string Source { get; set; }

    [Name("managerCik")] public required string ManagerCik { get; set; }

    [Name("managerAddress")] public required string ManagerAddress { get; set; }

    [Name("managerName")] public required string ManagerName { get; set; }

    [Name("reportCalendarOrQuarter")] public required string ReportCalendarOrQuarter { get; set; }

    [Name("cusip")] public required string Cusip { get; set; }

    [Name("cusip6")] public required string Cusip6 { get; set; }

    [Name("companyName")] public required string CompanyName { get; set; }

    [Name("value")] public float Value { get; set; }

    [Name("shares")] public int Shares { get; set; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("Form 13").Append(": ").AppendLine(Source);
        AppendPropertyLineToStringBuilder(sb, ManagerName);
        AppendPropertyLineToStringBuilder(sb, ManagerCik);
        AppendPropertyLineToStringBuilder(sb, Cusip6);
        AppendPropertyLineToStringBuilder(sb, Cusip);
        AppendPropertyLineToStringBuilder(sb, CompanyName);
        AppendPropertyLineToStringBuilder(sb, Value);
        AppendPropertyLineToStringBuilder(sb, Shares);
        return sb.ToString();
    }

    private void AppendPropertyLineToStringBuilder(StringBuilder sb, object s,
        [CallerArgumentExpression("s")] string? parameterName = default)
    {
        sb.Append(parameterName).Append(": ").AppendLine(s.ToString());
    }

    public static async Task<List<EdgarForm13Row>> LoadAsync(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        return await csv.GetRecordsAsync<EdgarForm13Row>().ToListAsync();
    }
}