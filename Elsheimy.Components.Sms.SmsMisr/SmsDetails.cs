namespace Elsheimy.Components.Sms.SmsMisr
{
  public class SmsRawRequest
  {
    public string Username { get; set; }
    public string Password { get; set; }
    public int Language { get; set; }
    public int Environment { get; set; }
        public string Sender { get; set; }
    public string Mobile { get; set; }
    public string Message { get; set; }
    public string DelayUntil { get; set; }
  }
}
