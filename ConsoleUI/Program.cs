
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entitities.Concrete;

//ProductTest();
//CategoryTest();
ProductTest2();



//static void ProductTest()
//{
//    ProductManager productManager = new ProductManager(new EfProductDal());

//    foreach (var product in productManager.GetAllByCategoryId(2))   //2 numaralı kategoriideki ürünleri ver testi
//    {
//        Console.WriteLine(product.ProductName);
//    }
//}



static void CategoryTest()
{
    CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());

    foreach (var category in categoryManager.GetAll().Data)
    {
        Console.WriteLine(category.CategoryName);   //categoryleri ismine göre listeleme testi
    }
}





static void ProductTest2()
{
    ProductManager productManager = new ProductManager(new EfProductDal(),new CategoryManager(new EfCategoryDal()));

    var result = productManager.GetProductDetails();

    if(result.Success ==true)
    foreach (var product in result.Data)   //2 tablodan dataları join edip hem productname hemde categoryname verisini çekme testi
    {
        Console.WriteLine(product.ProductName + "/" + product.CategoryName);
    }
    else
    {
        Console.WriteLine(result.Message);
    }
}