using Elsheimy.Components.RemoteApi;

namespace Elsheimy.Components.Sms.SmsMisr
{
  public abstract class SmsMisrRequestBase : RemoteRequest
  {
    /// <summary>
    /// API username
    /// </summary>
    public string Username { get; set; }
    /// <summary>
    /// API password
    /// </summary>
    public string Password { get; set; }

  }
}
