using System.Text.Json.Serialization;

namespace Axon.Flow.MCP.Server.dto;

public class ToolsListResult
{
  [JsonPropertyName("tools")]
  public List<McpTool> Tools { get; set; } = new();
}