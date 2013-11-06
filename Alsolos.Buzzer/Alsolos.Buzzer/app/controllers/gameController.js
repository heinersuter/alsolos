(function () {
    'use strict';
    var moduleId = 'GameController';
    angular.module('app').controller(moduleId, ["$scope", "ControllerContext", "GameService", controller]);

    function controller($scope, context, gameService) {
        $scope.context = context;
        context.gameName = "";

        $scope.create = function () {
            if (context.gameName == "") {
                return;
            }

            gameService.canCreate(context.gameName, function (result) {
                if (result) {
                    context.isGameAssigned = true;
                    context.isGameOwner = true;
                }
            });
        };

        $scope.join = function () {
            if (context.gameName == "") {
                return;
            }
            gameService.canJoinGame(context.gameName, function (result) {
                if (result) {
                    context.isGameAssigned = true;
                    context.isGameOwner = false;
                }
            });
        };

    }
})();
