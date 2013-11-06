(function () {
    'use strict';
    var moduleId = 'GameController';
    angular.module('app').controller(moduleId, ["$scope", "angularFire", "ControllerContext", controller]);

    function controller($scope, angularFire, controllerContext) {
        $scope.state = controllerContext;
        controllerContext.gameName = "";

		var firebaseRef = new Firebase("https://alsolos.firebaseIO.com/buzzer/games");
		$scope.games = [];
		angularFire(firebaseRef, $scope, "games");
		
		$scope.create = function () {
		    if (controllerContext.gameName == "") {
				return;
			}
		    controllerContext.isGameAssigned = true;
		    controllerContext.isGameOwner = true;
		};

		$scope.join = function () {
		    if (controllerContext.gameName == "") {
				return;
			}
		    controllerContext.isGameAssigned = true;
		    controllerContext.isGameOwner = false;
		};

    }
})();
