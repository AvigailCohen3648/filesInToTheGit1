using ex1.Models;
using ex2.Models;

namespace ex1.Services
{
    public interface ITasksService
    {
        List<Tasks> GetAllTasks();
        Tasks GetTaskById(int Id);
        Tasks? CreateNewTask(Tasks newTask);
        void DeleteTaskById(int id);
        void UpdateTask(Tasks task);
        List<Tasks> GetTasksOfUserByUserName(string UserName);
    }
}
