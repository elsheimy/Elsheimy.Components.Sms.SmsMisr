using System.Linq;

namespace Elsheimy.Components.RemoteApi.Helpers
{
  /// <summary>
  /// URL helper.
  /// </summary>
  public static class UrlHelper
  {
    /// <summary>
    /// Joins two URL pieces together
    /// </summary>
    /// <param name="sections"></param>
    /// <returns></returns>
    public static string Concat(params string[] sections)
    {
      return string.Join("/", sections.Select(a => a.Trim('/')));
    }
  }
}
