using System.Text.Json.Serialization;

namespace Axon.Flow.MCP.Server.dto;

public class JsonRpcRequest
{
  [JsonPropertyName("jsonrpc")]
  public string JsonRpc { get; set; } = "2.0";

  [JsonPropertyName("method")]
  public string Method { get; set; } = string.Empty;

  [JsonPropertyName("id")]
  public object? Id { get; set; }

  [JsonPropertyName("params")]
  public object? Params { get; set; }
}