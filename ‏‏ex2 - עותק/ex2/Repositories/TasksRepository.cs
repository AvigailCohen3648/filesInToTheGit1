using ex1.Models;
using ex2.Models;
using ex2.Repositories;
using ex2.Services.Logger;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using TasksApi.Services.Logger;

namespace ex1.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private string _filePath = "tasks.json";
        private readonly TasksdbContext _context;
        private readonly DBLoggerService _DBLoggerService;
        private readonly TasksApi.Services.Logger.LoggerFactory _LoggerFactory;
        private  ILoggerService _logger;

        //string Cnn;
        //public TasksRepository(IConfiguration configuration)
        //{
        //    Cnn = configuration.GetConnectionString("DefaultConnection");
        //}

        public TasksRepository(TasksdbContext context, DBLoggerService dbLoggerService, TasksApi.Services.Logger.LoggerFactory loggerFactory)
        {
            _context = context;
            _DBLoggerService = dbLoggerService;
            _LoggerFactory = loggerFactory;
        }

        public List<Tasks> GetAllTasks()
        {
            return _context.Tasks.ToList();
        }

        public Tasks GetTaskById(int Id)
        {
            return _context.Tasks.Find(Id);
        }

        public Tasks? CreateNewTask(Tasks Task)
        {
            User? isExistUser = _context.Users.ToList().FirstOrDefault(u => u.UserId == Task.UserId);
            Project? isExistProject = _context.Projects.ToList().FirstOrDefault(p => p.ProjectId == Task.ProjectId);

            if(isExistUser!=null && isExistProject!=null){
                _context.Tasks.Add(Task);
                _context.SaveChanges();
                //_DBLoggerService.GetLogger(1);// GetLogger
                _logger = _LoggerFactory.GetLogger(1);//שליחה 1=כתיבת לכונסול 2=לקובץ 3=למסד נתונים
                _logger.Log("המשימה נוספה בהצלחה");
                return Task;
            }
            else
                return null;
        }
        public void logIntoDB(string message)
        {
            Messages newMassage= new Messages();
            newMassage.Description = message;
            newMassage.Update_Date = DateTime.Now;
            _context.Messages.Add(newMassage);
        }
        public void DeleteTaskById(int id)
        {
            Tasks? task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
        }

        public void UpdateTask(Tasks task)
        {
            try
            {
                 _context.Tasks.Update(task);
                   _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public List<Tasks>? GetTasksOfUserByUserName(string UserName)
        {
            User? currUser = _context.Users.FirstOrDefault(u => u.UserName == UserName);
            if (currUser != null)
            {
                List<Tasks>? tasksOfUser = _context.Tasks.Where(t => t.UserId == currUser.UserId).ToList();
                return tasksOfUser;
            }
            return null;
        }
    }
}
