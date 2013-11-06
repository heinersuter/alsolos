(function () {
    'use strict';
    var moduleId = 'BuzzerController';
    angular.module('app').controller(moduleId, ["$scope", "ControllerContext", controller]);

    function controller($scope, context) {
        $scope.context = context;
        context.isBuzzerReady = true;

		$scope.buzzer = function () {
		    context.winnerName = context.userName;
		    context.isBuzzerReady = false;
		};

		$scope.reset = function () {
		    context.winnerName = "";
		    context.isBuzzerReady = true;
		};
    }
})();
