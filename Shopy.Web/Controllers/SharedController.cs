using Microsoft.AspNetCore.Mvc;
using Shopy.Web.Shared;
[ApiController]
[Route("api/shared/")]
public class SharedController : ControllerBase
{
    [HttpPost("ContactUs/{name}/{email}/{message}")]
    public ActionResult ContactUs(string name, string email, string message)
    {
        try
        {
            Smtp.SendMessage(Smtp.From, "from : " + email, name + "\n" + message);
            return Ok("Message Sent");
        }
        catch (Exception ex)
        {
            return BadRequest("Failed to send message : " + ex.Message);
        }
    }

}
