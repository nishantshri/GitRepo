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
    public class ShowController : Controller
    {
        //
        // GET: /Show/

        public ActionResult Index()
        {
            var db = new DBModel();
            DataSet dataSet = new DataSet();
            List<CustomerModel> customerModel = new List<CustomerModel>();
            dataSet = db.GetAllCustomerData();
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                customerModel.Add(new CustomerModel
                {
                    Property = dataRow[0].ToString(),
                    CustomerName = dataRow[1].ToString(),
                    Value = Convert.ToInt32(dataRow[2]),
                    Action = dataRow[3].ToString(),
                    File = dataRow[4].ToString(),
                    Status = dataRow[5].ToString(),
                    Hash = dataRow[6].ToString()

                });
            }
            

            return View(customerModel);
        }

      

       
    }
}
