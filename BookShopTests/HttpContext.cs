using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Web;
using System.Collections.Specialized;
using System.Web.Routing;
using System.Web.Mvc;
using Moq;
using System.ComponentModel.DataAnnotations;


namespace BookShopTest
{
    public static class ValidateModelClass
    {
        /// <summary>
        /// Добавляет в ModelState контроллера ошибки валидации
        /// </summary>
        /// <param name="_controller">Контроллер</param>
        /// <param name="instance">Модель</param>
        public static void ValidateModel(this Controller _controller, object instance)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(instance, null, null);
            Validator.TryValidateObject(instance, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                _controller.ModelState
                  .AddModelError(
                    validationResult.MemberNames.First(),
                    validationResult.ErrorMessage);
            }
        }
    }

    public class ContextMocks
    {
        private IPrincipal _user;

        public Moq.Mock<HttpContextBase> HttpContext { get; set; }
        public Moq.Mock<HttpResponseBase> Response { get; set; }
        public Moq.Mock<HttpRequestBase> Request { get; set; }
        public RouteData RouteData { get; set; }

        public ContextMocks(ControllerBase controller, string url = "http://localhost")
        {
            Response = new Mock<HttpResponseBase>();
            Request = new Mock<HttpRequestBase>();
            HttpContext = new Mock<HttpContextBase>();

            Request.Setup(x => x.AppRelativeCurrentExecutionFilePath).Returns("~");
            Response.Setup(x => x.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(x => x);

            Request.Setup(x => x.Cookies).Returns(new HttpCookieCollection());
            Response.Setup(x => x.Cookies).Returns(new HttpCookieCollection());
            Request.Setup(x => x.QueryString).Returns(new NameValueCollection());
            Request.Setup(x => x.Form).Returns(new NameValueCollection());


            Request.Setup(x => x.ApplicationPath).Returns("/");
            Request.Setup(x => x.Url).Returns(new Uri(url));
            HttpContext.SetupGet(x => x.Request).Returns(Request.Object);
            HttpContext.SetupGet(x => x.Response).Returns(Response.Object);
            HttpContext.Setup(x => x.Session).Returns(new FakeSession());
            controller.ControllerContext = new ControllerContext(HttpContext.Object, new RouteData(), controller);
            //this.IsAjaxRequest(false);
        }

        public void SetUser(string UserName)
        {
            _user = new GenericPrincipal(new GenericIdentity(UserName), null);
            HttpContext.Setup(x => x.User).Returns(_user);
        }

        public void IsAjaxRequest(bool b)
        {
            if (b)
                HttpContext.Setup(r => r.Request["X-Requested-With"]).Returns("XMLHttpRequest");
            else
                HttpContext.Setup(r => r.Request["X-Requested-With"]).Returns("");
        }

        private class FakeSession : HttpSessionStateBase
        {
            readonly Dictionary<string, object> _items = new Dictionary<string, object>();

            public override object this[string name]
            {
                get { return _items.ContainsKey(name) ? _items[name] : null; }
                set
                {
                    _items[name] = value;
                }
            }
        }
    }
}
