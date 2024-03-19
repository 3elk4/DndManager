using System.IO;

namespace Application.Common.Models
{
    public class PdfResult
    {
        public string Filename { get; set; }

        public MemoryStream MemoryStream { get; set; }
    }
}
