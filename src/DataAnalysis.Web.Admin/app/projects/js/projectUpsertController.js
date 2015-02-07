
(function () {
    'use strict';

    angular.module("dataAnalysisApp")
        .controller("ProjectUpsertController", ["projectService", "$state", "$stateParams", projectUpsertController]);

    function projectUpsertController(projectService, $state, $stateParams) {
        var vm = this;
        vm.isNew = $stateParams.projectId == null;

        if (!vm.isNew) {
            projectService.getProjectById($stateParams.projectId).success(function (data, status) {
                vm.project = data;
            }).error(function (data, status, headers, config) {
                vm.error = "An error occurred while loading project. Please try again.";
            });
        }

        vm.cancel = function () {
            $state.go("home");
        }

        vm.submit = function () {
            if (vm.isNew) {
                projectService.create(vm.project).success(function(data, status) {
                    $state.go("home"); // TODO : Go to project view page
                }).error(function(data, status, headers, config) {
                    vm.error = "An error occurred while saving project. Please try again.";
                });
            } else {
                projectService.updateInfo(vm.project).success(function (data, status) {
                    $state.go("projectView.main", { projectId: vm.project.Id });
                }).error(function (data, status, headers, config) {
                    vm.error = "An error occurred while saving project. Please try again.";
                });
            }
        }
    }
}());