
(function () {
    'use strict';

    var app = angular.module("dataAnalysisApp");
    
    app.filter('jsonDate', function () {
        return function (x) {
            return new Date(parseInt(x.replace(/\/Date\((.*?)\)\//gi, "$1")));
        };
    });

}());