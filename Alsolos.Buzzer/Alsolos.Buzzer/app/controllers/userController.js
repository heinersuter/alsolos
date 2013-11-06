(function () {
    'use strict';
    var moduleId = 'UserController';
    angular.module('app').controller(moduleId, ["$scope", "ControllerContext", "GameService", controller]);

    function controller($scope, context, gameService) {
        $scope.context = context;
        context.userName = "";
		
		$scope.join = function () {
		    if (context.userName == "") {
				return;
		    }
		    if (context.isGameOwner) {
		        gameService.create(context.gameName, context.userName);
		        context.isUserAssigned = true;
            }
		    else {
		        if (gameService.join(context.gameName, context.userName)) {
		            context.isUserAssigned = true;
		        }
		    }
		};
    }
})();
