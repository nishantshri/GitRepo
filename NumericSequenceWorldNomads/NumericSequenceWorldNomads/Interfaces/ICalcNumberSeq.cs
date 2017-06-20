using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericSequenceWorldNomads.Interfaces
{
    public interface ICalcNumberSeq
    {
        string GetAllNumbers(int number);
        string GetAllOddNumbers(int number);
        string GetAllEvenNumbers(int number);
        string GetFibonacciSeries(int number);
        string GetConditionNumber(int number);
    }
}
