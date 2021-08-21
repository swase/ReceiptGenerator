using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantData;

namespace BusinessLayer.DataManager
{
    class OrderDetailManager
    {

        public static List<OrderDetail> GetOrderDetails(int orderID)
        {
            //Returns list of order details for one order
            using (var db = new Context())
            {
            var CurrentOrderItems =
            db.OrderDetails.Where(od => od.OrderID == orderID);
            return CurrentOrderItems.ToList();               
            }
        }


        public void Create(int quantity, int productID, int orderID)
        {
            using (var db = new Context())
            {
                try
                {
                    var od = new OrderDetail() { Quantity = quantity, ProductID = productID, OrderID = orderID };
                    db.OrderDetails.Add(od);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error trying to add orderItem");
                }                              
            }
        }

        public bool Update(int orderItemID, int quantity)
        {
            using (var db = new Context())
            {
                var OrderItemToUpdate =
                    db.OrderDetails.Where(od => od.OrderItemID == orderItemID).FirstOrDefault();

                if (OrderItemToUpdate == null)
                {
                    Debug.WriteLine($"Product {orderItemID} not found");
                    return false;
                }
                else
                {
                    OrderItemToUpdate.Quantity = quantity;

                    try
                    {
                        db.SaveChanges();

                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine($"Error updating {orderItemID}");
                        return false;
                    }
                }
            }
            return true;
        }

        public bool Delete(int orderItemID)
        {
            using (var db = new Context())
            {
                var orderItemToDelete =
                    db.OrderDetails.Where(od => od.ProductID == orderItemID).FirstOrDefault();
                if (orderItemToDelete == null)
                {
                    Debug.WriteLine($"Problem removing orderItem with ID: {orderItemID}");
                    return false;
                }

                else
                {
                    try
                    {
                        db.OrderDetails.RemoveRange(orderItemToDelete);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine($"Problem removing orderItem with ID: {orderItemID}");
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
