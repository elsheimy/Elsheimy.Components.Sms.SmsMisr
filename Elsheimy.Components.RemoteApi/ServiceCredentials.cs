namespace Elsheimy.Components.RemoteApi
{
  public class ServiceCredentials
  {
    public string Username { get; set; }
    public string Password { get; set; }


    public ServiceCredentials()
    {

    }

    public ServiceCredentials(string username, string password)
    {
      this.Username = username;
      this.Password = password;
    }
  }
}
