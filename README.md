# SmsMisr Wrapper

This is a C# wrapper around the API of the leading SMS provider in Egypt, **SMS Misr** [https://sms.com.eg](https://sms.com.eg/) .

# Prerequisites

You have to register in **SMS Misr** ([https://sms.com.eg](https://sms.com.eg/)) service and generate an API username and password the the Developer API page.

## Usage

    SmsRequest request = new SmsRequest();
    request.Sender = "<sender id>";
    request.Message = "<message>";
    request.Language = MessageLanguage.English; // accepts English, Arabic and Unicode
    request.MobileList = new string[] { "<mobile number>"};
         
    SmsMisrService svc = new SmsMisrService("<API username>", "<API password>");
    svc.SendSms(request);

## NuGet Package

Find the package on NuGet here:
[https://www.nuget.org/packages/Elsheimy.Components.Sms.SmsMisr](https://www.nuget.org/packages/Elsheimy.Components.Sms.SmsMisr)

