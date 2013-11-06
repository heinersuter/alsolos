(function () {
    'use strict';
    var moduleId = 'UserController';
    angular.module('app').controller(moduleId, ["$scope", "ControllerContext", "GameService", controller]);

    function controller($scope, context, gameService) {
        $scope.context = context;
        context.userName = "";
		
		$scope.joinAsUser = function () {
		    if (context.userName == "") {
				return;
		    }
		    if (context.isGameOwner) {
		        gameService.create($scope, context.gameName, context.userName);
		    }
		    else {
		        gameService.join($scope, context.gameName, context.userName);
		    }
		    context.isUserAssigned = true;
		};
    }
})();
