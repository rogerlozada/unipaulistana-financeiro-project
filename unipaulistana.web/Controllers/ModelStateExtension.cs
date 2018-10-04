namespace unipaulistana.web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using unipaulistana.model;
    
    public static class ModelStateExtension
    {
        public static Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary RemoveKeyModelState(this Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState, string key)
        {
            if (modelState.ContainsKey(key))
                modelState[key].Errors.Clear();

            return modelState;
        }
    }
}
