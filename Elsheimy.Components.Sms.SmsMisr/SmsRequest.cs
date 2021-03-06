using Elsheimy.Components.RemoteApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Elsheimy.Components.Sms.SmsMisr
{
  public class SmsRequest : SmsMisrRequestBase
  {
    public override string EndpointAddress => "v2";
    public override HttpMethod Method => HttpMethod.Post;

    public MessageLanguage Language { get; set; } = MessageLanguage.English;
    /// <summary>
    /// The registered and activated sender.
    /// </summary>
    public string Sender { get; set; }
    /// <summary>
    /// Recipient mobile list.
    /// </summary>
    public IEnumerable<string> MobileList { get; set; }
    /// <summary>
    /// Mobile prefix. Will be applied to each item of <see cref="Elsheimy.Components.Sms.SmsMisr.SmsRequest.MobileList"/>
    /// </summary>
    public virtual string MobilePrefix { get; set; } = "2";
    /// <summary>
    /// Your SMS message
    /// </summary>
    public string Message { get; set; }
    /// <summary>
    /// Allows you to schedule a message.
    /// </summary>
    public DateTime? DelayUntil { get; set; }
    public virtual string DelayUntilFormat { get; } = "yyyy-MM-dd-HH-mm";
    [Body]
    public SmsRawRequest RawRequest { get { return CreateRawRequest(); } }



    public SmsRequest() { }
    public SmsRequest(string message, MessageLanguage language, string senderId, string[] mobileList)
    {
      this.Message = message;
      this.Language = language;
      this.Sender = senderId;
      this.MobileList = mobileList;
    }

    protected virtual SmsRawRequest CreateRawRequest()
    {
      SmsRawRequest req = new SmsRawRequest();
      req.Username = this.Username;
      req.Password = this.Password;
      req.Language = (int)this.Language;
      req.Sender = this.Sender;
      req.Mobile = GetEncodedMobileList();
      req.Message = GetEncodedMessage();
      req.DelayUntil = GetEncodedDelayUntil();
      return req;
    }

    /// <summary>
    /// Joins mobile items together and applies <see cref="Elsheimy.Components.Sms.SmsMisr.SmsRequest.MobilePrefix"/> prefix.
    /// </summary>
    /// <returns></returns>
    protected virtual string GetEncodedMobileList()
    {
      string mobileParam = string.Empty;

      if (null != MobileList)
      {
        mobileParam = string.Join(",", MobileList.Select(mob => (MobilePrefix ?? string.Empty) + mob));
      }

      return mobileParam;
    }

    /// <summary>
    /// Returns encoded message based on message language.
    /// </summary>
    /// <returns></returns>
    protected virtual string GetEncodedMessage()
    {
      if (null == Message || Language == MessageLanguage.English || Language == MessageLanguage.Arabic)
        return Message;


      string encodedMessage = string.Empty;
      foreach (var chr in Message)
        encodedMessage += ((int)chr).ToString("x4");

      return encodedMessage;
    }

    /// <summary>
    /// Returns encoded DelayUntil value.
    /// </summary>
    /// <returns></returns>
    protected virtual string GetEncodedDelayUntil()
    {
      if (null == DelayUntil)
        return null;

      return DelayUntil.Value.ToString(DelayUntilFormat);
    }

  }
}
