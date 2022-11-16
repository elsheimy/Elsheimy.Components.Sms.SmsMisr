using Elsheimy.Components.RemoteApi;
using System.Threading.Tasks;

namespace Elsheimy.Components.Sms.SmsMisr
{
    public class SmsMisrService : RemoteService
    {
        public override string BaseAddress => "https://smsmisr.com/api";
        [Query("username")]
        internal protected string Username { get; set; }
        [Query("password")]
        internal protected string Password { get; set; }


        public SmsMisrService() { }
        public SmsMisrService(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }


        /// <summary>
        /// Sends an SMS asynchronously.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="language"></param>
        /// <param name="senderId"></param>
        /// <param name="mobileList"></param>
        /// <returns></returns>
        public Task<SmsResponse> SendSmsAsync(string message, MessageLanguage language, string senderId, string[] mobileList)
        {
            return Task.Run<SmsResponse>(() =>
            {
                return SendSms(message, language, senderId, mobileList);
            });
        }
        /// <summary>
        /// Sends an SMS asynchronously.
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public Task<SmsResponse> SendSmsAsync(SmsRequest req)
        {
            return Task.Run<SmsResponse>(() =>
            {
                return SendSms(req);
            });
        }
        /// <summary>
        /// Sends an SMS synchronously.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="language"></param>
        /// <param name="senderId"></param>
        /// <param name="mobileList"></param>
        /// <returns></returns>
        public SmsResponse SendSms(string message, MessageLanguage language, string senderId, string[] mobileList)
        {
            SmsRequest req = new SmsRequest();
            req.Message = message;
            req.Language = language;
            req.Sender = senderId;
            req.MobileList = mobileList;

            return SendSms(req);
        }
        /// <summary>
        /// Sends an SMS synchronously.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="language"></param>
        /// <param name="senderId"></param>
        /// <param name="mobileList"></param>
        /// <returns></returns>
        public SmsResponse SendSms(SmsRequest req)
        {
            req.Username = this.Username;
            req.Password = this.Password;
            return base.SendRequest<SmsResponse>(req);
        }
    }
}
