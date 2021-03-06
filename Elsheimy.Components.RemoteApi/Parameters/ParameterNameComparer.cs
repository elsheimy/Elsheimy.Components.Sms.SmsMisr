using System.Collections.Generic;

namespace Elsheimy.Components.RemoteApi
{
  public class ParameterNameComparer : IEqualityComparer<Parameter>
  {
    public bool Equals(Parameter x, Parameter y)
    {
      return x != null && y != null &&
              x.Name != null && y.Name != null &&
              0 == string.Compare(x.Name, y.Name, true);
    }

    public int GetHashCode(Parameter obj)
    {
      return (obj.Name ?? string.Empty).ToLower().GetHashCode();
    }
  }
}
