
(function() {
    'use strict';

    angular.module("dataAnalysisApp")
        .controller("ProjectListController", ["projectService", projectListController]);

    function projectListController(projectService) {
        var vm = this;


        projectService.getAllProjects().success(function(data, status) {
            vm.projects = data;
        }).error(function(data, status, headers, config) {
            vm.error = "An error occurred while loading projects. Please try again.";
        });
    }
}());