using System;

namespace Elsheimy.Components.RemoteApi
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
  public abstract class ParamAttribute : Attribute
  {
    /// <summary>
    /// Target parameter name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Is parameter required. Indicates whether to include an empty string if parameter value is null/empty.  Default is True.
    /// </summary>
    public bool IsRequired { get; set; }

    public ParamAttribute(string name) { this.Name = name; }
  }
}
