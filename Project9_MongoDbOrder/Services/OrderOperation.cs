using MongoDB.Bson;
using MongoDB.Driver;
using Project9_MongoDbOrder.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project9_MongoDbOrder.Services
{
    public class OrderOperation
    {
        public void AddOrder(Order order)
        {
            var connection = new MongoDbConnection();
            var ordercollection = connection.GetOrdersCollection();
            var document = new BsonDocument
            {
                { "CustomerName", order.CustomerName },
                { "District", order.District },
                { "City", order.City },
                { "TotalPrice", order.TotalPrice }
            };
            ordercollection.InsertOne(document);
        }

        public List<Order> GetAllOrders()
        {
            var connection = new MongoDbConnection();
            var ordercollection = connection.GetOrdersCollection();
            var orders = ordercollection.Find(new BsonDocument()).ToList();
            List<Order> orderList = new List<Order>();
            foreach (var order in orders)
            {
                orderList.Add(new Order
                {
                    OrderId = order["_id"].ToString(),
                    CustomerName = order["CustomerName"].ToString(),
                    District = order["District"].ToString(),
                    City = order["City"].ToString(),
                    TotalPrice = decimal.Parse(order["TotalPrice"].ToString())
                });
            }
            return orderList;
        }

        public void DeleteOrder(string orderId)
        {
            var connection = new MongoDbConnection();
            var ordercollection = connection.GetOrdersCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id",  ObjectId.Parse(orderId));
            ordercollection.DeleteOne(filter);
        }

        public void UpdateOrder(string orderId, Order order)
        {
            var connection = new MongoDbConnection();
            var ordercollection = connection.GetOrdersCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id",  ObjectId.Parse(orderId));
            var update = Builders<BsonDocument>.Update
                .Set("CustomerName", order.CustomerName)
                .Set("District", order.District)
                .Set("City", order.City)
                .Set("TotalPrice", order.TotalPrice);
            ordercollection.UpdateOne(filter, update);
        }


        public Order GetOrderById(string orderId)
        {
            var connection = new MongoDbConnection();
            var ordercollection = connection.GetOrdersCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(orderId));
            var order = ordercollection.Find(filter).FirstOrDefault();
            if (order != null)
            {
                return new Order
                {
                    OrderId = order["_id"].ToString(),
                    CustomerName = order["CustomerName"].ToString(),
                    District = order["District"].ToString(),
                    City = order["City"].ToString(),
                    TotalPrice = decimal.Parse(order["TotalPrice"].ToString())
                };
            }
            else
            {
                return null;
            }
        }
    }
}
