
(function () {
    'use strict';

    angular.module("dataAnalysisApp")
        .controller("ConnectionUpsertController", ["connection", "dataSourceService", "$state", "$scope", "$stateParams", connectionUpsertController]);

    function connectionUpsertController(connection, dataSourceService, $state, $scope, $stateParams) {
        var vm = this;

        vm.isNew = connection == null;

        $scope.stupid = 'yes';

        if (!vm.isNew) {
            if (connection.status == 200) {
                vm.connection = connection.data;
            } else {
                vm.error = "An error occurred while loading project. Please try again.";
            }
        } else {
            vm.connection = {};
            vm.connection.ConnectionType = 1;
        }

        vm.cancel = function () {
            $scope.$dismiss();
        }

        vm.submit = function () {
            vm.warning = null;
            if (vm.isNew) {
                dataSourceService.createConnection(vm.connection).success(function (data, status) {
                    if (data.ErrorMessage != null) {
                        vm.warning = data.ErrorMessage;
                    } else {
                        $scope.$close(data);
                    }
                }).error(function (data, status, headers, config) {
                    vm.error = "An error occurred while saving project. Please try again.";
                });
            } else {
                alert('Not Implemented!');
            }
        }
    }
}());