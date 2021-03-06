using System;

namespace Elsheimy.Components.RemoteApi
{
  /// Can be applied to a propety/field to indicate that this member will be a header.
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
  public class HeaderAttribute : ParamAttribute
  {
    public HeaderAttribute(string name) : base(name)
    {
    }
  }
}
