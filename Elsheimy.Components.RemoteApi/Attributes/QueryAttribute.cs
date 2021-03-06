using System;

namespace Elsheimy.Components.RemoteApi
{
  /// Can be applied to a propety/field to indicate that this member will be a query parameter.
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
  public class QueryAttribute : ParamAttribute
  {
    public QueryAttribute(string name) : base(name)
    {
    }
  }
}
