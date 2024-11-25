using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Net.NetworkInformation;
using ex1.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using ex1.Services;
using ex2.Models;
using ex2.Repositories;
using ex2.Services;
using System.Data;
namespace ex1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _ITaskService;
        
        public TasksController(ITasksService iTaskService)
        {
            _ITaskService = iTaskService;
        }

        
        [HttpGet]
        public ActionResult<IEnumerable<Tasks>> GetAllTasks()
        {
            return Ok(_ITaskService.GetAllTasks());
        }

        
        [HttpGet("{Id}")]
        public IActionResult GetTasksById(int Id)
        {
             var task=_ITaskService.GetTaskById(Id);
            if (task == null)
                return NotFound();
            return Ok(task);
        }

        
        [HttpPost]
        public IActionResult CreateNewTask([FromBody] Tasks newTask)
        {
            Tasks? didAddedTask = _ITaskService.CreateNewTask(newTask);
                if (didAddedTask == null)
                    return BadRequest();
                else
                    return Ok(didAddedTask);
        }
        
        [HttpDelete("{id}")]
        public void DeleteTaskById(int id)
        {
             _ITaskService.DeleteTaskById(id);
        }

        [HttpPut]
        public void UpdateTask([FromBody] Tasks task)
        {
            _ITaskService.UpdateTask(task);
        }

        [Route("api/Tasks/GetTasksOfUserByUserName/{UserName}")]
        [HttpGet]
        public IActionResult GetTasksOfUserByUserName(string UserName)
        {
            return Ok(_ITaskService.GetTasksOfUserByUserName(UserName));
        }

    //    [Route("api/Tasks/Transtaction_AddingTaskAndAttachment")]
    //    //transaction
    //    [HttpPost]
    //    public ActionResult<bool> Transtaction_AddingTaskAndAttachment([FromBody]AttachmentsAndTasks attachmentAndTask)
    //    {
    //        return _ITaskService.Transtaction_AddingTaskAndAttachment(attachmentAndTask);
    //    }
    }
}

