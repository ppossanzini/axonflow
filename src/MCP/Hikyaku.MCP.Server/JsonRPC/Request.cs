using System.Text.Json.Serialization;
using Axon.Flow.MCP.Server.dto;

namespace Axon.Flow.MCP.Server.JsonRPC;

public class Request
{
  [JsonPropertyName("jsonrpc")] public string JsonRpc { get; init; } = "2.0";

  [JsonPropertyName("method")] public string Method { get; set; } = string.Empty;

  [JsonPropertyName("id")] public object Id { get; set; }

  [JsonPropertyName("params")] public object Params { get; set; }

  public Response SuccessfulResponse(Object result)
  {
    return new Response(result, Id);
  }

  public Response ErrorResponse(ErrorCode code, string message, Object errordata = null)
  {
    return Response.Error(code, message, errordata);
  }
}