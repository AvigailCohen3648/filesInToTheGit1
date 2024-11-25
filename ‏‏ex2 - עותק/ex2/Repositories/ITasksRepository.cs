using ex1.Models;
using ex2.Models;

namespace ex1.Repositories
{
    public interface ITasksRepository
    {
        List<Tasks> GetAllTasks();
        Tasks GetTaskById(int Id);
        Tasks? CreateNewTask(Tasks Task);
        void DeleteTaskById(int id);
        void UpdateTask(Tasks task);
        List<Tasks> GetTasksOfUserByUserName(string UserName);
        //transaction
        //bool Transtaction_AddingTaskAndAttachment(AttachmentsAndTasks attachmentAndTask);
    }
}