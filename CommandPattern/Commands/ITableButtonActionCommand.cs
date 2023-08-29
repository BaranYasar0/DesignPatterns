using Microsoft.AspNetCore.Mvc;

namespace CommandPattern.WebApp.Commands
{
    public interface ITableActionCommand
    {
        IActionResult Execute();
    }
}
