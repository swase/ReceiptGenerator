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
                where p.ProductName.Contains("TestProduct")
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
                _pm.Create("TestProduct", 15.5);
                int newCount = db.Products.Count();
                Assert.That(originalCount + 1, Is.EqualTo(newCount));
            }
        }

        [Test]
        public void WhenANewProductIsAdded_DetailsAreCorrect()
        {
            _pm.Create("TestProduct", 15.5);
            using (var db = new Context())
            {
                var testProductAddQuery =
                    db.Products.Where(p => p.ProductName == "TestProduct").FirstOrDefault();
                Assert.That(testProductAddQuery, Is.Not.Null);
                Assert.That(15.5, Is.EqualTo(testProductAddQuery.Price));
            }
        }

        [Test]
        public void WhenAProductNameIsChanged_TheDataBaseIsUpdated()
        {
            _pm.Create("TestProduct", 15.5);
            

            using (var db = new Context())
            {
                var selectProduct =
                    db.Products.Where(p => p.ProductName == "TestProduct").Select(p => p.ProductID).FirstOrDefault();

                _pm.Update("TestProduct", "TestProductNewName", 14.0);
                var selectUpdatedProduct =
                    db.Products.Where(p => p.ProductID == selectProduct).FirstOrDefault();

                Assert.That("TestProductNewName", Is.EqualTo(selectUpdatedProduct.ProductName));
                Assert.That(14.0, Is.EqualTo(selectUpdatedProduct.Price));

            }
        }
        [Test]
        public void WhenTryingToUpdateProductThatDoesntExist_returnsFalse()
        {
            Assert.IsFalse(_pm.Update("TestProduct", "TestProductNewName", 14.0));

        }

        [Test]
        public void WhenAProductIsDeleted_TotalProductCountDecreasesBy1()
        {
            _pm.Create("TestProduct", 15.5);
            using (var db = new Context())
            {
                int productCountAfterAdding =
                    db.Products.Count();
                _pm.Delete("TestProduct");
                Assert.That(productCountAfterAdding - 1, Is.EqualTo(db.Products.Count()));
            }
        }

        [Test]
        public void WhenAProductIsDeleted_DataBaseIsUpdated()
        {
            _pm.Create("TestProduct", 15.5);
            
            using(var db = new Context())
            {
                var countAfterAdding =
                    db.Products.Count();

                _pm.Delete("TestProduct");
                var countAfterDeleting = db.Products.Count();

                Assert.That(countAfterAdding - 1, Is.EqualTo(countAfterDeleting));
            }
        }

        [Test]
        public void WhenTryingToDeleteANonExistingProduct_returnsFalse()
        {
            Assert.IsFalse(_pm.Delete("TestProduct"));     
        }




        [TearDown]
        public void TearDown()
        {
            using (var db = new Context())
            {
                var selectedProduct =
                from p in db.Products
                where p.ProductName.Contains("TestProduct")
                select p;

                db.Products.RemoveRange(selectedProduct);
                db.SaveChanges();
            }

        }
    }
}
