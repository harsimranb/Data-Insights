
var baseSiteUrlPath = "/DataAnalysisWebAdmin";

(function() {
    "use strict";

    var app = angular.module("dataAnalysisApp", ["ui.router", 'angular-loading-bar', 'ui.bootstrap']);

    app.config([
        "$stateProvider", "$urlRouterProvider", '$locationProvider', function($stateProvider, $urlRouterProvider, $locationProvider) {
            $urlRouterProvider.otherwise("/");

            var websiteTitle = " - Data Analysis Web";

            $stateProvider
                // Home
                .state("home", {
                    url: baseSiteUrlPath + "/",
                    templateUrl: "app/projects/views/projectsListView.html",
                    controller: "ProjectListController as vm",
                    data: {
                        pageTitle: 'Home' + websiteTitle
                    }
                })
                // Create Project
                .state("createProject", {
                    url: baseSiteUrlPath + "/projects/create/",
                    templateUrl: "app/projects/views/upsertProjectView.html",
                    controller: "ProjectUpsertController as vm",
                    data: {
                        pageTitle: 'New Project' + websiteTitle
                    }
                })
                // Edit Project
                .state("editProjectInfo", {
                    url: baseSiteUrlPath + "/projects/edit/:projectId",
                    templateUrl: "app/projects/views/upsertProjectView.html",
                    controller: "ProjectUpsertController as vm",
                    data: {
                        pageTitle: 'Edit Project' + websiteTitle
                    }
                })
                /*********** View Project Routers START ************/
                .state("projectView", {
                    url: baseSiteUrlPath + "/projects/view/:projectId",
                    templateUrl: "app/projects/views/projectView.html",
                    controller: "ProjectViewController as vm",
                    resolve: {
                        $http: "projectService",
                        project: function(projectService, $stateParams) {
                            var id = $stateParams.projectId;
                            return projectService.getProjectById(id);
                        }
                    },
                    abstract: true
                })
                .state("projectView.main", {
                    url: "/Main",
                    templateUrl: "app/projects/views/projectDetailView.html",
                })
                .state("projectView.dataSources", {
                    url: "/DataSources",
                    templateUrl: "app/projects/views/projectDataSourceView.html",
                })
                .state("projectView.reports", {
                    url: "/Reports",
                    templateUrl: "app/projects/views/projectReportsView.html",
                })
                .state("projectView.charts", {
                    url: "/Charts",
                    templateUrl: "app/projects/views/projectChartsView.html",
                })
                .state("projectView.publish", {
                    url: "/Publish",
                    templateUrl: "app/projects/views/projectPublishView.html",
                })
                /*********** View Project Routers END ************/
                // Create Data Source
                .state("createDataSource", {
                    url: baseSiteUrlPath + "/projects/:projectId/DataSources/create",
                    templateUrl: "app/dataSource/views/upsertDataSource.html",
                    controller: "DataSourceUpsertController as vm",
                    resolve: {
                        dataSourceService: "dataSourceService",
                        dataSource: function() {
                            return null;
                        },
                        connections: function(dataSourceService) {
                            return dataSourceService.getAllConnections();
                        }
                    },
                    data: {
                        pageTitle: 'New DataSource' + websiteTitle
                    }
                })
                // Edit Data Source
                .state("editDataSource", {
                    url: baseSiteUrlPath + "/projects/:projectId/DataSources/:dataSourceId/edit",
                    templateUrl: "app/projects/views/upsertProjectView.html",
                    controller: "ProjectUpsertController as vm",
                    resolve: {
                        dataSourceService: "dataSourceService",
                        dataSource: function(dataSourceService, $stateParams) {
                            var id = $stateParams.projectId;
                            return projectService.getProjectById(id);
                        },
                        connections: function() {
                            return dataSourceService.getAllConnections();
                        }
                    },
                    data: {
                        pageTitle: 'Edit DataSource' + websiteTitle
                    }
                })
                // Edit Data Source Tables
                .state("editDataSourceTables", {
                    url: baseSiteUrlPath + "/projects/:projectId/DataSources/:dataSourceId/editTables",
                    templateUrl: "app/dataSource/views/upsertDataSourceTablesView.html",
                    controller: "DataSourceTablesUpsertController as vm",
                    resolve: {
                        dataSourceService: "dataSourceService",
                        dataSource: function (dataSourceService, $stateParams) {
                            var id = $stateParams.dataSourceId;
                            return dataSourceService.getDataSourceById(id);
                        },
                        allTables: function (dataSourceService, $stateParams) {
                            var id = $stateParams.dataSourceId;
                            return dataSourceService.getAllTables(id);
                        }
                    },
                    data: {
                        pageTitle: 'Edit Tables' + websiteTitle
                    }
                })
                // Create Connection
                .state("createDataSource.createConnection", {
                    url: "/connections/create",
                    onEnter: [
                        '$stateParams', '$state', '$modal', function ($stateParams, $state, $modal) {
                            // TODO
                            $modal.open({
                                templateUrl: "app/dataSource/views/upsertConnection.html",
                                resolve: {
                                    connection: function() {
                                        return null;
                                    }
                                },
                                controller: "ConnectionUpsertController as vm",
                            }).result.then(function(selectedItem) {
                                // closed
                                debugger;
                                $state.go('^');
                            }, function() {
                                // dismissed
                            });
                        }
                    ]
                })
                // Edit Connection
                .state("editConnection", {
                    url: baseSiteUrlPath + "/connections/:connectionId/edit",
                    templateUrl: "app/projects/views/upsertConnection.html",
                    controller: "ConnectionUpsertController as vm",
                    resolve: {
                        dataSourceService: "dataSourceService",
                        connection: function(dataSourceService, $stateParams) {
                            // TODO
                        }
                    },
                    data: {
                        pageTitle: 'Edit Connection' + websiteTitle
                    }
                });

            // enable html5Mode for pushstate ('#'-less URLs)
            $locationProvider.html5Mode(true);
            $locationProvider.hashPrefix('!');
        }
    ]);

}());