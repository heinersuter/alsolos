(function () {
    'use strict';
    var moduleId = 'BuzzerController';
    angular.module('app').controller(moduleId, ["$scope", "ControllerContext", controller]);

    function controller($scope, controllerContext) {
        $scope.state = controllerContext;
        controllerContext.isBuzzerReady = true;

		$scope.buzzer = function () {
		    controllerContext.winnerName = controllerContext.userName;
		    controllerContext.isBuzzerReady = false;
		};

		$scope.reset = function () {
		    controllerContext.winnerName = "";
		    controllerContext.isBuzzerReady = true;
		};
    }
})();
