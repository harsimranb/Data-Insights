
(function() {
    angular.module('dataAnalysisApp')
        .controller('DataSourceTablesUpsertController', ["dataSource", "allTables", "dataSourceService", "$state", "$stateParams", dataSourceTablesUpsertController]);

    function dataSourceTablesUpsertController(dataSource, allTables, dataSourceService, $state, $stateParams) {
        var vm = this;

        vm.projectId = $stateParams.projectId;
        vm.tableSearch = '';
        vm.viewSearch = '';

        if (dataSource.status != 200) {
            vm.error = "Unable to load the data source. It might've been deleted by another user.";
        } else {
            if (allTables.status != 200) {
                vm.error = "Unable to load the tables for this data source. Please check the connection and try again.";
            } else {
                vm.allTables = allTables.data;
            }

            vm.dataSource = dataSource.data;
        }

        vm.submit = function () {
            dataSourceService.updateTables(vm.dataSource.Id, vm.allTables).success(function (data, status) {
                if (data.ErrorMessage != null) {
                    vm.error = data.ErrorMessage;
                } else {
                    $state.go('projectView.main', { projectId: $stateParams.projectId });
                }
            }).error(function (data, status, headers, config) {
                vm.error = "An error occurred while saving project. Please try again.";
            });
        }

        vm.cancel = function() {
            $state.go('projectView.main', { projectId: $stateParams.projectId });
        }
    }
}());