using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity.Mvc3;
using Microsoft.Practices.Unity;
using NumericSequenceWorldNomads.Interfaces;
using NumericSequenceWorldNomads.Services;
using System.Web.Mvc;

namespace NumericSequenceWorldNomads
{
    public class DIConfig
    {
        public static void RegisterComponent()
        {
            var unityContainer = new UnityContainer();
            unityContainer.RegisterType<ICalcNumberSeq, CalcNumberSequence>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(unityContainer));
        }
        
    }
}