using Microsoft.AspNetCore.Mvc;

namespace CommandPattern.WebApp.Commands
{
    public class CreateExcelTableActionCommand<T> : ITableActionCommand
    {
        private readonly ExcelFile<T> excelFile;

        public CreateExcelTableActionCommand(ExcelFile<T> excelFile)
        {
            this.excelFile = excelFile;
        }

        public async Task<IActionResult> ExecuteAsync()
        {
            var excelMemoryStream =await excelFile.Create();

            return new FileContentResult(excelMemoryStream.ToArray(), excelFile.FileType){FileDownloadName = excelFile.FileName};
        }

        public IActionResult Execute()
        {
            throw new NotImplementedException();
        }
    }
}
