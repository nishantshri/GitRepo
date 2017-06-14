using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.Helpers;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Threading;
using AFDDevTestApplication.Models;

namespace AFDDevTestApplication.Controllers
{
    public class SendController : Controller
    {
       

        public ActionResult Index()
        {
            var dbModel = new DBModel();
            if (!dbModel.GetCustomerData())
            {
                ViewData["IsDataPresent"] = false;
                ViewData["Errmsg"] = "No data is present";
                return View();
            }
            else
            {
                ViewData["IsDataPresent"] = true;
                ViewData["msg"] = "Please Click on the Send Button to send data present in the database to Remote API";
            }
            return View();
        }

        public JsonResult CheckStatus()
        {
            string Message = "hi";
            bool flag = true;
            var dbModel = new DBModel();
            try
            {
                var customerStatus = dbModel.GetCustomerStatus();
                Message = "Total records processing :" + customerStatus.totalCount;
                Message += "<br/>Total records processed :" + customerStatus.totalUpdate;
                Message += "<br/> Please wait..We are still processing..";
            }
            catch (Exception ex)
            {

                Message = "Error :" + ex.Message;
            }
           
            return Json(new
            {
                Data = Message,
                Status = flag
            }, JsonRequestBehavior.AllowGet);
           
        }
        
        public JsonResult SendApi()
        {
            string Message = "No records to Process";
            bool flag = true;
            int apiCount = 0;            
            Object thisLock = new Object();
            try
            {
                var db = new DBModel();
                var customerDetails = db.GetCustomerList();
                
                if (customerDetails != null)
                {
                     var url = @"http://evilapi-env.ap-southeast-2.elasticbeanstalk.com/upload";
                     foreach (var customerDetail in customerDetails)
                     {
                         var webRequest = (HttpWebRequest)WebRequest.Create(url);
                         webRequest.Method = "POST";
                         webRequest.ContentType = "application/json";
                         var json = Json(customerDetail).Data;
                        
                         string jsonReq = "{";
                         jsonReq = jsonReq + "\"property\":";
                         jsonReq = jsonReq + " \"" + customerDetail.Property + "\",";
                         jsonReq = jsonReq + "\"customer\":";
                         jsonReq += " \"" + customerDetail.CustomerName + "\",";
                         jsonReq = jsonReq + "\"action\":";
                         jsonReq += " \"" + customerDetail.Action + "\",";
                         jsonReq = jsonReq + "\"value\":";
                         jsonReq = jsonReq + customerDetail.Value + ",";
                         //jsonReq += " \"" + customerDetail.Value + "\",";
                         jsonReq = jsonReq + "\"file\":";
                         jsonReq += " \"" + customerDetail.File + "\" }";

                         using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                         {
                             streamWriter.Write(jsonReq);
                         }
                         var httpResponse = (HttpWebResponse)webRequest.GetResponse();

                         DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Response));
                         object objResponse = jsonSerializer.ReadObject(httpResponse.GetResponseStream());
                         Response jsonResponse = objResponse as Response;
                         jsonResponse.value = customerDetail.Value;                         
                         apiCount++;
                         ThreadPool.QueueUserWorkItem(UpdateCustomerHash, jsonResponse);
                         
                     }
                     Message = "YIPPEE!!!Request Completed..Total records processed :" + apiCount;
                }
            }
            catch (Exception ex)
            {

                Message = "Error :" +ex.Message;
            }
            return Json(new
            {
                Data = Message,
                Status = flag
            }, JsonRequestBehavior.AllowGet);
          
        }

        private void UpdateCustomerHash(Object obj)
        {
            var response = obj as Response;
            var db = new DBModel();
            var flagUpdate = db.UpdateResponse(response);
        }
       
    }
}
