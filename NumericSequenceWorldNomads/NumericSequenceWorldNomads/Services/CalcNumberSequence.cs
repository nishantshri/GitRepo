using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using NumericSequenceWorldNomads.Interfaces;
using NumericSequenceWorldNomads.Enum;

namespace NumericSequenceWorldNomads.Services
{
    public class CalcNumberSequence : ICalcNumberSeq
    {
        
        #region Public Members

        public string GetAllNumbers(int number)
        {
            return GetNumbers(Convert.ToInt32(EnumValues.StartNumber), number, i => ++i);
        }

        public string GetAllOddNumbers(int number)
        {
            return GetNumbers(Convert.ToInt32(EnumValues.StartNumber), number, i => i+2);
        }

        public string GetAllEvenNumbers(int number)
        {
            return GetNumbers(Convert.ToInt32(EnumValues.StartEvenNumber), number, i => i + 2);
        }

        public string GetFibonacciSeries(int number)
        {
            int current = 0;
            int next = 1;
            return GetNumbers(0, number, i => { int temp = next; next = current + next; current = temp; return current; });            
        }

        public string GetConditionNumber(int number)
        {
            return GetNumbers(Convert.ToInt32(EnumValues.StartNumber),number , i => ++i,true);
        }

        #endregion

        private static string GetNumbers(int startNum, int number, Func<int, int> increment,bool? isSubstitute = false)
        {
            var sb = new StringBuilder();
            string result = string.Empty;
           
            try
            {
               while (startNum <= number)
                {
                    if (sb.Length > 0)
                        sb.Append(",");
                    if (isSubstitute.Value)
                        result = startNum % 15 == 0 ? "Z" : startNum % 3 == 0 ? "C" : startNum % 5 == 0 ? "E" : startNum.ToString();
                    else
                        result = startNum.ToString();
                    sb.Append(result);
                    startNum = increment(startNum);
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
           
        }
    }
}
