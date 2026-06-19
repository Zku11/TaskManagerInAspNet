using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaskManagerInAspNet.Servicios
{
    public class Constants
    {
        public const string AdminRole = "admin";
        public static readonly SelectListItem[] SupportedUiCultures = new SelectListItem[]
        {
            new SelectListItem{Value = "es", Text="Español"},
            new SelectListItem{Value = "en", Text="English"}
        };
    }
}
