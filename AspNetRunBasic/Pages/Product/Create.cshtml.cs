using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetRunBasic.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNetRunBasic.Pages.Product
{
    public class CreateModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public CreateModel(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var categories = await _productRepository.GetCategories();
            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Entities.Product Product { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Product = await _productRepository.AddAsync(Product);
            return RedirectToPage("./Index");
        }
    }
}