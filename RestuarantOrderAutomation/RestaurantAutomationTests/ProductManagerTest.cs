using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RestaurantData;
using BusinessLayer;

namespace RestaurantAutomationTests
{
    public class ProductManagerTest
    {
        ProductManager _pm;
        [SetUp]
        public void Setup()
        {
            // remove test entry in DB if present
            _pm = new ProductManager();
            using (var db = new Context())
            {
                var selectedProduct =
                from p in db.Products
                where p.ProductID == 8008145
                select p;

                db.Products.RemoveRange(selectedProduct);
                db.SaveChanges();
            }
        }

        [Test]
        public void WhenANewProductIsAdded_NumberOfProductsIncreasesByOne()
        {
            using (var db = new Context())
            {
                int originalCount = db.Products.Count();
                _pm.Create("TestProduct", 15.5, 8008145);
                Assert.That(++originalCount, Is.EqualTo(db.Products.Count()));

                //Remove TestProduct
            }
        }

        [Test]
        public void WhenANewProductIsAdded_DetailsAreCorrect()
        {
            _pm.Create("TestProduct", 15.5, 8008145);
            using (var db = new Context())
            {
                var testProductAddQuery =
                    db.Products.Where(p => p.ProductID == 8008145).FirstOrDefault();
                Assert.That("TestProduct", Is.EqualTo(testProductAddQuery.ProductName));
                Assert.That(15.5, Is.EqualTo(testProductAddQuery.Price));
            }
        }

        [Test]
        public void WhenAProductNameIsChanged_TheDataBaseIsUpdated()
        {
            _pm.Create("TestProduct", 15.5, 8008145);
            _pm.Update(8008145,"TestProductNewName", 14.0);

            using (var db = new Context())
            {
                var selectUpdatedProduct =
                    db.Products.Where(p => p.ProductID == 8008145).First();
                Assert.That("TestProductNewName", Is.EqualTo(selectUpdatedProduct.ProductName));
                Assert.That(14.0, Is.EqualTo(selectUpdatedProduct.Price));

            }
        }
        [Test]
        public void WhenTryingToUpdateProductThatDoesntExist_returnsFalse()
        {
            Assert.IsFalse(_pm.Update(8008145, "TestProductNewName", 14.0));
        }

        [Test]
        public void WhenAProductIsDeleted_TotalProductCountDecreasesBy1()
        {
            _pm.Create("TestProduct", 15.5, 8008145);
            using (var db = new Context())
            {
                int productCountAfterAdding =
                    db.Products.Count();
                _pm.Delete(8008145);
                Assert.That(productCountAfterAdding - 1, Is.EqualTo(db.Products.Count()));
            }
        }

        [Test]
        public void WhenAProductIsDeleted_DataBaseIsUpdated()
        {
            _pm.Create("TestProduct", 15.5, 8008145);
            
            using(var db = new Context())
            {
                var countAfterAdding =
                    db.Products.Count();

                _pm.Delete(8008145);
                var countAfterDeleting = db.Products.Count();

                Assert.That(countAfterAdding - 1, Is.EqualTo(countAfterDeleting));
            }
        }




        [TearDown]
        public void TearDown()
        {
            using (var db = new Context())
            {
                var selectedProduct =
                from p in db.Products
                where p.ProductID == 8008145
                select p;

                db.Products.RemoveRange(selectedProduct);
                db.SaveChanges();
            }

        }
    }
}
