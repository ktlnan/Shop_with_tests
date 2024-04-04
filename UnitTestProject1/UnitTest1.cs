using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using laba4_ptpm;
using System.Collections.Generic;
using Moq;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.IO;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestRegisterUser()
        {
            // Arrange
            var context = new laba4Entities();


            // Создаем форму с учетом контекста
            var form2 = new Form2(context);

            // Act
            bool result = form2.RegisterUser("qwertyu", "2423", "fedsders", "5675238", "user");
            var user = context.User.FirstOrDefault(u => u.Login == "qwertyu");

            // Assert
            Assert.IsTrue(result);
            Assert.IsNotNull(user);
            context.User.Remove(user);
            context.SaveChanges();
        }
        [TestMethod]
        public void TestLog()
        {
            // Arrange
            var context = new laba4Entities();

            // Создаем тестового пользователя
            var id = 0;
            try
            {
                id = context.User.Max(u => u.id) + 1;
            }
            catch { };
            var testUser = new User(id, "qwertyu", "2423", "fedsders", 56775238, "user");

            context.User.Add(testUser);
            context.SaveChanges();


            // Создаем форму с учетом контекста
            var form1 = new Form1(context);

            // Act
            bool result = form1.LogIn("qwertyu", "2423");

            // Assert
            Assert.IsTrue(result); // Проверяем, что вход выполнен успешно
            var user = context.User.FirstOrDefault(u => u.Login == "qwertyu");
            context.User.Remove(user);
            context.SaveChanges();
        }
        [TestMethod]
        public void TestAdd()
        {
            // Arrange
            var context = new laba4Entities();
            string imagePath = @"C:\Users\Катя\Desktop\cleo2.jpg";

            // Получаем байтовый массив изображения
            byte[] imageBytes = File.ReadAllBytes(imagePath);

            // Создаем форму с учетом контекста
            var formA = new FormA(context);

            // Act
            var result = formA.AddProd("cloiiii", 2423787, 67, imageBytes);
            var product = context.Product.FirstOrDefault(u => u.Name == "cloiiii");

            // Assert
            Assert.IsNotNull(result);
            context.Product.Remove(product);
            context.SaveChanges();
        }
        [TestMethod]
        public void TestUpdate()
        {
            // Arrange
            var context = new laba4Entities();
            string imagePath = @"C:\Users\Катя\Desktop\cleo2.jpg";

            // Получаем байтовый массив изображения
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            var product = new Product()
            {
                Name = "cliooo",
                Price = 124454,
                Quantity = 484,
                Image = imageBytes
            };
            context.Product.Add(product);
            context.SaveChanges();

            var newProd = context.Product.First(p => p.Name == "cliooo");

            // Создаем форму с учетом контекста
            var formE = new Edit(context, product);

            // Act
            var result = formE.Update(newProd.Id, "cliyooo", 124454, 484, imageBytes);
            var resproduct = context.Product.FirstOrDefault(u => u.Id == newProd.Id);

            // Assert
            Assert.IsNotNull(result);
            context.Product.Remove(resproduct);
            context.SaveChanges();

        }
        [TestMethod]
        public void TestDelete()
        {
            var context = new laba4Entities();
            string imagePath = @"C:\Users\Катя\Desktop\cleo2.jpg";

            // Получаем байтовый массив изображения
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            var product = new Product()
            {
                Name = "cliooo",
                Price = 124454,
                Quantity = 484,
                Image = imageBytes
            };
            context.Product.Add(product);
            context.SaveChanges();

            var newProd = context.Product.First(p => p.Name == "cliooo");

            // Создаем форму с учетом контекста
            var formt = new FormT(context, product);

            // Act
            var result = formt.Delete(newProd.Id);
            var resproduct = context.Product.FirstOrDefault(u => u.Id == newProd.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.IsNull(resproduct);
            if (resproduct != null)
            {
                context.Product.Remove(resproduct);
                context.SaveChanges();
            }

        }
        [TestMethod]
        public void TestShow()
        {
            var context = new laba4Entities();
            string imagePath = @"C:\Users\Катя\Desktop\cleo2.jpg";

            // Получаем байтовый массив изображения
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            var products = new List<Product>()
        {
            new Product{
                Name = "test1Unit",
                Price = 124454,
                Quantity = 484,
                Image = imageBytes
            },
            new Product{
                Name = "test2Unit",
                Price = 124454,
                Quantity = 484,
                Image = imageBytes
            },
            new Product{
                Name = "test3Unit",
                Price = 124454,
                Quantity = 484,
                Image = imageBytes
            }
        };
            context.Product.AddRange(products);
            context.SaveChanges();

            // Создаем форму с учетом контекста
            var formU = new FormU(context, true);

            // Act
            var result = formU.GetProducts();
          
            // Assert
            Assert.IsTrue(result.Any());
           
            context.Product.Remove(context.Product.First(p => p.Name == "test1Unit"));
            context.Product.Remove(context.Product.First(p => p.Name == "test2Unit"));
            context.Product.Remove(context.Product.First(p => p.Name == "test3Unit"));
            context.SaveChanges();
        }
    }
}
