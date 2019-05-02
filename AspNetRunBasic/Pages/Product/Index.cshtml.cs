using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetRunBasic.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetRunBasic.Pages.Product
{
    public class IndexModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public IndexModel(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public IEnumerable<Entities.Product> ProductList { get; set; } = new List<Entities.Product>();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ProductList = await _productRepository.GetProductByNameAsync(SearchTerm);
            return Page();
        }
    }
}