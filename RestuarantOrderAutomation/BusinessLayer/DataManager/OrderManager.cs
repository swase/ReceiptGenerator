using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using RestaurantData;
using System.Collections;
using System.Collections.Generic;

namespace BusinessLayer
{

    public class OrderManager
    {
        public static List<Order> RetrieveAllActiveOrders()
        {
            using (var db = new Context())
            {
                return db.Orders.Where(o => o.Status != OrderStatus.Collected && o.Status != OrderStatus.Ordering).ToList();
            }
        }

        public static int Create(double subtotal, OrderStatus status)
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

        public bool Update(int orderID, double subtotal, OrderStatus status)
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
                    orderToUpdate.OrderItemID = null;

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
                var orderDetailsToDelete =
                    db.OrderDetails.Where(od => od.OrderID == orderID);
                if (orderToDelete == null)
                {
                    Debug.WriteLine($"Problem removing orderItem with ID: {orderID}");
                    return false;
                }

                else
                {
                    try
                    {
                        db.OrderDetails.RemoveRange(orderDetailsToDelete);
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


        public int GetNumOrdersForStatus(OrderStatus status)
        {
            //Returns list of order details for one order
            using (var db = new Context())
            {
                var queryNumberOrdersWithStatus =
                db.Orders.Where(od => od.Status == status);

                if(queryNumberOrdersWithStatus != null)
                {
                    return queryNumberOrdersWithStatus.Count();
                }
                else
                {
                    return 0;
                }
                
            }
        }
    }
}

