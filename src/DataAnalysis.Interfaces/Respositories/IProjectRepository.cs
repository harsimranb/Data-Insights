using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAnalysis.DomainObjects.Common;

namespace DataAnalysis.Interfaces.Respositories
{
    public interface IProjectRepository
    {
        void CreateProject(string name, string description);
        List<Project> GetAllProjects();
        Project GetSingleById(int projectId);
        void UpdateInfo(int projectId, string name, string description);
    }
}
