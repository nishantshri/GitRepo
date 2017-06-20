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
        
        #region ICalcNumberSeq Members

        public string GetAllNumbers(int number)
        {
            return GetNumbers(Convert.ToInt32(EnumValues.StartNumber), number, i => i.ToString(), i => ++i);
        }

        public string GetAllOddNumbers(int number)
        {
            return GetNumbers(Convert.ToInt32(EnumValues.StartNumber), number, i => i.ToString(), i => i+2);
        }

        public string GetAllEvenNumbers(int number)
        {
            return GetNumbers(Convert.ToInt32(EnumValues.StartEvenNumber), number, i => i.ToString(), i => i + 2);
        }

        public string GetFibonacciSeries(int number)
        {
            int current = 0;
            int next = 1;
            return GetNumbers(0, number, i => i.ToString(), i => { int temp = next; next = current + next; current = temp; return current; });            
        }

        public string GetConditionNumber(int number)
        {
            return GetNumbers(Convert.ToInt32(EnumValues.StartNumber), number, i => i % 15 == 0 ? "Z" : i % 3 == 0 ? "C" : i % 5 == 0 ? "E" : i.ToString(), i => ++i);
        }

        #endregion

        private static string GetNumbers(int startNum, int number, Func<int, string> convert, Func<int, int> increment)
        {
            var sb = new StringBuilder();
            
            try
            {
               while (startNum <= number)
                {
                    if (sb.Length > 0)
                        sb.Append(",");
                    sb.Append(convert(startNum));
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