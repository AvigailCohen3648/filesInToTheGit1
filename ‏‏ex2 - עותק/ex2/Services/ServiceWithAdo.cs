using ex1.Repositories;
using ex2.Models;
using ex2.Repositories;
using System.Data;

namespace ex2.Services
{
    public class ServiceWithAdo:IServiceWithAdo
    {
        private readonly IRepositoryWithAdo _IRepositoryWithAdo;

        public ServiceWithAdo(IRepositoryWithAdo iRepositoryWithAdo)
        {
            _IRepositoryWithAdo = iRepositoryWithAdo;
        }

        //ADO - EX4 - PROCEDURE
        public DataTable addAttachment(string Route, string AttachmentName, string Description, string size, string endingAttachment)
        {
            return _IRepositoryWithAdo.addAttachment(Route, AttachmentName, Description, size, endingAttachment);
        }

        //ADO - EX5
        public DataTable getTasksbyProjectId(int ProjectId)
        {
            return _IRepositoryWithAdo.getTasksbyProjectId(ProjectId);
        }
        //transaction
        public bool Transtaction_AddingTaskAndAttachment(AttachmentsAndTasks attachmentAndTask)
        {
            return _IRepositoryWithAdo.Transtaction_AddingTaskAndAttachment(attachmentAndTask);
        }
    }

}
