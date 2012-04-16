using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookShopWeb.Controllers;
using BookShopModel.ContainerMock;
using BookShopModel;
using System.Web.Mvc;
using BookShopModel.Model;

namespace BookShopTest
{
    [TestClass]
    public class ControllerTests
    {
        private HomeController controller;
        ContextMocks myContext;
        ModelContainerMock modelContainer;
        Repository repo;

        [TestInitialize]
        public void MyTestInitialize()
        {
            modelContainer = new ModelContainerMock();
            controller = new HomeController(modelContainer);
            myContext = new ContextMocks(controller);

            repo = new Repository(modelContainer);
            repo.CreateUser("TestUser", "123456", "TestEmail");
            myContext.SetUser("TestUser");
            repo.CreateTheme(new Theme { Id = 1, ThemeName = "TestTheme" });
            repo.CreateAuthor(new Author { Id = 1, FirstName = "TestName", LastName = "TestLastName" });
            repo.CreateBook(new Book { Id = 1, AuthorId = 1, ThemeId = 1, Price = 100, BookName = "TestBook" });
            repo.CreateBook(new Book { Id = 2, AuthorId = 1, ThemeId = 1, Price = 150, BookName = "TestBook2" });
            repo.CreateSale("TestUser", new Sale { BookId = 1, Quantity = 2, UserId = 0 });
            repo.CreateSale("TestUser", new Sale { BookId = 2, Quantity = 1, UserId = 0 });
        }
        [TestMethod]
        public void Get_List_All_Books()
        {
            var view = controller.Index();
            Assert.IsTrue(view is ViewResult, "Неправильный тип ViewResult");
            Assert.IsTrue(controller.ViewData.Model is IEnumerable<Book>, "Неправильный тип модели");
            Assert.IsNotNull(controller.ViewBag.SalesHistory, "Нет истории продаж");
            Assert.IsTrue(controller.ViewBag.SalesHistory is IEnumerable<Sale>, "Неправильный тип истории продаж");
        }
        [TestMethod]
        public void Buy_Book_Get()
        {
            var view = controller.BuyBook(1);
            Assert.IsTrue(view is ViewResult, "Неправильный тип ViewResult");
            Assert.IsTrue(controller.ViewData.Model is Sale, "Неправильный тип модели");
        }
        [TestMethod]
        public void Buy_Book_Get_Ajax()
        {
            myContext.IsAjaxRequest(true);
            var view = controller.BuyBook(1);
            Assert.IsTrue(view is PartialViewResult, "Неправильный тип ViewResult");
            Assert.IsTrue(controller.ViewData.Model is Sale, "Неправильный тип модели");
        }
        [TestMethod]
        public void Buy_Book_Post()
        {
            Sale sale = new Sale { BookId = 1, Id = 3, Quantity = 2, Date = DateTime.Now };
            controller.ValidateModel(sale);
            var view = controller.BuyBook(sale);
            Assert.IsTrue(controller.ModelState.IsValid, "Инвалидная модель");
            Assert.IsTrue(view is ViewResult, "Неправильный тип ViewResult");
            Assert.IsNotNull(controller.ViewBag.Message, "Нет сообщения об успешной покупке");
        }
        [TestMethod]
        public void Buy_Book_Post_Ajax()
        {
            myContext.IsAjaxRequest(true);
            Sale sale = new Sale { BookId = 1, Id = 3, Quantity = 2, Date = DateTime.Now };
            controller.ValidateModel(sale);
            var view = controller.BuyBook(sale);
            Assert.IsTrue(controller.ModelState.IsValid, "Инвалидная модель");
            Assert.IsTrue(view is PartialViewResult, "Неправильный тип ViewResult");
            Assert.IsNotNull(controller.ViewBag.Message, "Нет сообщения об успешной покупке");
        }
        [TestMethod]
        public void Buy_Book_Post_NotValidModelState()
        {
            Sale sale = new Sale { BookId = 1, Id = 3};
            controller.ValidateModel(sale);
            var view = controller.BuyBook(sale);
            Assert.IsFalse(controller.ModelState.IsValid, "Валидная модель");
            Assert.IsTrue(view is ViewResult, "Неправильный тип ViewResult");
            Assert.IsNull(controller.ViewBag.Message, "Есть сообщение об успешной покупке");
        }
        [TestMethod]
        public void Buy_Book_Post_Ajax_NotValidModelState()
        {
            myContext.IsAjaxRequest(true);
            Sale sale = new Sale { BookId = 1, Id = 3 };
            controller.ValidateModel(sale);
            var view = controller.BuyBook(sale);
            Assert.IsFalse(controller.ModelState.IsValid, "Валидная модель");
            Assert.IsTrue(view is PartialViewResult, "Неправильный тип ViewResult");
            Assert.IsNull(controller.ViewBag.Message, "Есть сообщение об успешной покупке");
        }
    }
}
