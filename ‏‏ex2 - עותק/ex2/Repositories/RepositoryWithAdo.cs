using ex1.Models;
using ex1.Repositories;
using ex2.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Net.Mail;

namespace ex2.Repositories
{
    public class RepositoryWithAdo: IRepositoryWithAdo
    {
        string Cnn;
        public RepositoryWithAdo(IConfiguration configuration)
        {
            Cnn = configuration.GetConnectionString("DefaultConnection");
        }

        //ADO - EX4 - PROCEDURE
        public DataTable addAttachment(string Route, string AttachmentName, string Description, string size, string endingAttachment)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(Cnn))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    
                    command.CommandText = "Attachment_AddAttachment";
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter sqlParameter1 = new SqlParameter("@Route", Route);
                    SqlParameter sqlParameter2 = new SqlParameter("@AttachmentName", AttachmentName);
                    SqlParameter sqlParameter3 = new SqlParameter("@Description", Description);
                    SqlParameter sqlParameter4 = new SqlParameter("@size", size);
                    SqlParameter sqlParameter5 = new SqlParameter("@endingAttachment", endingAttachment);
                    command.Parameters.Add(sqlParameter1);
                    command.Parameters.Add(sqlParameter2);
                    command.Parameters.Add(sqlParameter3);
                    command.Parameters.Add(sqlParameter4);
                    command.Parameters.Add(sqlParameter5);

                    connection.Open();

                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }


        //ADO - EX5
        public DataTable getTasksbyProjectId(int projectId)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(Cnn))
            {
                using (SqlCommand command = new SqlCommand("select * from Tasks where ProjectId = @projectId", connection))
                {
                    command.CommandType = CommandType.Text;
                    SqlParameter sqlParameter1 = new SqlParameter("@ProjectId", projectId);
                    command.Parameters.Add(sqlParameter1);
                    connection.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        } 
        //transaction
        public bool Transtaction_AddingTaskAndAttachment(AttachmentsAndTasks attachmentAndTask)
        {
            using (SqlConnection connect = new SqlConnection(Cnn))
            {
                connect.Open();
                SqlTransaction transaction = connect.BeginTransaction();
                try
                {
                    using (SqlCommand command1 = new SqlCommand("INSERT INTO Attachments(Route,Description,Size,EndingAttachment,AttachmentName) VALUES(@Route,@Description,@Size,@EndingAttachment,@AttachmentName)", connect, transaction))
                    {
                        command1.Parameters.AddWithValue("@Route", attachmentAndTask.attachment.Route);
                        command1.Parameters.AddWithValue("@Description", attachmentAndTask.attachment.Description);
                        command1.Parameters.AddWithValue("@Size", attachmentAndTask.attachment.Size);
                        command1.Parameters.AddWithValue("@EndingAttachment", attachmentAndTask.attachment.EndingAttachment);
                        command1.Parameters.AddWithValue("@AttachmentName", attachmentAndTask.attachment.AttachmentName);
                        command1.ExecuteNonQuery();
                    }
                    using (SqlCommand command2 = new SqlCommand("INSERT INTO Tasks(Priority,DueDate,Status,ProjectId,UserId) VALUES(@Priority,@DueDate,@Status,@ProjectId,@UserId)", connect, transaction))
                    {
                        command2.Parameters.AddWithValue("@Priority", attachmentAndTask.task.Priority);
                        command2.Parameters.AddWithValue("@DueDate", attachmentAndTask.task.DueDate);
                        command2.Parameters.AddWithValue("@Status", attachmentAndTask.task.Status);
                        command2.Parameters.AddWithValue("@ProjectId", attachmentAndTask.task.ProjectId);
                        command2.Parameters.AddWithValue("@UserId", attachmentAndTask.task.UserId);
                        command2.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}