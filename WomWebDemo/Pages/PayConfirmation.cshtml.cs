using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WomWebDemo.Pages
{
    public class PayConfirmationModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? Discount { get; set; }

        public string DiscountType
        {
            get
            {
                return Discount switch
                {
                    "coffee" => "a freshly brewed espresso coffee",
                    "book" => "a book of your favorite author",
                    "bike" => "renting a bike",
                    "course" => "a language course",
                    _ => "something"
                };
            }
        }

        [BindProperty(SupportsGet = true)]
        public int Amount { get; set; }

        public void OnGet()
        {
        }
    }
}
