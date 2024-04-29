
using Microsoft.Extensions.Logging;
using Neo4j.Driver;

namespace Neo4jExamples.SemanticKernel;

/**
  * The KnowledgeGraphStore class is a wrapper around the Neo4j.Driver.IDriver
  * interface. It provides a simple API for querying the knowledge graph.
  */
public class KnowledgeGraphStore {

  private readonly IDriver neo4jDriver;
  
  public KnowledgeGraphStore(IDriver neo4jDriver, Microsoft.Extensions.Logging.ILogger? logger) {
    this.neo4jDriver = neo4jDriver;
  }

  public Task VerifyConnectivityAsync() {
     return neo4jDriver.VerifyConnectivityAsync();
  }
  public IExecutableQuery<IRecord, IRecord> Query(string cypher)
  {
      return neo4jDriver.ExecutableQuery(cypher)
          .WithConfig(new QueryConfig(RoutingControl.Writers, "neo4j"));
  }

  public static KnowledgeGraphStore KnowledgeGraphStoreFactory(ILoggerFactory loggerFactory) {
    var logger = loggerFactory.CreateLogger("KnowledgeGraphStore");
    var neo4jDriver = DriverFactory.BuildDriver(loggerFactory);

    return new KnowledgeGraphStore(neo4jDriver, logger);
  }
}