
(function () {
    'use strict';

    angular.module("dataAnalysisApp").service("dataSourceService", ["$http", dataSourceService]);

    function dataSourceService($http) {
        
        var getDataSourceById = function (dataSourceId) {
            return $http.get(baseSiteUrlPath + "/DataSource/Get?dataSourceId=" + dataSourceId); 
        }

        var getAllTables = function (dataSourceId) {
            return $http.get(baseSiteUrlPath + "/DataSource/GetAllTables?dataSourceId=" + dataSourceId);
        }

        var create = function (projectId, dataSource) {
            var viewModel = {};
            viewModel.projectId = projectId;
            viewModel.dataSource = dataSource;
            return $http.post(baseSiteUrlPath + "/DataSource/Create", viewModel);
        }

        var updateInfo = function (dataSource) {
            return $http.post(baseSiteUrlPath + "/DataSource/Edit", dataSource);
        }

        var updateTables = function (dataSourceId, tables) {
            var viewModel = {};
            viewModel.dataSourceId = dataSourceId;
            viewModel.tables = tables;
            return $http.post(baseSiteUrlPath + "/DataSource/UpdateTables", viewModel);
        }

        var getAllConnections = function() {
            return $http.get(baseSiteUrlPath + "/DataSource/GetConnections");
        }

        var createConnection = function (connection) {
            return $http.post(baseSiteUrlPath + "/DataSource/CreateNewConnection", connection);
        }

        return {
            getDataSourceById: getDataSourceById,
            create: create,
            updateInfo: updateInfo,
            getAllConnections: getAllConnections,
            createConnection: createConnection,
            getAllTables: getAllTables,
            updateTables: updateTables
        };
    }

}());