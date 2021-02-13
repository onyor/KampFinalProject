using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    // DTO -> Data Transformation Object
    class Program
    {
        static void Main(string[] args)
        {
            //ProductTest1();
            ProductTest2();
            //CategoryTest();
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll())
            {
                Console.WriteLine(category.CategoryName);
            }
        }
        private static void ProductTest2()
        {
            ProductManager productManager = new ProductManager(new EfProductDal());

            var result = productManager.GetProductDetails();

            if (result.Success==true)
            {
                foreach (var product in result.Data)
                {
                    Console.WriteLine(product.ProductName + " / " + product.CategoryName);
                }
                Console.WriteLine(result.Message);
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            
        }
        private static void ProductTest1()
        {
            ProductManager productManager = new ProductManager(new EfProductDal());

            // GetByUnitPrice(40,100))
            //foreach (var product in productManager.GetByUnitPrice(40, 100))
            //{
            //    Console.WriteLine(product.UnitsInStock);
            //}


            // ProductDetail
            foreach (var productDetail in productManager.GetProductDetails().Data)
            {
                Console.WriteLine(productDetail.ProductName+" / "+productDetail.CategoryName);
            }
        }
    }
}
