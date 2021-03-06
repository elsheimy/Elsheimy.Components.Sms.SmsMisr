using System.Collections.Generic;

namespace Elsheimy.Components.RemoteApi.Helpers
{
  /// <summary>
  /// Query string helper.
  /// </summary>
  public static class QueryHelper
  {
    /// <summary>
    /// Generates a query string from a parameter.
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public static string GenerateQueryString(IEnumerable<Parameter> parameters)
    {
      string queryStr = string.Empty;
      foreach (var param in parameters)
      {
        queryStr += string.Format("{0}={1}&", param.Name, param.EncodedValue);
      }

      queryStr = queryStr.TrimEnd('&');
      return queryStr;
    }

  }
}
