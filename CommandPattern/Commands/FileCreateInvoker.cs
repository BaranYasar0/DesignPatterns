using Microsoft.AspNetCore.Mvc;

namespace CommandPattern.WebApp.Commands
{
    public class FileCreateInvoker
    {
        private ITableActionCommand tableActionCommand;
        private List<ITableActionCommand> tableActionCommands = new List<ITableActionCommand>();

        public void SetCommand(ITableActionCommand tableActionCommand) =>
            this.tableActionCommand = tableActionCommand;

        public void AddCommand(ITableActionCommand tableActionCommand) =>
            tableActionCommands.Add(tableActionCommand);

        public IActionResult CreateFile() => tableActionCommand.Execute();

        public List<IActionResult> CreateFiles() => tableActionCommands.Select(x => x.Execute()).ToList();
    }
}
