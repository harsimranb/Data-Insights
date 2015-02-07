
(function () {
    'use strict';

    angular.module("dataAnalysisApp")
        .controller("DataSourceUpsertController", ["dataSource", "connections", "dataSourceService", "$state", "$stateParams", "$modal", dataSourceUpsertController]);

    function dataSourceUpsertController(dataSource, connections, dataSourceService, $state, $stateParams, $modal) {
        var vm = this;

        vm.isNew = dataSource == null;

        if (connections.status == 200) {
            vm.connections = connections.data;
        }

        if (!vm.isNew) {
            if (project.status == 200) {
                vm.dataSource = dataSource.data;
            } else {
                vm.error = "An error occurred while loading project. Please try again.";
            }
        }

        vm.newConnection = function() {
            $modal.open({
                templateUrl: "app/dataSource/views/upsertConnection.html",
                resolve: {
                    connection: function () {
                        return null;
                    }
                },
                controller: "ConnectionUpsertController as vm",
            }).result.then(function (selectedItem) {
                // closed
                vm.connections.push(selectedItem);
                $state.go('^');
            }, function () {
                // dismissed
            });
        }

        vm.cancel = function () {
            $state.go("projectView.main", { projectId: $stateParams.projectId });
        }

        vm.submit = function () {
            if (vm.isNew) {
                dataSourceService.create($stateParams.projectId, vm.dataSource).success(function (data, status) {
                    $state.go("projectView.main", { projectId: $stateParams.projectId });
                }).error(function (data, status, headers, config) {
                    vm.error = "An error occurred while saving project. Please try again.";
                });
            } else {
                dataSourceService.updateInfo(vm.dataSource).success(function (data, status) {
                    $state.go("projectView.main", { projectId: $stateParams.projectId });
                }).error(function (data, status, headers, config) {
                    vm.error = "An error occurred while saving project. Please try again.";
                });
            }
        }
    }
}());