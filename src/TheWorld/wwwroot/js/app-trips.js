﻿//app.trips.js
(function () {
    "use strict";
    angular.module("app-trips", ["simpleControls", "ngRoute"])
        .config(function($routeProvider, $locationProvider) 
        {
            $routeProvider.when("/", {
                controller: "tripsController",
                controllerAs: "vm",
                templateUrl: "/views/tripsView.html"
            });
            
            $routeProvider.when("/editor/:tripName", {
                controller: "tripEditorController",
                controllerAs: "vm",
                templateUrl: "/views/tripEditorView.html"
            });

            $routeProvider.otherwise({ redirectTo: "/" });

            $locationProvider.html5Mode(false).hashPrefix('');
        });
})();