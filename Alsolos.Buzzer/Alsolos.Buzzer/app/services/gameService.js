(function () {
    'use strict';
    var moduleId = 'GameService';
    angular.module('app').service(moduleId, ["angularFire", "$rootScope", service]);

    function service(angularFire, $rootScope) {

        var gamesRef = new Firebase("https://alsolos.firebaseIO.com/buzzer/games");

        this.canCreate = function (gameName, callback) {
            canCreateOrJoin(gameName, callback, true);
        };

        this.create = function (gameName, userName) {
            var result = false;

            var gameRef = gamesRef.child(gameName);
            gameRef.transaction(function (currentData) {
                if (currentData === null) {
                    result = true;
                    return { name: gameName, ownerName: userName, winner: "", updated: new Date().getTime() };
                } else {
                    result = false;
                    return currentData;
                }
            });

            if (result) {
                gamesRef.child(gameName).child("users").child(userName).set({ name: userName, updated: new Date().getTime() });

                var winnerRef = gamesRef.child(gameName).child("winner");
                angularFire(winnerRef, $rootScope, "winner");
            }
            
            return result;
        };

        this.canJoinGame = function (gameName, callback) {
            canCreateOrJoin(gameName, callback, false);
        };

        this.join = function (gameName, userName) {
            var result = false;

            var userRef = gamesRef.child(gameName).child("users").child(userName);
            userRef.transaction(function (currentData) {
                if (currentData === null) {
                    result = true;
                    return { name: userName, updated: new Date().getTime() };
                } else {
                    result = false;
                    return currentData;
                }
            });
            
            if (result) {
                var winnerRef = gamesRef.child(gameName).child("winner");
                angularFire(winnerRef, $rootScope, "winner");
            }

            return result;
        };
        
        this.buzzer = function(gameName, userName) {
            return setWinner(gameName, userName);
        };
        
        this.reset = function(gameName) {
            return setWinner(gameName, "");
        };

        function setWinner(gameName, userName) {
            if ($rootScope.winner == "" || userName == "") {
                $rootScope.winner = userName;
                return true;
            }
            return false;
            //var result = false;

            //var winnerRef = gamesRef.child(gameName).child("winner");
            //winnerRef.transaction(function (currentData) {
            //    if (userName === "" || currentData === "") {
            //        result = true;
            //        return userName;
            //    } else {
            //        result = false;
            //        return currentData;
            //    }
            //});

            //return result;
        }

        function canCreateOrJoin(gameName, callback, isCreate) {
            var isNameExisting;
            if ($rootScope.games === undefined) {
                $rootScope.games = [];
                var promise = angularFire(gamesRef, $rootScope, "games");
                promise.then(function () {
                    isNameExisting = isItemContained($rootScope.games, gameName);
                    callback(isCreate != isNameExisting);
                });
            } else {
                isNameExisting = isItemContained($rootScope.games, gameName);
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
