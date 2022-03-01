using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WomWebDemo.Pages
{
    public class RedeemModel : PageModel
    {
        public RedeemModel(
            WomPlatform.Connector.Instrument instrument
        )
        {
            Instrument = instrument;
        }

        public WomPlatform.Connector.Instrument Instrument { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response = await Instrument.RequestVouchers(new WomPlatform.Connector.Models.VoucherCreatePayload.VoucherInfo[]
            {
                new WomPlatform.Connector.Models.VoucherCreatePayload.VoucherInfo
                {
                    Aim = "0",
                    Count = 1,
                    CreationMode = WomPlatform.Connector.Models.VoucherCreatePayload.VoucherCreationMode.SetLocationOnRedeem,
                    Timestamp = DateTime.Now,
                }
            });

            return RedirectToPage("RedeemShow", new
            {
                Otc = response.OtcGen,
                Password = response.Password,
            });
        }
    }
}
