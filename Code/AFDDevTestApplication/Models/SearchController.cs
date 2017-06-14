using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Web.Script.Serialization;

namespace AFDDevTestApplication.Models
{
    public class SearchController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult SearchCustomer(string name)
        {
            string message = string.Empty;
            bool flag = true;
            var dbModel = new DBModel();
            List<CustomerModel> customerModel = null;
            try
            {
                customerModel = dbModel.GetCustomerData(2, name);
                if (customerModel == null || customerModel.Count == 0)
                {
                    message = "No records found";
                    flag = false;
                }

            }
            catch (Exception ex)
            {

                message = ex.Message;
                flag = false;
            }
            
            return Json(new
            {
                Data = customerModel,
                Message = message,
                Status = flag
            }, JsonRequestBehavior.AllowGet);
            //return Json(customerModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CheckHash(string hash)
        {
            var url = @"http://evilapi-env.ap-southeast-2.elasticbeanstalk.com/check?hash="+hash;
            string streamReader = string.Empty;
            var customerModel = new CustomerModel();
            string errMsg = string.Empty;
            bool flag = true;
            try
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = "GET";
                webRequest.ContentType = "application/text";
                using (var httpResponse = (HttpWebResponse)webRequest.GetResponse())
                {
                    using (var stream = httpResponse.GetResponseStream())
                    {
                        streamReader = new StreamReader(stream).ReadToEnd();
                        JavaScriptSerializer j = new JavaScriptSerializer();
                        object obj = j.Deserialize(streamReader, typeof(object));
                        Dictionary<string, object> dictionary = new Dictionary<string, object>();
                        dictionary = (Dictionary<string, object>)obj;
                        foreach (var dict in dictionary)
                        {
                            string key = dict.Key;
                            if (key == "value")
                                customerModel.Value = Convert.ToInt32(dict.Value);
                            if (key == "property")
                                customerModel.Property = dict.Value.ToString();
                            if (key == "customer")
                                customerModel.CustomerName = dict.Value.ToString();
                            if (key == "action")
                                customerModel.Action = dict.Value.ToString();
                            if (key == "file")
                                customerModel.File = dict.Value.ToString();
                            if (key == "hash")
                                customerModel.Hash = dict.Value.ToString();
                        }

                    }
                }
                           
               
            }
            
            catch (Exception ex)
            {
                errMsg = ex.Message;
                flag = false;
            }
            return Json(new
            {
                Message = customerModel,
                Error = errMsg,
                Flag = flag
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCustomer(int value)
        {
            var dbModel = new DBModel();
                       
            var customer = new CustomerModel();
            try
            {
                customer = dbModel.GetCustomerData(3, value);
            }
            catch (Exception)
            {
                
                throw;
            }
            
            return Json(customer, JsonRequestBehavior.AllowGet);
        }
    }
}
