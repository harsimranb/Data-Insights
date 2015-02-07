using System;
using System.Web.Mvc;
using DataAnalysis.DomainObjects.Common;
using DataAnalysis.DomainObjects.Exceptions;
using DataAnalysis.Interfaces.Services;
using DataAnalysis.Web.Admin.Framework;

namespace DataAnalysis.Web.Admin.Controllers
{
    public class ProjectController : Controller
    {
        #region Fields

        private readonly IProjectService _projectService;

        #endregion

        #region Constructor

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        #endregion

        #region GET

        public ActionResult GetAll()
        {
            var result = _projectService.GetAllProjects();
            return new JsonNetResult(true)
            {
                Data = result
            };
        }

        public ActionResult Get(int? projectId)
        {
            if (!projectId.HasValue)
            {
                // TODO: return errorResult
                throw new BasicException("No project specified.", "projectId is null.");
            }

            Project project = _projectService.GetSingleById(projectId.Value);
            if (project == null)
            {
                // TODO: return errorResult
                throw new BasicException("THe specified project no longer exists.", string.Format("Project with id {0} not found.", projectId.Value));
            }

            return new JsonNetResult()
            {
                Data = project
            };;
        }

        #endregion

        #region POST

        [HttpPost]
        public ActionResult Create(Project project)
        {
            Exception ex;
            if (!validateCreateProjectRequest(project, out ex))
            {
                // TODO: return errorResult
                throw ex;
            }

            _projectService.CreateProject(project.Name, project.Description);
            return new JsonResult();
        }

        [HttpPost]
        public ActionResult Edit(Project project)
        {
            Exception ex;
            if (!validateCreateProjectRequest(project, out ex))
            {
                // TODO: return errorResult
                throw ex;
            }

            _projectService.UpdateInfo(project.Id, project.Name, project.Description);
            return new JsonResult();
        }

        #endregion

        #region Private Methods

        private bool validateCreateProjectRequest(Project viewModel, out Exception ex)
        {
            ex = null;

            if (viewModel == null)
            {
                ex = new BasicException("Invalid data was submitted. Please try again.", "CreateProjectViewModel is null.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(viewModel.Name))
            {
                ex = new BasicException("Project Name is required. Please try again.", "CreateProjectViewModel.Project.Name is null or empty.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(viewModel.Description))
            {
                ex = new BasicException("Project Description is required. Please try again.", "CreateProjectViewModel.Project.Name is null or empty.");
                return false;
            }

            return true;
        }

        #endregion
    }
}