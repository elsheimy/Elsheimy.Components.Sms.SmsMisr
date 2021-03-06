using System;
using System.Reflection;

namespace Elsheimy.Components.RemoteApi
{
  public class MemberAttributePair<T> where T : Attribute
  {
    public MemberInfo Member { get; set; }
    public T Attribute { get; set; }
  }
}
