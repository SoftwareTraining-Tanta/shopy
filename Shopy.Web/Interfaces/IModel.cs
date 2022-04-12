namespace Shopy.Web.Interfaces;
public interface IModel
{
    void UpdateName(int modelName, string value);
    void UpdateImagePath(int modelName, string value);
    void UpdatePrice(int modelName, decimal value);
    void UpdateSalePrice(int modelName, decimal value);
    void UpdateBrand(int modelName, string value);
    void UpdateColor(int modelName, string value);
    void UpdateFeatures(int modelName, string value);
    float GetRate(int modelName);
    int CntProducts(int modelName);
    void AddProducts(int modelName, int qt);

}