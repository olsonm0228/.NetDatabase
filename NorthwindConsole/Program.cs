using System;
using NLog.Web;
using System.IO;
using System.Linq;
using NorthwindConsole.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace NorthwindConsole
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Program started");

            try
            {
                var db = new NorthwindConsole_31_mdoContext();
                int id = 0;
                string choice;
                String displayChoice;
                do
                {
                    //todo use nlog
                    //todo new product
                    //todo edit specific product
                    //todo display all records in products table (productname only) - user decides if they want to see all products, discontinued products, or active (not discontinued) products
                    //todo display a specific product (display all product fields)
                    //todo
                    //todo edit a category
                    //todo

                    Console.WriteLine("1) Add Product");
                    Console.WriteLine("2) Edit A Product");
                    Console.WriteLine("3) Display All Products");
                    Console.WriteLine("4) Display Specific Product");
                    Console.WriteLine("5) Add A Category");
                    Console.WriteLine("6) Edit A Category");
                    Console.WriteLine("7) Display Categories");
                    Console.WriteLine("8) Display Category and related products");
                    Console.WriteLine("9) Display all Categories and their related products");
                    Console.WriteLine("\"q\" to quit");
                    choice = Console.ReadLine();
                    Console.Clear();
                    logger.Info($"Option {choice} selected");

                    switch(choice){
                        case "1": // Add Product
                            logger.Info("1) selected");
                            var query1 = db.Categories.OrderBy(p => p.CategoryId);

                            Console.WriteLine("Select the category you want to add a product to:");
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            foreach (var item in query1)
                            {
                                Console.WriteLine($"{item.CategoryId}) {item.CategoryName}");
                            }
                            Console.ForegroundColor = ConsoleColor.White;
                            id = int.Parse(Console.ReadLine());
                            Console.Clear();
                            logger.Info($"CategoryId {id} selected");

                            if (db.Categories.Any(c => c.CategoryId == id))
                            {
                                logger.Info("Validation passed, category id exists");
                                Products product = new Products();
                                product.CategoryId = id;
                                var querySupplier = db.Suppliers.OrderBy(p => p.SupplierId);

                                Console.WriteLine("Select the supplier of your product:");
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                foreach (var item in querySupplier)
                                {
                                    Console.WriteLine($"{item.SupplierId}) {item.CompanyName}");
                                }
                                Console.ForegroundColor = ConsoleColor.White;
                                id = int.Parse(Console.ReadLine());
                                Console.Clear();
                                logger.Info($"CategoryId {id} selected");

                                if (db.Suppliers.Any(c => c.SupplierId == id))
                                {
                                    logger.Info("Validation passed, supplier id exists");
                                    product.SupplierId = id;

                                    Console.WriteLine("What is the products name?");
                                    String productName = Console.ReadLine();
                                    Console.WriteLine("What is the quantity per unit for this product?");
                                    String quantityPerUnit = Console.ReadLine();
                                    Console.WriteLine("What is the unit price for this product (##.##)?");
                                    decimal unitPrice = decimal.Round(decimal.Parse(Console.ReadLine()),2);
                                    Console.WriteLine("How many units are in stock (##)?");
                                    short stock = short.Parse(Console.ReadLine());

                                    if(productName.Equals("")||quantityPerUnit.Equals("")){
                                        logger.Error("Empty values are not accepted");
                                    } else{
                                        if (db.Products.Any(c => c.ProductName == productName))
                                        {
                                            logger.Error("That product name already exists");
                                        }
                                        else
                                        {
                                            //setting default values for a new product
                                            product.UnitsOnOrder = 0;
                                            product.ReorderLevel = 0;
                                            product.Discontinued = false;

                                            product.ProductName = productName;
                                            product.QuantityPerUnit = quantityPerUnit;
                                            product.UnitPrice = unitPrice;
                                            product.UnitsInStock = stock;

                                            db.AddProduct(product);
                                            logger.Info($"{product.ProductName} was added");
                                        }
                                    }
                                } else {
                                    logger.Error("There was not a supplier with that id");
                                }
                            }
                            else
                            {
                                logger.Error("There was not a category with that id");
                            }
                            break;
                        case "2": // Edit a product ----------------------------------------------------------
                            logger.Info("2) selected");

                            break;
                        case "3": // Display all products - all, discontinued, or acitve
                      


                            

                            break;
                        case "4": // Display specific product
                            logger.Info("4) selected");
                            break;
                        case "5": // Add a category
                            logger.Info("5) selected");
                            Categories category5 = new Categories();
                            Console.WriteLine("Enter Category Name:");
                            category5.CategoryName = Console.ReadLine();
                            Console.WriteLine("Enter the Category Description:");
                            category5.Description = Console.ReadLine();

                            ValidationContext context = new ValidationContext(category5, null, null);
                            List<ValidationResult> results = new List<ValidationResult>();

                            var isValid = Validator.TryValidateObject(category5, context, results, true);
                            if (isValid)
                            {
                                //var db = new NorthwindConsole_31_mdoContext();
                                // check for unique name
                                if (db.Categories.Any(c => c.CategoryName == category5.CategoryName))
                                {
                                    // generate validation error
                                    isValid = false;
                                    results.Add(new ValidationResult("Name exists", new string[] { "CategoryName" }));
                                }
                                else
                                {
                                    //todo figure out why this only works with console write line.... both of them
                                    logger.Info("Validation passed");
                                    Console.WriteLine(category5.CategoryName +" "+category5.Description +"//");
                                    Console.WriteLine(category5.CategoryId);
                                    db.AddCategory(category5);
                                    logger.Info($"{category5.CategoryName} was added");
                                }
                            }
                            if (!isValid)
                            {
                                foreach (var result in results)
                                {
                                    logger.Error($"{result.MemberNames.First()} : {result.ErrorMessage}");
                                }
                            }
                            break;
                        case "6": //Edit A Category
                            logger.Info("6) selected");
                            break;
                        case "7": //Display Categories
                            logger.Info("7) selected");
                            //var db = new NorthwindConsole_31_mdoContext();
                            var query7 = db.Categories.OrderBy(p => p.CategoryName);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"{query7.Count()} records returned");
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            foreach (var item in query7)
                            {
                                Console.WriteLine($"{item.CategoryName} - {item.Description}");
                            }
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case "8": //Display Category and related products
                            logger.Info("8) selected");
                            //var db = new NorthwindConsole_31_mdoContext();
                            var query8 = db.Categories.OrderBy(p => p.CategoryId);

                            Console.WriteLine("Select the category whose products you want to display:");
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            foreach (var item in query8)
                            {
                                Console.WriteLine($"{item.CategoryId}) {item.CategoryName}");
                            }
                            Console.ForegroundColor = ConsoleColor.White;
                            id = int.Parse(Console.ReadLine());
                            Console.Clear();
                            logger.Info($"CategoryId {id} selected");
                            Categories category8 = db.Categories.Include("Products").FirstOrDefault(c => c.CategoryId == id);
                            Console.WriteLine($"{category8.CategoryName} - {category8.Description}");
                            foreach (Products p in category8.Products)
                            {
                                Console.WriteLine(p.ProductName);
                            }
                            break;
                        case "9": //Display all Categories and their related products
                            logger.Info("9) selected");
                            //var db = new NorthwindConsole_31_mdoContext();
                            var query9 = db.Categories.Include("Products").OrderBy(p => p.CategoryId);
                            foreach (var item in query9)
                            {
                                Console.WriteLine($"{item.CategoryName}");
                                foreach (Products p in item.Products)
                                {
                                    Console.WriteLine($"\t{p.ProductName}");
                                }
                            }
                            break;
                        default:
                            break;
                    }

                    Console.WriteLine();

                } while (choice.ToLower() != "q");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            logger.Info("Program ended");
        }
    }
}
