using Microsoft.AspNetCore.Mvc;

namespace Elsheimy.Components.Sms.SmsMisr.WebDriver.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      SmsRequest request = new SmsRequest();
      request.Username = "<API username>";
      request.Password = "<API password>";
      request.Sender = "<sender id>";
      request.Message = "<the message>";
      request.MobileList = new string[] { };
      return View(request);
    }


    [ValidateAntiForgeryToken]
    [HttpPost]
    public SmsResponse  Send([FromBody] SmsRequest request)
    {
      SmsMisrService svc = new SmsMisrService(request.Username, request.Password);
      return svc.SendSms(request);
    }
  }
}
