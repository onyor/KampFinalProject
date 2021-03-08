using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using System;

namespace ConsoleUI
{
    // DTO -> Data Transformation Object
    class Program
    {
        static void Main(string[] args)
        {
            //ProductTest1();
            //ProductTest2();
            //CategoryTest();
            ProductTest3();
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll().Data)
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        private static void ProductTest3()
        {
            ProductManager productManager = new ProductManager(new EfProductDal(),new CategoryManager(new EfCategoryDal()));

            var result = productManager.GetAllByCategoryId(1);
            
            if (result.Success == true)
            {
                foreach (var productDetail in result.Data)
                {
                    Console.WriteLine(productDetail.ProductName);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            
        }


        private static void ProductTest2()
        {
            ProductManager productManager = new ProductManager(new EfProductDal(),new CategoryManager(new EfCategoryDal()));

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
            ProductManager productManager = new ProductManager(new EfProductDal(),new CategoryManager(new EfCategoryDal()));

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
