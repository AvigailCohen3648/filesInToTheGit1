using ex1.Models;
using ex1.Repositories;
using ex2.Models;
using System.Net.NetworkInformation;

namespace ex1.Services
{
    public class TasksService : ITasksService
    {
        private readonly ITasksRepository _ITasksRepository;

        public TasksService(ITasksRepository ITasksRepository)
        {
            _ITasksRepository = ITasksRepository;
        }

        public List<Tasks> GetAllTasks()
        {
            return _ITasksRepository.GetAllTasks();
        }

        public Tasks GetTaskById(int Id)
        {
            return _ITasksRepository.GetTaskById(Id);
        }

        public Tasks? CreateNewTask(Tasks newTask)
        {
            return _ITasksRepository.CreateNewTask(newTask);
        }
         public void DeleteTaskById(int id)
        {
            _ITasksRepository.DeleteTaskById(id);
        }


        public void UpdateTask(Tasks task)
        {
            _ITasksRepository.UpdateTask(task);
        }

        public List<Tasks> GetTasksOfUserByUserName(string UserName)
        {
            return _ITasksRepository.GetTasksOfUserByUserName(UserName);
        }
     }
}
