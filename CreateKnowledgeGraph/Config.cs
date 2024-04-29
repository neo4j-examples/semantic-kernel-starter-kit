using Microsoft.Extensions.Configuration;

namespace Neo4jExamples.SemanticKernel;

sealed class Config
{
    private static readonly Lazy<IConfigurationRoot> ConfigValues;

    static Config()
    {
        ConfigValues = new Lazy<IConfigurationRoot>(() =>
                new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .AddUserSecrets<DriverFactory>()
                    .Build(),
            LazyThreadSafetyMode.ExecutionAndPublication);
    }

    public static string ApiKey => ConfigValues.Value["OpenAI:ApiKey"] ??
                                   throw new InvalidOperationException("secret for api key not loaded");

    public static string ModelName => ConfigValues.Value["OpenAI:Model"] ??
                                      throw new InvalidOperationException("'Model' missing from appsettings");


    public static string Neo4jUri => ConfigValues.Value.GetSection("Neo4j")["URI"] ??
                                     throw new InvalidOperationException("'URI' missing from appsettings");

    public static string Neo4jUser => ConfigValues.Value.GetSection("Neo4j")["Username"] ??
                                      throw new InvalidOperationException("'Username' missing from appsettings");

    public static string Neo4jPassword => ConfigValues.Value["Neo4j:Password"] ??
                                          throw new InvalidOperationException("secret for Neo4j Password not loaded");
}