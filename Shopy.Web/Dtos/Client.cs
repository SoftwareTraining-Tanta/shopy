using System.Collections.Generic;
using Shopy.Models;

namespace Shopy.Models.Dtos;
#nullable disable
public class ClientDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

}