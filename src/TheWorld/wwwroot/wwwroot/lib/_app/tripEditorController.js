!function(){"use strict";function t(t,n){var a=this;a.tripName=t.tripName,a.stops=[],a.errorMessage="",a.isBusy=!0,a.newStop={};var e="/api/trips/"+a.tripName+"/stops";n.get(e).then(function(t){angular.copy(t.data,a.stops),o(a.stops)},function(t){a.errorMessage="Failed to load data: "+t}).finally(function(){a.isBusy=!1}),a.addStop=function(){a.isBusy=!0,n.post(e,a.newStop).then(function(t){a.stops.push(t.data),o(a.stops),a.newStop={}},function(t){a.errorMessage="Failed to save new stop"}).finally(function(){a.isBusy=!1})}}function o(t){if(t&&t.length>0){var o=_.map(t,function(t){return{lat:t.latitude,long:t.longitude,info:t.name}});travelMap.createMap({stops:o,selector:"#map",currentStop:1,initialZoom:3})}}t.$inject=["$routeParams","$http"],angular.module("app-trips").controller("tripEditorController",t)}();