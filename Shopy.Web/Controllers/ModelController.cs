using Microsoft.AspNetCore.Mvc;
using Shopy.Web.Models;
using Shopy.Web.Dtos;
using Shopy.Web.Shared;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Northwind.WebApi.Controllers;

[Route("api/models/")]
[ApiController]
public class ModelController : ControllerBase
{
    // GET: api/<ModelController>
    [HttpGet("Get/{name}")]
    public ModelDto Get(string name)
    {
        Model model = new();
        ModelDto _model = model.Get(name).AsDto();
        return _model;
    }

    // PUT api/<ModelController>/5
    [HttpPut("updatename/modelname={modelName}/value={value}")]
    public void UpdateName(string modelName, string value)
    {
        Model model = new();
        model.UpdateName(modelName, value);

    }
    [HttpPut("updateimage/modelname={modelName}/value={value}")]
    public string UpdateImagePath(string modelName, string value)
    {
        Model model = new();
        return model.UpdateImagePath(modelName, @String.Join('/', value.Split("%2F")));

    }
    [HttpPut("updateprice/modelname={modelName}/value={value}")]
    public void UpdatePrice(string modelName, decimal value)
    {
        Model model = new();
        model.UpdatePrice(modelName, value);

    }
    [HttpPut("updateSalePrice/modelname={modelName}/value={value}")]
    public void UpdateSalePrice(string modelName, decimal value)
    {
        Model model = new();
        model.UpdateSalePrice(modelName, value);

    }
    [HttpPut("updateBrand/modelname={modelName}/value={value}")]
    public void UpdateBrand(string modelName, string value)
    {
        Model model = new();
        model.UpdateBrand(modelName, value);

    }
    [HttpPut("updateColor/modelname={modelName}/value={value}")]
    public void UpdateColor(string modelName, string value)
    {
        Model model = new();
        model.UpdateColor(modelName, value);

    }
    [HttpPut("updateFeatures/modelname={modelName}/value={value}")]
    public void UpdateFeatures(string modelName, string value)
    {
        Model model = new();
        model.UpdateFeatures(modelName, value);

    }
    // POST api/<ModelController>
    [HttpGet("count/")]
    public int count(string modelName)
    {
        Model model = new();
        return model.CntProducts(modelName);
    }


    // PUT api/<ModelController>/5
    [HttpPost("addWithproducts/quntity={number}")]
    public void AddModelWithProducts(ModelDto model, int number)
    {
        Model _model = new Model();
        _model.AddModelWithProducts(model.AsNormal(), number);

    }
    [HttpGet("EvaluateRate/")]
    public string EvaluateRate(string modelName)
    {
        Model model = new();
        return model.EvaluateRate(modelName);

    }
    [HttpPut("UpdateIsSale/{modelName}/{value}")]
    public void UpdateIsSale(string modelName, bool value)
    {
        Model model = new();
        model.UpdateIsSale(modelName, value);

    }

    [HttpGet("AvailableProducts/{modelName}/")]
    public int UpdateIsSale(string modelName)
    {
        Model model = new();
        return model.AvailableProducts(modelName);

    }
    [HttpGet("GetAllOrderdBySale/{limit}/")]
    public List<ModelDto> GetAllOrderBySale(int limit)
    {
        Model model = new();
        return model.GetAllOrderedbySale(limit).AsDto();

    }
    [HttpGet("GetAllOrderdByRate/{limit}/")]
    public List<ModelDto> GetAllOrderByRate(int limit)
    {
        Model model = new();
        return model.GetAllOrderedbyRate(limit).AsDto();

    }
    // DELETE api/<ModelController>/5
    [HttpDelete("Delete/{modelName}")]
    public void Delete(string modelName)
    {
        Model model = new();
        model.DeleteModel(modelName);
    }
    [HttpGet("GetProducts/{modelName}")]
    public ActionResult GetProducts(string modelName)
    {
        Model model = new();

        return Ok(model.GetProducts(modelName));
    }
}


