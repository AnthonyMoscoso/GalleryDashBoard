using DashboardGallery.Shared.Services.JsFunctions.Index;
using DashboardGallery.Shared.Services.JsFunctions.Interfaces;
using Microsoft.JSInterop;

namespace DashboardGallery.Shared.Services.JsFunctions
{
    public class JsFunctions : IJsFunctions
    {
        private IJSRuntime _jsRuntime;
        private JsIndexFunctions _jsIndexFunctions;
        private JsDropFunctions _JsDropFunctions;
  
       
        public JsFunctions(IJSRuntime jSRuntime) 
        { 
            _jsRuntime = jSRuntime;
            _jsIndexFunctions = new JsIndexFunctions() ;
            _JsDropFunctions = new JsDropFunctions() ;
            


        }

        public async Task<IJsDropFunctions> Drop()
        {
            return await _JsDropFunctions.Build(_jsRuntime);
        }

        public async Task<IJsIndexFunctions> Index()
        {
            return await _jsIndexFunctions.Build(_jsRuntime);
        }

       
    }
}
