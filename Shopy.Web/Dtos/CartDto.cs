using System;
using System.Collections.Generic;
using Shopy.Web.Models;

namespace Shopy.Web.Dtos;
#nullable disable
public class CartDto
{
    public int Id { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public string Phone { get; set; }
    public string Email { get; set; }
    public virtual IEnumerable<ProductDto> Products { get; set; }

}