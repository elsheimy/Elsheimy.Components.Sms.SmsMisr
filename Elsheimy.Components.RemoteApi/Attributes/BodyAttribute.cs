using System;

namespace Elsheimy.Components.RemoteApi
{
  /// <summary>
  /// Can be applied to a propety/field to indicate that this member will be the body of the request. Only one item can be set as a body.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
  public class BodyAttribute : Attribute
  {
  }
}
