using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DataAnalysis.DomainObjects.DataSource;
using DataAnalysis.DomainObjects.Exceptions;
using DataAnalysis.Interfaces.Services;
using DataAnalysis.Web.Admin.Framework;
using DataAnalysis.Web.Admin.ViewModels;

namespace DataAnalysis.Web.Admin.Controllers
{
    public class DataSourceController : Controller
    {
        #region Fields

        private readonly IDataSourceService _dataSourceService;

        #endregion

        #region Constructor

        public DataSourceController(IDataSourceService dataSourceService)
        {
            _dataSourceService = dataSourceService;
        }

        #endregion

        #region GET

        public JsonNetResult Get(long? dataSourceId)
        {
            DataSource dataSource = getDataSourceById(dataSourceId);
            return new JsonNetResult
            {
                Data = dataSource
            };
        }

        public JsonNetResult GetConnections()
        {
            List<DataConnectionInfo> connections = _dataSourceService.GetAllConnections();

            return new JsonNetResult
            {
                Data = connections
            };
        }

        public JsonNetResult GetAllTables(int? dataSourceId)
        {
            DataSource dataSource = getDataSourceById(dataSourceId);

            var tables = _dataSourceService.GetTablesFromSource(dataSource.DataConnectionInfo);
            var selectableTables = new List<SelectableTableInfo>(tables.Count);
            foreach (var table in tables)
            {
                selectableTables.Add(new SelectableTableInfo
                {
                    IsSelected = dataSource.Tables.FirstOrDefault(i => i.ObjectId == table.ObjectId) != null, // TODO: Improve performance
                    TableInfo = table
                });
            }
            return new JsonNetResult()
            {
                Data = selectableTables
            };
        }

        #endregion

        #region POST

        [HttpPost]
        public JsonNetResult CreateNewConnection(DataConnectionInfo connectionInfo)
        {
            Exception ex;
            if (!validateConnection(connectionInfo, out ex))
            {
                // TODO: return errorResult
                throw ex;
            }

            // Check connection
            bool testResult = _dataSourceService.TestConnection(connectionInfo);
            if (!testResult)
            {
                return new JsonNetResult
                {
                    Data = new ErrorResult("Unable to connect to the server. Please check the connection parameters and try again.")
                };
            }

            var newConnectionResult = _dataSourceService.CreateConnection(connectionInfo);
            return new JsonNetResult
            {
                Data = newConnectionResult
            };
        }

        [HttpPost]
        public JsonNetResult Create(int projectId, DataSource dataSource)
        {
            Exception ex;
            if (!validateDataSource(dataSource, out ex))
            {
                // TODO: return errorResult
                throw ex;
            }

            _dataSourceService.Create(projectId, dataSource);
            return new JsonNetResult();
        }

        [HttpPost]
        public JsonNetResult Edit(DataSource dataSource)
        {
            Exception ex;
            if (!validateDataSource(dataSource, out ex))
            {
                // TODO: return errorResult
                throw ex;
            }

            _dataSourceService.UpdateInfo(dataSource);
            return new JsonNetResult();
        }

        [HttpPost]
        public JsonNetResult UpdateTables(long? dataSourceId, List<SelectableTableInfo> tables)
        {
            if (!dataSourceId.HasValue)
            {
                // TODO: return errorResult
                throw new BasicException("No project specified.", "projectId is null.");
            }

            _dataSourceService.UpdateTableInfoMappings(dataSourceId.Value, (from item in tables
                                                                        where item.IsSelected
                                                                        select item.TableInfo).ToList());

            return new JsonNetResult
            {
            };
        }

        #endregion

        #region Private Methods

        private bool validateDataSource(DataSource viewModel, out Exception exception)
        {
            exception = null;

            if (viewModel == null)
            {
                exception = new BasicException("Invalid request.", "viewModel is null.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(viewModel.Name))
            {
                exception = new BasicException("The name field is required.", "Name field is empty in Data Source request.");
                return false;
            }

            return true;
        }

        private bool validateConnection(DataConnectionInfo connectionInfo, out Exception exception)
        {
            exception = null;

            if (connectionInfo == null)
            {
                exception = new BasicException("Invalid request.", "viewModel is null.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(connectionInfo.ServerName))
            {
                exception = new BasicException("The 'Server Name' field is required.", "Name field is empty in Data Source request.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(connectionInfo.DatabaseName))
            {
                exception = new BasicException("The 'Database Name' field is required.", "Name field is empty in Data Source request.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(connectionInfo.Username))
            {
                exception = new BasicException("The 'Username' field is required.", "Name field is empty in Data Source request.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(connectionInfo.Password))
            {
                exception = new BasicException("The 'Password' field is required.", "Name field is empty in Data Source request.");
                return false;
            }

            return true;
        }

        private DataSource getDataSourceById(long? dataSourceId)
        {
            if (!dataSourceId.HasValue)
            {
                // TODO: return errorResult
                throw new BasicException("No project specified.", "projectId is null.");
            }

            DataSource dataSource = _dataSourceService.GetSingleById(dataSourceId.Value, true, false);
            if (dataSource == null)
            {
                // TODO: return errorResult
                throw new BasicException("The specified data source could not be found. It might have been deleted by another user.", string.Format("Data Source with id {0} not found.", dataSourceId.Value));
            }

            return dataSource;
        }

        #endregion
    }
}