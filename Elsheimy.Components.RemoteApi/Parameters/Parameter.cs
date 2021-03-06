namespace Elsheimy.Components.RemoteApi
{
  public class Parameter
  {
    public string Name { get; set; }
    public string Value { get; set; }
    public string EncodedValue
    {
      get
      {
        if (null == Value)
          return null;

        return System.Web.HttpUtility.UrlEncode(Value);
      }
    }


    public Parameter() { }
    public Parameter(string name, string value)
    {
      this.Name = name;
      this.Value = value;
    }

  }
}
