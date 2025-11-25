using Axon.Flow.MCP.Server.dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Axon.Flow.MCP.Server.Controllers;

[ApiController]
public class MCPController(IOptions<ServerInfo> serverInfo) : ControllerBase
{
  [Route("mcp")]
  public async Task<IActionResult> Handshake(JsonRpcRequest request)
  {
    switch (request.Method)
    {
      case "initialize": return Initialize(request);
      case "tools/list": return ToolsList(request);
      
    }
    
    return BadRequest("Unsupported method");
  }


  public IActionResult Initialize(JsonRpcRequest request)
  {
    return Ok(
      new
      {
        jsonrpc = "2.0",
        id = request.Id,
        result = new
        {
          protocolVersion = "2024-11-05",
          capabilities = new { tools = new { } },
          serverInfo = new
            { name = serverInfo?.Value?.Name ?? "axonflow.McpServer", version = serverInfo?.Value?.Version ?? "1.0.0" }
        }
      });
  }
  
  
  public IActionResult ToolsList(JsonRpcRequest request)
  {
    return Ok(
      new
      {
        jsonrpc = "2.0",
        id = request.Id,
        result = new
        {
          protocolVersion = "2024-11-05",
          capabilities = new { tools = new { } },
          serverInfo = new
            { name = serverInfo?.Value?.Name ?? "axonflow.McpServer", version = serverInfo?.Value?.Version ?? "1.0.0" }
        }
      });
  }
}