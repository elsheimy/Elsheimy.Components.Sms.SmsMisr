using System.Collections.Generic;

namespace Elsheimy.Components.Sms.SmsMisr
{
  public class SmsErrorHandler
  {
    private Dictionary<string, string> _errorMessages;

    public string this[string code] { get { return GetErrorMessage(code); } }

    public SmsErrorHandler()
    {
      _errorMessages = new Dictionary<string, string>();

      RegisterDefaultErrors();
    }

    public virtual void RegisterDefaultErrors()
    {
      RegisterError("1200", Properties.Resources.SmsResponse_1200);
      RegisterError("1901", Properties.Resources.SmsResponse_1901);
      RegisterError("1902", Properties.Resources.SmsResponse_1902);
      RegisterError("1903", Properties.Resources.SmsResponse_1903);
      RegisterError("1904", Properties.Resources.SmsResponse_1904);
      RegisterError("1905", Properties.Resources.SmsResponse_1905);
      RegisterError("1906", Properties.Resources.SmsResponse_1906);
      RegisterError("1907", Properties.Resources.SmsResponse_1907);
      RegisterError("1908", Properties.Resources.SmsResponse_1908);
      RegisterError("1909", Properties.Resources.SmsResponse_1909);
      RegisterError("6000", Properties.Resources.SmsResponse_6000);
      RegisterError("8001", Properties.Resources.SmsResponse_8001);
      RegisterError("8002", Properties.Resources.SmsResponse_8002);
      RegisterError("8003", Properties.Resources.SmsResponse_8003);
      RegisterError("8004", Properties.Resources.SmsResponse_8004);
      RegisterError("8005", Properties.Resources.SmsResponse_8005);
      RegisterError("8006", Properties.Resources.SmsResponse_8006);
      RegisterError("Error", Properties.Resources.SmsResponse_Error);
    }


    public virtual void RegisterError(string code, string message)
    {
      _errorMessages.Add(code, message);
    }

    public virtual void UnregisterError(string code)
    {
      _errorMessages.Remove(code);
    }

    public virtual void ClearErrors()
    {
      _errorMessages.Clear();
    }

    public virtual string GetErrorMessage(string code)
    {
      return _errorMessages[code];
    }
  }
}
