using Microsoft.Extensions.Configuration;
using System;
using System.IO;

public static class Config
{
    private const string DefaultConfigFile = "appsettings.json";

    private static readonly IConfigurationRoot ConfigValues;

    static Config()
    {
        string configFile = Path.Combine(Directory.GetCurrentDirectory(), "..", DefaultConfigFile);

        ConfigValues = new ConfigurationBuilder()
                    .AddJsonFile(configFile)
                    .Build();
    }

    public static bool UseAzureOpenAI => (ConfigValues["OpenAI:UseAzureOpenAI"] != null) ? bool.Parse(ConfigValues["OpenAI:UseAzureOpenAI"])
                                            : throw new InvalidOperationException("'UseAzureOpenAI' missing from appsettings");

    public static string ApiKey => ConfigValues["OpenAI:ApiKey"] ?? 
                                        throw new InvalidOperationException("secret for api key not loaded");

    public static string ModelName => ConfigValues["OpenAI:Model"] ??
                                      throw new InvalidOperationException("'Model' missing from appsettings");


    public static string Neo4jUri => ConfigValues.GetSection("Neo4j")["URI"] ??
                                     throw new InvalidOperationException("'URI' missing from appsettings");

    public static string Neo4jUser => ConfigValues.GetSection("Neo4j")["Username"] ??
                                      throw new InvalidOperationException("'Username' missing from appsettings");

    public static string Neo4jPassword => ConfigValues["Neo4j:Password"] ??
                                          throw new InvalidOperationException("secret for Neo4j Password not loaded");
}