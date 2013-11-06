(function () {
    'use strict';
    var moduleId = 'UserController';
    angular.module('app').controller(moduleId, ["$scope", "ControllerContext", controller]);

    function controller($scope, controllerContext) {
        $scope.state = controllerContext;
        controllerContext.userName = "";
		
		$scope.join = function () {
		    if (controllerContext.userName == "") {
				return;
			}
		    controllerContext.isUserAssigned = true;
		};
    }
})();
