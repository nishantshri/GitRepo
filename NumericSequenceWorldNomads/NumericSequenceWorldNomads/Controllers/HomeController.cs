using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using NumericSequenceWorldNomads.Models;
using NumericSequenceWorldNomads.Factory;
using NumericSequenceWorldNomads.Interfaces;

namespace NumericSequenceWorldNomads.Controllers
{
    public class HomeController : Controller
    {
        private ICalcNumberSeq _calcNumber;

        /// <summary>
        /// This is the default constructor of HomeController
        /// </summary>
        public HomeController()
        {
            _calcNumber = NumericSequenceFactory.CreateNumberSequenceService();
        }

        /// <summary>
        /// This constructor implemends Dependency Injection using Unity. 
        /// Please see DIConfig class in App_Start folder and Global.asax folder for DI implementation.
        /// This code by default will run on Factory Design Pattern
        /// </summary>
        /// <param name="calcNumber"></param>
        //public HomeController(ICalcNumberSeq calcNumber)
        //{
        //    _calcNumber = calcNumber;
        //}

        /// <summary>
        /// Index action
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Index action for HTTP Post. This is called when user submits the form
        /// </summary>
        /// <param name="numberModel"> This is the model which contains the user entered value</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult Index(NumberModel numberModel)
        {
            int number = 0 ;
            if (ModelState.IsValid)
            {
                try
                {
                    number = numberModel.Number;
                    numberModel.AllNumbers = _calcNumber.GetAllNumbers(number);
                    numberModel.EvenNumbers = _calcNumber.GetAllEvenNumbers(number);
                    numberModel.OddNumbers = _calcNumber.GetAllOddNumbers(number);
                    numberModel.SubstitutedNumbers = _calcNumber.GetConditionNumber(number);
                    numberModel.FibonacciNumbers = _calcNumber.GetFibonacciSeries(number);
                    return PartialView("_Result", numberModel);
                }
                catch (Exception ex)
                {

                    ViewData["errorMsg"] = ex.Message;
                    return View("Error");
                }
             
            }
            return PartialView("_Error",numberModel) ;
        }
    }
}
