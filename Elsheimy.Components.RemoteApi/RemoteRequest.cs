using System.Net.Http;

namespace Elsheimy.Components.RemoteApi
{
  /// <summary>
  /// Base for remote requests
  /// </summary>
  public abstract class RemoteRequest
  {
    /// <summary>
    /// request endpoint address
    /// </summary>
    public abstract string EndpointAddress { get; }
    /// <summary>
    /// HTTP  method
    /// </summary>
    public virtual HttpMethod Method { get; } = HttpMethod.Get;
  }
}
