// Pages/Index.cshtml.cs
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Threading.Tasks;

namespace webapp_sam_lab13.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly BlobService _blobService;

        // Propriété pour lier le fichier uploadé
        [BindProperty]
        public IFormFile File { get; set; }

        // Propriété pour afficher l'URL du fichier uploadé
        public string FileUrl { get; set; }

        // Constructeur pour injecter BlobService
        public IndexModel(ILogger<IndexModel> logger, BlobService blobService)
        {
            _logger = logger;
            _blobService = blobService;
        }

        // Action qui gère l'upload du fichier
        public async Task<IActionResult> OnPostUploadAsync()
        {
            if (File != null && File.Length > 0)
            {
                using (var stream = File.OpenReadStream())
                {
                    var fileName = Path.GetFileName(File.FileName);
                    // Appel du service BlobService pour uploader le fichier
                    FileUrl = await _blobService.UploadFileAsync(stream, fileName);
                }
            }

            // Retourner la même page avec l'URL du fichier uploadé
            return Page();
        }

        public void OnGet()
        {
        }
    }
}
