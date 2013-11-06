(function () {
    'use strict';
    var moduleId = 'GameService';
    angular.module('app').service(moduleId, ["angularFire", service]);

    function service(angularFire) {

        function canCreateCallback($scope, gameName, callback) {
            var isNameNotExisting = true;
            for (var i = 0; i < $scope.games.length; i++) {
                if ($scope.games[i].name === gameName) {
                    isNameNotExisting = false;
                }
            }
            if (isNameNotExisting) {
                $scope.games.push({ name: gameName });
            }
            callback(isNameNotExisting);
        }

        this.canCreate = function ($scope, gameName, callback) {
            var firebaseRef = new Firebase("https://alsolos.firebaseIO.com/buzzer/games");
            if ($scope.games === undefined) {
                $scope.games = [];
                var promise = angularFire(firebaseRef, $scope, "games");
                promise.then(function () {
                    canCreateCallback($scope, gameName, callback);
                });
            } else {
                canCreateCallback($scope, gameName, callback);
            }
        };

        this.camJoin = function ($scope, gameName) {
            if ($scope.games.indexOf(gameName) == -1) {
                return false;
            }
            $scope.games.push(gameName);
            return true;
        };

    }
})();
