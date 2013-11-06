(function () {
    'use strict';
    var moduleId = 'GameController';
    angular.module('app').controller(moduleId, ["$scope", "ControllerContext", "GameService", controller]);

    function controller($scope, controllerContext, gameService) {
        $scope.context = controllerContext;
        controllerContext.gameName = "";

        $scope.create = function () {
            if (controllerContext.gameName == "") {
                return;
            }

            gameService.canCreate($scope, controllerContext.gameName, function (result) {
                if (result) {
                    controllerContext.isGameAssigned = true;
                    controllerContext.isGameOwner = true;
                }
            });
        };

        $scope.join = function () {
            if (controllerContext.gameName == "") {
                return;
            }
            if (gameService.canJoin(controllerContext.gameName)) {
                controllerContext.isGameAssigned = true;
                controllerContext.isGameOwner = true;
            }
        };

    }
})();
