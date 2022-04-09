using System;

namespace Shopy.Models.Dtos;
#nullable disable
public class ProductDto
{


    public string Category { get; set; }

    public string Model { get; set; }

    public decimal Price { get; set; }

    public string Details { get; set; }
    public string ImagePath { get; set; }

}