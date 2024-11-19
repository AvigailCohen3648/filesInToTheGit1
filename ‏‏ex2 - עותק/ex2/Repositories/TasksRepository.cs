using ex1.Models;
using ex2.Models;
using ex2.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.Json;

namespace ex1.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private string _filePath = "tasks.json";
        private readonly TasksdbContext _context;

        public TasksRepository(TasksdbContext context)
        {
            _context = context;
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
                return Task;
            }
            else
                return null;
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
