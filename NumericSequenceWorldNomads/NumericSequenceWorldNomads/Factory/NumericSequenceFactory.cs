using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NumericSequenceWorldNomads.Interfaces;
using NumericSequenceWorldNomads.Services;

namespace NumericSequenceWorldNomads.Factory
{
    public static class NumericSequenceFactory
    {
        public static ICalcNumberSeq CreateNumberSequenceService()
        {
            return new CalcNumberSequence();
        }
    }
}