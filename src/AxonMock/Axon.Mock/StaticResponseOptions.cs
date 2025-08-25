namespace Axon.Mock;

public class StaticResponseOptions
{
  public string BasePath { get; set; }
  public Func<IRequest, string> GetResponsePathFor { get; set; }
}