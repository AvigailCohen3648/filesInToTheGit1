using ex1.Models;
using System.Data;

namespace ex2.Repositories
{
    public interface IRepositoryWithAdo
    {

        //ADO - EX4 - PROCEDURE
        DataTable addAttachment(string Route, string AttachmentName, string Description, string size, string endingAttachment);

        //ADO - EX5
        DataTable getTasksbyProjectId(int ProjectId);
    }
}
