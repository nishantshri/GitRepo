using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using NumericSequenceWorldNomadsTest.Framework;

namespace NumericSequenceWorldNomadsTest.UITests
{
    [TestClass]
    public class UITests
    {
        [TestMethod]
        public void EndtoEndTest()
        {
            try
            {
                string number = "Number";
                string inputNumber = "6";
                string strAllNumber = "1,2,3,4,5,6";
                string strOddNumber = "1,3,5";
                string strEvenNumber = "2,4,6";
                string strsubsNumber = "1,2,C,4,E,C";
                string strFibonacci = "0,1,1,2,3,5";
                var driver = Host.Instance.WebDriver;
                driver.Navigate().GoToUrl(driver.Url);
                driver.FindElement(By.Id(number)).Clear();
                driver.FindElement(By.Id(number)).SendKeys(inputNumber);
                driver.FindElement(By.Id("btnSubmit")).Click();
                System.Threading.Thread.Sleep(3000);
                var allNumber = driver.FindElementById("allnumbers");
                var oddNumber = driver.FindElementById("oddnumbers");
                var evenNumber = driver.FindElementById("evennumbers");
                var subsNumber = driver.FindElementById("substitutednumbers");
                var fibonacciNumber = driver.FindElementById("fibonaccinumbers");
                Assert.AreEqual(strAllNumber, allNumber.Text);
                Assert.AreEqual(strOddNumber, oddNumber.Text);
                Assert.AreEqual(strEvenNumber, evenNumber.Text);
                Assert.AreEqual(strsubsNumber, subsNumber.Text);
                Assert.AreEqual(strFibonacci, fibonacciNumber.Text);
            }
            catch (Exception ex)
            {
                
                throw;
            }
           

        }
    }
}
