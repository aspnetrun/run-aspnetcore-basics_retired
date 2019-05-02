using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetRunBasic.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel()
        {
        }

        public async Task<IActionResult> OnGet()
        {
            return Page();
        }
    }
}
