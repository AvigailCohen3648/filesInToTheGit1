using ex2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ex2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ADOController : ControllerBase
    {
        private readonly IServiceWithAdo _IServiceWithAdo;
        public ADOController(IServiceWithAdo iServiceWithAdo)
        {
            _IServiceWithAdo = iServiceWithAdo;
        }

        //ADO - EX4 - PROCEDURE
        [HttpPost]
        public IActionResult addAttachment(string Route, string AttachmentName, string Description, string size, string endingAttachment)
        {
            return (IActionResult)_IServiceWithAdo.addAttachment(Route, AttachmentName, Description, size, endingAttachment);
        }

        //ADO - EX5
        [HttpGet]
        public DataTable getTasksbyProjectId(int ProjectId)
        {
            return _IServiceWithAdo.getTasksbyProjectId(ProjectId);
        }

    }
}
