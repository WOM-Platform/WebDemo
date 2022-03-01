using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WomWebDemo.Pages
{
    public class PayModel : PageModel
    {
        public PayModel(
            WomPlatform.Connector.PointOfSale pos
        )
        {
            Pos = pos;
        }

        public WomPlatform.Connector.PointOfSale Pos { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(
            string demoDiscount,
            int demoAmount
        )
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var ackUrlQuery = new QueryBuilder
            {
                { "discount", demoDiscount },
                { "amount", demoAmount.ToString() }
            };
            var ackUrl = $"https://{Environment.GetEnvironmentVariable("SELF_HOST")}/confirm{ackUrlQuery}";

            var response = await Pos.RequestPayment(1, ackUrl, filter: new WomPlatform.Connector.Models.SimpleFilter
            {
                Aim = "0"
            });

            return RedirectToPage("PayShow", new
            {
                Otc = response.OtcPay,
                Password = response.Password,
            });
        }
    }
}
