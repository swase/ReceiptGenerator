using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using RestaurantData;

namespace BusinessLayer
{

    public class ProductManager
    {
        public Product SelectedProduct { get; set; }

        public void SetSelectedProduct(object selectedItem)
        {
            SelectedProduct = (Product)selectedItem;
        }

        public string GetProductName(object selectedObject)
        {
            var selectedProductName = (Product)selectedObject;
            return selectedProductName.ProductName;
        }
        public List<Product> RetrieveAll()
        {
            using (var db = new Context())
            {
                 return db.Products.ToList();            
            }
        }
        public OrderItem RetrieveForCustSelection(Product product)
        {
            using (var db = new Context())
            {
                var orderItem = new OrderItem()
                {
                    ProductName = product.ProductName,
                    Quantity = 1,
                    UnitPrice = product.Price * (1 - product.Discount)
                };
                orderItem.SetTotal();
                return orderItem;
            }   
        }
        public void Create(string pName, double price)
        {

            using(var db = new Context())
            {
                //Ceck For product Duplicates
                var queryIfProductInCatalogue =
                    db.Products.Where(p => p.ProductName == pName).FirstOrDefault();
                if (queryIfProductInCatalogue == null)
                {
                    var product = new Product() {ProductName = pName, Price = price };
                    db.Products.Add(product);
                    db.SaveChanges();
                    
                }  
            }
        }

        public bool Update(string pName, string newName, double price)
        {
            using (var db = new Context())
            {
                var productToUpdate =
                    db.Products.Where(p => p.ProductName == pName).FirstOrDefault();
                
                if(productToUpdate == null)
                {
                    Debug.WriteLine($"Product {pName} not found");
                    return false;
                }
                else
                {
                    productToUpdate.ProductName = newName;
                    productToUpdate.Price = price;

                    try
                    {
                        db.SaveChanges();

                    }
                    catch(Exception e)
                    {
                        Debug.WriteLine($"Error updating {pName}");
                        return false;
                    }
                }
            }
            return true;
        }

        //Remove using prodID
        public bool Delete(int prodID)
        {
            using (var db = new Context())
            {
                var productToDelete =
                    db.Products.Where(p => p.ProductID == prodID).FirstOrDefault();
                if (productToDelete == null)
                {
                    Debug.WriteLine($"Problem removing product with ID: {prodID}");
                    return false;
                }

                else
                {
                    try
                    {
                        db.Products.RemoveRange(productToDelete);
                        db.SaveChanges();
                    }
                    catch(Exception e)
                    {
                        Debug.WriteLine($"Problem removing product with ID: {prodID}");
                        return false;
                    }                                      
                }
            }
            return true;
        }

        //Remove using productName
        public bool Delete(string pName)
        {
            using (var db = new Context())
            {
                var productToDelete =
                    db.Products.Where(p => p.ProductName == pName).FirstOrDefault();
                if (productToDelete == null)
                {
                    Debug.WriteLine($"Problem removing product {pName}");
                    return false;
                }

                else
                {
                    try
                    {
                        db.Products.RemoveRange(productToDelete);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine($"Problem removing product {pName}");
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
