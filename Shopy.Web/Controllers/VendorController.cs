using Microsoft.AspNetCore.Mvc;

using Shopy.Web.Models;
using Shopy.Web.Dtos;
using Shopy.Web.Shared;
namespace Northwind.WebApi.Controllers;

[Route("api/Vendors/")]
[ApiController]
public class VendorController : ControllerBase
{
    // GET: api/<VendorController>
    [HttpGet("get/username={username}")]
    public VendorDto Get(string username)
    {
        Vendor _vendor = new();
        VendorDto vendor = _vendor.Get(username).AsDto();
        return vendor;
    }
    [HttpGet("getlimit/limit={limit:int}")]
    public dynamic GetLimit(int limit)
    {
        Vendor vendor = new();

        return vendor.AllVendors(limit).AsDto();

    }

    [HttpPut("updatePass/username={username}/value={value}")]
    public string UpdatePassword(string username, string value)
    {
        Vendor vendor = new();
        return vendor.UpdatePassword(username, value);

    }
    [HttpPut("updateVCode/username={username}/value={value}")]
    public string UpdateVerificationCode(string username, string value)
    {
        Vendor vendor = new();
        vendor = vendor.Get(username);


        return vendor.UpdateVerificationCode(vendor, value);

    }
    // POST api/<VendorController>
    [HttpPost]
    public string Add(VendorDto vendor)
    {
        Vendor _vendor = new();
        return _vendor.Add(vendor.AsNormal());
    }

    [HttpDelete("Delete")]
    public dynamic Delete(string username)
    {
        Vendor _vendor = new();

        return _vendor.Delete(username);
    }
    [HttpPost("signin/{username}/{password}")]
    public ActionResult SignIn(string username, string password)
    {
        try
        {
            Vendor _vendor = new();

            if (!_vendor.Exist(username))
            {
                return NotFound(MyExceptions.VendorNotFound(username));
            }

            VendorDto vendor = _vendor.Get(username).AsDto();
            if (password.ToSha256() == vendor.Password)
            {
                return Ok(vendor.Username);
            }
            return BadRequest("Wrong password ");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("SignUp")]
    public ActionResult SignUp(VendorDto vendorDto)
    {
        Vendor vendor = vendorDto.AsNormal();
        string VerificationCode = new Random().Next(100000, 999999).ToString();
        try
        {
            Smtp.SendMessage(
            toEmail: vendorDto.Email,
            subject: "Verification code for Shopy",
            body: "Please enter this code to verify your account : " + VerificationCode);
        }
        catch
        {
            return BadRequest("Error sending verification code");
        }
        Vendor _vendor = new();

        _vendor.UpdateVerificationCode(vendor, VerificationCode);
        _vendor.Add(vendor);
        return Ok(VerificationCode);

    }
    [HttpGet("Verify/{username}/{verificationCode}")]
    public ActionResult Verify(string username, string verificationCode)
    {
        Vendor vendor = new();

        Vendor _vendor = vendor.Get(username);
        try
        {
            if (_vendor.VerificationCode != verificationCode)
            {
                return BadRequest("Verification Code is wrong");
            }
            using (ShopyCtx db = new())
            {
                _vendor.IsVerified = true;
                db.SaveChanges();
            }
            return Ok(_vendor.Username);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("vendorModels/{username}")]
    public ActionResult VendorModels(string username)
    {
        Vendor vendor = new();
        Vendor _vendor = vendor.Get(username);
        try
        {
            if (_vendor.IsVerified == false)
            {
                return BadRequest("Vendor is not verified");
            }
            return Ok(_vendor.VendorModels(username).AsDto());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}



