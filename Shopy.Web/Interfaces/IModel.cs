using Shopy.Web.Models;

namespace Shopy.Web.Interfaces;
public interface IModel
{
    Model Get(string name);
    void UpdateName(string modelName, string value);
    void UpdateImagePath(string modelName, string value);
    void UpdatePrice(string modelName, decimal value);
    void UpdateSalePrice(string modelName, decimal value);
    void UpdateBrand(string modelName, string value);
    void UpdateColor(string modelName, string value);
    void UpdateFeatures(string modelName, string value);
    int CntProducts(string modelName);
    void AddModelWithProducts(Model model, int qt);
    void EvaluateRate(string modelName);
    void UpdateIsSale(string modelName, bool value);
    public int AvailableProducts(string modelName);
    public List<Model> GetAllOrderedbySale(int limit);
    public void DeleteModel(string modelName);



}