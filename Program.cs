using System;
using System.Collections.Generic;

namespace ProductLibrary
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal Rate { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }

        public static List<Product> products = new List<Product>();

        public static List<Product> GetAllProducts()
        {
            return products;
        }

        public static Product GetSingleProduct(int productId)
        {
            return products.Find(p => p.ProductId == productId);
        }

        public static void Insert(Product product)
        {
            ValidateProduct(product);
            products.Add(product);
        }

        public static void Update(Product product)
        {
            ValidateProduct(product);
            int index = products.FindIndex(p => p.ProductId == product.ProductId);
            if (index != -1)
            {
                products[index] = product;
            }
        }

        public static void Delete(int productId)
        {
            int index = products.FindIndex(p => p.ProductId == productId);
            if (index != -1)
            {
                products.RemoveAt(index);
            }
        }

        private static void ValidateProduct(Product product)
        {
            if (product.ProductId <= 0)
            {
                throw new ProductValidationException("Product ID must be greater than 0.");
            }

            if (string.IsNullOrEmpty(product.ProductName))
            {
                throw new ProductValidationException("Product Name cannot be null or empty.");
            }

            if (product.Rate <= 0)
            {
                throw new ProductValidationException("Rate must be greater than 0.");
            }

            if (product.CategoryId <= 0)
            {
                throw new ProductValidationException("Category ID must be greater than 0.");
            }
        }
    }

    public class ProductValidationException : Exception
    {
        public ProductValidationException(string message) : base(message)
        {
        }
    }
}






namespace ProductConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Choose an action:");
                    Console.WriteLine("1. Get all products");
                    Console.WriteLine("2. Get single product");
                    Console.WriteLine("3. Insert product");
                    Console.WriteLine("4. Update product");
                    Console.WriteLine("5. Delete product");
                    Console.WriteLine("0. Exit");

                    Console.Write("Enter your choice: ");
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            GetAllProducts();
                            break;
                        case 2:
                            GetSingleProduct();
                            break;
                        case 3:
                            InsertProduct();
                            break;
                        case 4:
                            UpdateProduct();
                            break;
                        case 5:
                            DeleteProduct();
                            break;
                        case 0:
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }

                    Console.WriteLine();
                }
            }
            catch (ProductLibrary.ProductValidationException ex)
            {
                Console.WriteLine("Validation Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void GetAllProducts()
        {
            Console.WriteLine("All Products:");
            foreach (var product in ProductLibrary.Product.GetAllProducts())
            {
                Console.WriteLine($"Product ID: {product.ProductId}, Product Name: {product.ProductName}, Rate: {product.Rate}, Category ID: {product.CategoryId}");
            }
        }

        static void GetSingleProduct()
        {
            Console.Write("Enter the Product ID: ");
            int productId = int.Parse(Console.ReadLine());
            var product = ProductLibrary.Product.GetSingleProduct(productId);
            if (product != null)
            {
                Console.WriteLine($"Product ID: {product.ProductId}, Product Name: {product.ProductName}, Rate: {product.Rate}, Category ID: {product.CategoryId}");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        static void InsertProduct()
        {
            Console.Write("Enter Product ID: ");
            int productId = int.Parse(Console.ReadLine());

            Console.Write("Enter Product Name: ");
            string productName = Console.ReadLine();

            Console.Write("Enter Rate: ");
            decimal rate = decimal.Parse(Console.ReadLine());

            Console.Write("Enter Category ID: ");
            int categoryId = int.Parse(Console.ReadLine());

            ProductLibrary.Product.Insert(new ProductLibrary.Product
            {
                ProductId = productId,
                ProductName = productName,
                Rate = rate,
                Description = "This is a sample product.", // Auto property value
                CategoryId = categoryId
            });

            Console.WriteLine("Product inserted successfully.");
        }

        static void UpdateProduct()
        {
            Console.Write("Enter the Product ID to update: ");
            int productId = int.Parse(Console.ReadLine());

            var productToUpdate = ProductLibrary.Product.GetSingleProduct(productId);
            if (productToUpdate != null)
            {
                Console.Write("Enter updated Product Name: ");
                productToUpdate.ProductName = Console.ReadLine();

                Console.Write("Enter updated Rate: ");
                productToUpdate.Rate = decimal.Parse(Console.ReadLine());

                Console.Write("Enter updated Category ID: ");
                productToUpdate.CategoryId = int.Parse(Console.ReadLine());

                ProductLibrary.Product.Update(productToUpdate);
                Console.WriteLine("Product updated successfully.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        static void DeleteProduct()
        {
            Console.Write("Enter the Product ID to delete: ");
            int productId = int.Parse(Console.ReadLine());
            ProductLibrary.Product.Delete(productId);
            Console.WriteLine("Product deleted successfully.");
        }
    }
}