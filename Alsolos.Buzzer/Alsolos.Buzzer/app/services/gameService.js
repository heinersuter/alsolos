(function () {
    'use strict';
    var moduleId = 'GameService';
    angular.module('app').service(moduleId, ["angularFire", service]);

    function service(angularFire) {

        var gamesRef = new Firebase("https://alsolos.firebaseIO.com/buzzer/games");

        this.canCreate = function ($scope, gameName, callback) {
            canCreateOrJoin($scope, gameName, callback, true);
        };

        this.create = function ($scope, gameName, ownerName) {
            gamesRef.child(gameName).set({ name: gameName, ownerName: ownerName, created: new Date().getTime() });
            gamesRef.child(gameName).child("users").child(ownerName).set({ name: ownerName });
        };

        this.canJoin = function ($scope, gameName, callback) {
            canCreateOrJoin($scope, gameName, callback, false);
        };

        this.join = function ($scope, gameName, userName) {
            gamesRef.child(gameName).child("users").child(userName).set({ name: userName });
        };

        function canCreateOrJoin($scope, gameName, callback, isCreate) {
            var isNameExisting;
            if ($scope.games === undefined) {
                $scope.games = [];
                var promise = angularFire(gamesRef, $scope, "games");
                promise.then(function () {
                    isNameExisting = isItemContained($scope.games, gameName);
                    callback(isCreate != isNameExisting);
                });
            } else {
                isNameExisting = isItemContained($scope.games, gameName);
                callback(isCreate != isNameExisting);
            }
        }

        function isItemContained(games, gameName) {
            for (var game in games) {
                if (game == gameName) {
                    return true;
                }
            }
            return false;
        }

    }
})();
