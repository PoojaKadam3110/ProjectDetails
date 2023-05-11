using ProjectDetailsAPI.Data.Command;

namespace ProjectDetails.Test
{
    internal interface IMediator
    {
        void Send(AddProjectsCommand addProjectsCommand, object value);
    }
}