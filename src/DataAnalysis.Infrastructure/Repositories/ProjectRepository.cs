using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataAnalysis.DomainObjects.Common;
using DataAnalysis.DomainObjects.DataSource;
using DataAnalysis.DomainObjects.Exceptions;
using DataAnalysis.Infrastructure.Framework;
using DataAnalysis.Interfaces.Respositories;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace DataAnalysis.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IDataSourceRepository _dataSourceRepository;

        public ProjectRepository(IDataSourceRepository dataSourceRepository)
        {
            _dataSourceRepository = dataSourceRepository;
        }

        public void CreateProject(string name, string description)
        {
            try
            {
                var db = SqlDatabaseFactory.Create(Constants.ConnectionStrings.Core);

                var command = db.GetStoredProcCommand("[dbo].[pr_CreateProject]");
                db.AddInParameter(command, "@name", SqlDbType.NVarChar, name);
                db.AddInParameter(command, "@description", SqlDbType.NVarChar, description);

                db.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw new BasicDataException(string.Format("An unexpected error occurred while creating the project. ERROR: {0}", ex.Message), ex);
            }
        }

        public void UpdateInfo(int projectId, string name, string description)
        {
            try
            {
                var db = SqlDatabaseFactory.Create(Constants.ConnectionStrings.Core);

                var command = db.GetStoredProcCommand("[dbo].[pr_UpdateProjectInfo]");
                db.AddInParameter(command, "@project_id", SqlDbType.Int, projectId);
                db.AddInParameter(command, "@name", SqlDbType.NVarChar, name);
                db.AddInParameter(command, "@description", SqlDbType.NVarChar, description);

                db.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw new BasicDataException(string.Format("An unexpected error occurred while updating the project. ERROR: {0}", ex.Message), ex);
            }
        }

        public List<Project> GetAllProjects()
        {
            try
            {
                List<Project> resultSet;

                var db = SqlDatabaseFactory.Create(Constants.ConnectionStrings.Core);
                var command = db.GetStoredProcCommand("[dbo].[pr_GetAllProjects]");
                
                using (var result = db.ExecuteDataSet(command))
                {
                    var defaultTable = result.Tables[0];
                    resultSet = new List<Project>(defaultTable.Rows.Count);
                    resultSet.AddRange(from DataRow row in defaultTable.Rows 
                                       select convertDataRowToProject(row));
                }

                return resultSet;
            }
            catch (Exception ex)
            {
                throw new BasicDataException(string.Format("An unexpected error occurred while retrieving all the projects. ERROR: {0}", ex.Message), ex);
            }
        }

        public Project GetSingleById(int projectId)
        {
            try
            {
                var db = SqlDatabaseFactory.Create(Constants.ConnectionStrings.Core);
                var command = db.GetStoredProcCommand("[dbo].[pr_GetSingleProject]");
                db.AddInParameter(command, "@project_id", SqlDbType.Int, projectId);

                using (var resultDataSet = db.ExecuteDataSet(command))
                {
                    // Get Project
                    var projectInfoTable = resultDataSet.Tables[0];
                    if (projectInfoTable.Rows.Count == 0)
                    {
                        throw new Exception("Project no longer exists. It might've been deleted by another user.");
                    }

                    DataRow projectInfoRow = projectInfoTable.Rows[0];
                    Project project = convertDataRowToProject(projectInfoRow);
                    
                    // Get Data Sources
                    project.DataSources.AddRange(_dataSourceRepository.GetAllByProjectId(projectId, true, false));

                    return project;
                }
            }
            catch (Exception ex)
            {
                throw new BasicDataException(
                    string.Format("An unexpected error occurred while retrieving the project. Error: {0}", ex.Message), 
                    ex, 
                    string.Format("Project Id: {0}", projectId));
            }
        }

        private static Project convertDataRowToProject(DataRow row)
        {
            var project = new Project();

            project.Id = row.Field<int>("project_id");
            project.Name = row.Field<string>("name");
            project.Description = row.Field<string>("description");
            project.ModifiedOn = row.Field<DateTime>("modified_datetime");
            project.CreatedOn = row.Field<DateTime>("created_datetime");

            return project;
        }
    }
}
