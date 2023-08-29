using Microsoft.AspNetCore.Mvc;
using WebApp.Command.Commands;

namespace CommandPattern.WebApp.Commands
{
    public class CreatePdfTableActionCommand<T> : ITableActionCommand
    {
        private readonly PdfFile<T> pdfFile;

        public CreatePdfTableActionCommand(PdfFile<T> pdfFile)
        {
            this.pdfFile = pdfFile;
        }

        public IActionResult Execute()
        {
            MemoryStream pdfMemoryStream = pdfFile.Create();

            return new FileContentResult(pdfMemoryStream.ToArray(), pdfFile.FileType) { FileDownloadName = pdfFile.FileName };
        }
    }
}
