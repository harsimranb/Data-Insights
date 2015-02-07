
(function () {
    'use strict';

    angular.module("dataAnalysisApp").service("projectService", ["$http", projectService]);

    function projectService($http) {
        var getAllProjects = function () {
            return $http.get(baseSiteUrlPath + "/Project/GetAll");
        }

        var getProjectById = function (id) {
            return $http.get(baseSiteUrlPath + "/Project/Get?projectId=" + id); // TODO fix these 
        }

        var create = function (project) {
            return $http.post(baseSiteUrlPath + "/Project/Create", project);
        }

        var updateInfo = function (project) {
            return $http.post(baseSiteUrlPath + "/Project/Edit", project);
        }

        return {
            getAllProjects: getAllProjects,
            getProjectById: getProjectById,
            create: create,
            updateInfo: updateInfo
        };
    }

}());