using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using RestaurantData;

namespace BusinessLayer
{

    public class OrderManager
    {
        public static int Create(double subtotal, string status)
        {

            using (var db = new Context())
            {
                try
                {
                    var o = new Order() { Subtotal = subtotal, Status =  status, OrderItemID = null};
                    db.Orders.Add(o);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error trying to add orderItem");
                }
                return db.Orders.Count();
            }
        }

        public bool Update(int orderID, double subtotal, string status, int orderItemID)
        {
            using (var db = new Context())
            {
                var orderToUpdate =
                    db.Orders.Where(o => o.OrderID == orderID).FirstOrDefault();

                if (orderToUpdate == null)
                {
                    Debug.WriteLine($"Product {orderID} not found");
                    return false;
                }
                else
                {
                    orderToUpdate.Subtotal = subtotal;
                    orderToUpdate.Status = status;
                    orderToUpdate.OrderItemID = orderItemID;

                    try
                    {
                        db.SaveChanges();

                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine($"Error updating {orderID}");
                        return false;
                    }
                }
            }
            return true;
        }

        public bool Delete(int orderID)
        {
            using (var db = new Context())
            {
                var orderToDelete =
                    db.Orders.Where(o => o.OrderID == orderID).FirstOrDefault();
                if (orderToDelete == null)
                {
                    Debug.WriteLine($"Problem removing orderItem with ID: {orderID}");
                    return false;
                }

                else
                {
                    try
                    {
                        db.Orders.RemoveRange(orderToDelete);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine($"Problem removing orderItem with ID: {orderID}");
                        return false;
                    }
                }
            }
            return true;
        }
    }
}

