using System.Data;
using ClosedXML.Excel;

namespace CommandPattern.WebApp.Commands
{
    public class ExcelFile<T>
    {

        private readonly List<T> list;
        public string FileName => $"{typeof(T).Name}.xlsx";
        public string FileType => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public async Task<MemoryStream> Create()
        {
            var wb = new XLWorkbook();

            var ds = new DataSet();

            ds.Tables.Add(GetTable());

            wb.Worksheets.Add(ds);

            var excelMemory = new MemoryStream();

            wb.SaveAs(excelMemory);
            return excelMemory;


        }

        public ExcelFile(List<T> list)
        {
            this.list = list;
        }

        private DataTable GetTable()
        {
            DataTable table = new();

            var type = typeof(T);
            type.GetProperties().ToList().ForEach(x =>
                table.Columns.Add(x.Name, x.PropertyType)
            );

            list.ForEach(x =>
            {
                var values = type.GetProperties().Select(y => y.GetValue(x, null)).ToArray();
                table.Rows.Add(values);
            });

            return table;
        }
    }
}
