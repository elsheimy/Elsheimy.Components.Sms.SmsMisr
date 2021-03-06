using Newtonsoft.Json;
using System;

namespace Elsheimy.Components.RemoteApi.Formatters
{
  public class JsonFormatter : FormatterBase
  {
    public override string ContentTypeHeader => "application/json";
    public JsonSerializerSettings Settings { get; set; }

    public override string Format(object target)
    {
      return JsonConvert.SerializeObject(target, this.Settings);
    }

    public override object FormatBack(string data, Type type)
    {
      return JsonConvert.DeserializeObject(data, type, Settings);
    }

  }

}
