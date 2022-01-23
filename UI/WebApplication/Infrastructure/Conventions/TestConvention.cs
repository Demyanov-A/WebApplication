using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Diagnostics;

namespace WebApplication.Infrastructure.Conventions
{
    public class TestConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            Debug.WriteLine(controller.ControllerName);

            //controller.Actions

            //controller.RouteValues
        }
    }
}
