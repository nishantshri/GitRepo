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
using AFDDevTestApplication.Models;

namespace AFDDevTestApplication.Controllers
{
    public class UploadController : Controller
    {
        

        public ActionResult Index()
        {
            var dbModel = new DBModel();
            if (dbModel.GetCustomerData())
            {
                var strMessage = "Data is already present in the Database. <br></br> You must delete the data before uploading new file ";
                ViewBag.HtmlStr = strMessage;
                ViewData["IsDataPresent"] = true;
            }
            return View();
        }


        public ActionResult Delete()
        {
            var dbModel = new DBModel();
            int count = dbModel.DeleteCustomerData();
            String msg = "Total Records deleted:" + count;
            ViewData["msg"] = msg;
            return View("index");
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase fileUpload)
        {
            string Message = String.Empty;
            bool flag = false;
            DataSet dataSet = new DataSet();
            List<CustomerModel> customerModel = new List<CustomerModel>();
            try
            {
                var dbModel = new DBModel();
                if (dbModel.GetCustomerData())
                {
                    var strMessage = "Data is already present in the Database. <br></br> You must delete the data before uploading new file ";
                    ViewBag.Feedback = "";
                    ViewBag.HtmlStr = strMessage;
                    ViewData["IsDataPresent"] = true;
                    return View();
                }
                if (fileUpload.ContentLength > 0)
                {
                    var fileExtension = Path.GetExtension(fileUpload.FileName);
                    if (String.Equals(fileExtension, ".csv", StringComparison.OrdinalIgnoreCase))
                    {
                        var fileName = Path.GetFileName(fileUpload.FileName);
                        string path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                        fileUpload.SaveAs(path);
                        DBModel db = new DBModel();
                        var messageModel = db.ProcessCSV(path, fileName);
                        dataSet = db.GetAllCustomerData();
                        //foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                        //{
                        //    customerModel.Add(new CustomerModel
                        //    {
                        //        Property = dataRow[0].ToString(),
                        //        CustomerName = dataRow[1].ToString(),
                        //        Value = Convert.ToInt32(dataRow[2]),
                        //        Action = dataRow[3].ToString(),
                        //        File = dataRow[4].ToString(),
                        //        Status = dataRow[5].ToString(),
                        //        Hash = dataRow[6].ToString()

                        //    });
                        //}

                        //if (customerModel.Count > 0)
                        //{
                        //    Session["customerModel"] = customerModel;
                        //}
                        Message = messageModel.Message;
                        flag = messageModel.Flag;
                        ViewBag.Feedback = Message;
                        //ViewData["Feedback"] = Message;
                    }
                    else
                    {
                        ViewBag.Feedback = "Please select CSV file";
                    }
                }
                else
                {
                    ViewBag.Feedback = "Please select a file";
                }

               
                return View();
                
            }
            catch (Exception ex)
            {
                ViewBag.Feedback = "Please select a file";
                return View();
            }
        }
        
    }
}
