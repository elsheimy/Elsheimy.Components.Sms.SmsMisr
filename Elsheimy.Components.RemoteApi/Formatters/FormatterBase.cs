using System;

namespace Elsheimy.Components.RemoteApi.Formatters
{
  public abstract class FormatterBase
  {
    public abstract string ContentTypeHeader { get; }
    public abstract string Format(object target);
    public abstract object FormatBack(string data, Type type);
  }
}
