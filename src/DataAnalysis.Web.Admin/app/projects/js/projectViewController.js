
(function () {
    'use strict';

    angular.module("dataAnalysisApp")
        .controller("ProjectViewController", ["project", "projectService", projectViewController]);

    function projectViewController(project, projectService) {
        var vm = this;

        if (project.status == 200) {
            vm.project = project.data;
        } else {
            vm.error = "An error occurred while loading project. Please try again.";
        }
    }
}());