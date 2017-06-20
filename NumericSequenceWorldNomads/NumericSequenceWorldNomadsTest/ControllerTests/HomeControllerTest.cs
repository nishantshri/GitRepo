using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumericSequenceWorldNomads.Services;
using NumericSequenceWorldNomads.Models;
using NumericSequenceWorldNomads.Controllers;
using System.Web.Mvc;

namespace NumericSequenceWorldNomadsTest.ControllerTests
{
    [TestClass]
    public class HomeControllerTest
    {
        #region Positive Test cases
        
        /// <summary>
        /// S3.1.1 All numbers up to and including the number entered
        /// </summary>
        [TestMethod]
        public void IndexPostTestAllNumbers()
        {
            var model = new NumberModel();
            var calc = new CalcNumberSequence();
            var controller = new HomeController() ;
            model.Number = 6;
            var viewResult = controller.Index(model) as PartialViewResult;
            var outputModel = (NumberModel)viewResult.ViewData.Model;
            var result = outputModel.AllNumbers;           
            Assert.AreEqual("1,2,3,4,5,6", result);           
        }

        /// <summary>
        /// S3.1.2 All odd numbers up to and including the number entered
        /// </summary>
        [TestMethod]
        public void IndexPostTestOddNumbers()
        {
            var model = new NumberModel();
            var calc = new CalcNumberSequence();
            var controller = new HomeController();
            model.Number = 6;
            var viewResult = controller.Index(model) as PartialViewResult;
            var outputModel = (NumberModel)viewResult.ViewData.Model;
            var result = outputModel.OddNumbers;
            Assert.AreEqual("1,3,5", result);
            
        }

        /// <summary>
        /// S3.1.3 All even numbers up to and including the number entered
        /// </summary>
        [TestMethod]
        public void IndexPostTestEvenNumbers()
        {
            var model = new NumberModel();
            var calc = new CalcNumberSequence();
            var controller = new HomeController();
            model.Number = 6;
            var viewResult = controller.Index(model) as PartialViewResult;
            var outputModel = (NumberModel)viewResult.ViewData.Model;
            var result = outputModel.EvenNumbers;
            Assert.AreEqual("2,4,6", result);           
        }

        /// <summary>
        /// S3.1.4 All numbers up to and including the number entered, except when
        /// S3.1.4.1 A number is a multiple of 3 output C
        /// S3.1.4.2 A number is a multiple of 5 output E
        /// S3.1.4.3 A number is a multiple of both 3 and 5 output Z
        /// </summary>
        [TestMethod]
        public void IndexPostTestSubstitutedNumbers()
        {
            var model = new NumberModel();
            var calc = new CalcNumberSequence();
            var controller = new HomeController();
            model.Number = 15;
            var viewResult = controller.Index(model) as PartialViewResult;
            var outputModel = (NumberModel)viewResult.ViewData.Model;
            var result = outputModel.SubstitutedNumbers;
            Assert.AreEqual("1,2,C,4,E,C,7,8,C,E,11,C,13,14,Z", result);
        }

        /// <summary>
        ///S3.1.5 All fibonacci number up to and including the number entered
        /// </summary>
        [TestMethod]
        public void IndexPostTestFibonacciNumbers()
        {
            var model = new NumberModel();
            var calc = new CalcNumberSequence();
            var controller = new HomeController();
            model.Number = 6;
            var viewResult = controller.Index(model) as PartialViewResult;
            var outputModel = (NumberModel)viewResult.ViewData.Model;
            var result = outputModel.FibonacciNumbers;
            Assert.AreEqual("0,1,1,2,3,5", result);
        }
        #endregion

        #region Negative Test Cases

        /// <summary>
        /// Passed negative value
        /// </summary>
        [TestMethod]
        public void IndexPostTestNegativeNumnber()
        {
            var model = new NumberModel();
            var calc = new CalcNumberSequence();
            var controller = new HomeController();
            model.Number = -6;
            var viewResult = controller.Index(model) as PartialViewResult;
            var outputModel = (NumberModel)viewResult.ViewData.Model;
            var allNumbers = outputModel.AllNumbers;
            var oddNumbers = outputModel.OddNumbers;
            var evenNumbers = outputModel.EvenNumbers;
            var subsitutedNumbers = outputModel.SubstitutedNumbers;
            var fibonacciNumbers = outputModel.FibonacciNumbers;
            Assert.IsTrue(String.IsNullOrEmpty(allNumbers));
            Assert.IsTrue(String.IsNullOrEmpty(oddNumbers));
            Assert.IsTrue(String.IsNullOrEmpty(evenNumbers));
            Assert.IsTrue(String.IsNullOrEmpty(subsitutedNumbers));
            Assert.IsTrue(String.IsNullOrEmpty(fibonacciNumbers));
        
        }
        #endregion
    }
}
