using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using DataAnalysis.DomainObjects.Common;
using DataAnalysis.Interfaces.Respositories;
using DataAnalysis.Interfaces.Services;

namespace DataAnalysis.Infrastructure.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void CreateProject(string name, string description)
        {
            _projectRepository.CreateProject(name, description);
        }

        public void UpdateInfo(int projectId, string name, string description)
        {
            _projectRepository.UpdateInfo(projectId, name, description);
        }

        public List<Project> GetAllProjects()
        {
            return _projectRepository.GetAllProjects();
        }

        public Project GetSingleById(int projectId)
        {
            return _projectRepository.GetSingleById(projectId);
        }
    }
}
