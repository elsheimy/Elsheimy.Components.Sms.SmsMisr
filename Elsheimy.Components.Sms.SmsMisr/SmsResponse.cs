using Elsheimy.Components.RemoteApi;

namespace Elsheimy.Components.Sms.SmsMisr
{
  public class SmsResponse : RemoteResponse
  {
    /// <summary>
    /// Default Success Code
    /// </summary>
    public virtual string DefaultSuccessCode { get; } = "6000";
    public bool IsSuccess { get { return string.Compare(DefaultSuccessCode, Code, true) == 0; } }
    /// <summary>
    /// Error message handler.
    /// </summary>
    public SmsErrorHandler ErrorHandler { get; set; }
    public  virtual string ErrorMessage
    {
      get
      {
        if (false == IsSuccess && null != ErrorHandler)
          return ErrorHandler[Code];
        return null;
      }
    }

    public string Code { get; set; }
    public long SMSID { get; set; }

    /// <summary>
    /// Vodafone number count.
    /// </summary>
    public long Vodafone { get; set; }
    /// <summary>
    /// Etisalat number count.
    /// </summary>
    public long Etisalat { get; set; }
    /// <summary>
    /// Orange number count.
    /// </summary>
    public long Orange { get; set; }
    /// <summary>
    /// WE number count.
    /// </summary>
    public long We { get; set; }

    public MessageLanguage Language { get; set; }

    /// <summary>
    /// Vodafone cost (points.)
    /// </summary>
    public long VodafoneCost { get; set; }
    /// <summary>
    /// Etisalat cost (points.)
    /// </summary>
    public long EtisalatCost { get; set; }
    /// <summary>
    /// Orange cost (points.)
    /// </summary>
    public long OrangeCost { get; set; }
    /// <summary>
    /// WE cost (points.)
    /// </summary>
    public long WeCost { get; set; }


    public SmsResponse()
    {
      this.ErrorHandler = new SmsErrorHandler();
    }

  }
}
