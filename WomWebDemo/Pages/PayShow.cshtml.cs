using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WomWebDemo.Pages
{
    public class PayShowModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public Guid Otc { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Password { get; set; }

        public string Link
        {
            get
            {
                return $"https://{Environment.GetEnvironmentVariable("WOM_DOMAIN")}/api/payment/{Otc:N}";
            }
        }

        public void OnGet()
        {
        }
    }
}
