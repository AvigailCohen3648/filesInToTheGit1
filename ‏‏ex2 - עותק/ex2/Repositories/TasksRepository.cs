using ex1.Models;
using ex2.Models;
using ex2.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;

namespace ex1.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private string _filePath = "tasks.json";
        private readonly TasksdbContext _context;
        string Cnn;
        //public TasksRepository(IConfiguration configuration)
        //{
        //    Cnn = configuration.GetConnectionString("DefaultConnection");
        //}

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
        ////transaction
        //public bool Transtaction_AddingTaskAndAttachment(AttachmentsAndTasks attachmentAndTask)
        //{
        //    using (SqlConnection connect = new SqlConnection(Cnn))
        //    {
        //        connect.Open();
        //        SqlTransaction transaction = connect.BeginTransaction();
        //        try
        //        {
        //            using (SqlCommand command1 = new SqlCommand("INSERT INTO Attachment(Route,Description,Size,EndingAttachment,AttachmentName) VALUES(@Route,@Description,@Size,@EndingAttachment,@AttachmentName)", connect, transaction))
        //            {
        //                command1.Parameters.AddWithValue("@Route", attachmentAndTask.attachment.Route);
        //                command1.Parameters.AddWithValue("@Description", attachmentAndTask.attachment.Description);
        //                command1.Parameters.AddWithValue("@Size", attachmentAndTask.attachment.Size);
        //                command1.Parameters.AddWithValue("@EndingAttachment", attachmentAndTask.attachment.EndingAttachment);
        //                command1.Parameters.AddWithValue("@AttachmentName", attachmentAndTask.attachment.AttachmentName);
        //                command1.ExecuteNonQuery();
        //            }
        //            using (SqlCommand command2 = new SqlCommand("INSERT INTO Tasks(Priority,DueDate,Status,ProjectId,UserId) VALUES(@Priority,@DueDate,@Status,@ProjectId,@UserId)", connect, transaction))
        //            {
        //                command2.Parameters.AddWithValue("@Priority", attachmentAndTask.task.Priority);
        //                command2.Parameters.AddWithValue("@DueDate", attachmentAndTask.task.DueDate);
        //                command2.Parameters.AddWithValue("@Status", attachmentAndTask.task.Status);
        //                command2.Parameters.AddWithValue("@ProjectId", attachmentAndTask.task.ProjectId);
        //                command2.Parameters.AddWithValue("@UserId", attachmentAndTask.task.UserId);
        //                command2.ExecuteNonQuery();
        //            }
        //            transaction.Commit();
        //            return true;
        //        }
        //        catch
        //        {
        //            transaction.Rollback();
        //            return false;
        //        }
        //    }
        //}
    }
}
