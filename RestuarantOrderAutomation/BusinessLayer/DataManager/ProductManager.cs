using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using RestaurantData;

namespace BusinessLayer
{

    public class ProductManager
    {
       public void Create(string pName, double price, int productID = -999)
        {

            using(var db = new Context())
            {
                if(productID == -999)
                {
                    productID =
                    db.Products.Count() + 1;
                }

                var product = new Product() {ProductID = productID,ProductName = pName, Price = price };
                db.Products.Add(product);
                db.SaveChanges();
            }
        }

        public bool Update(int prodID, string pName, double price)
        {
            using (var db = new Context())
            {
                var productToUpdate =
                    db.Products.Where(p => p.ProductID == prodID).FirstOrDefault();
                
                if(productToUpdate == null)
                {
                    Debug.WriteLine($"Product with ID: {prodID} not found");
                    return false;
                }
                else
                {
                    productToUpdate.ProductName = pName;
                    productToUpdate.Price = price;

                    try
                    {
                        db.SaveChanges();

                    }
                    catch(Exception e)
                    {
                        Debug.WriteLine($"Error updating {prodID}");
                        return false;
                    }
                }
            }
            return true;
        }

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
                    return true;
                    
                }
            }
        }
    }
}
