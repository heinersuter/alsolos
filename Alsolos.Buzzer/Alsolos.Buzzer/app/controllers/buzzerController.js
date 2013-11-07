(function () {
    'use strict';
    var moduleId = 'BuzzerController';
    angular.module('app').controller(moduleId, ["$rootScope", "$scope", "ControllerContext", "GameService", controller]);

    function controller($rootScope, $scope, context, gameService) {
        $scope.context = context;
        context.isBuzzerReady = true;

        $rootScope.$watch("winner", function (newValue, oldValue) {
            context.isBuzzerReady = $rootScope.winner == "";
        });


        $scope.buzzer = function () {
            if (gameService.buzzer(context.gameName, context.userName)) {
                context.isBuzzerReady = false;
            }
        };

        $scope.reset = function () {
            if (gameService.reset(context.gameName)) {
                context.isBuzzerReady = true;
            }
        };
    }
})();
