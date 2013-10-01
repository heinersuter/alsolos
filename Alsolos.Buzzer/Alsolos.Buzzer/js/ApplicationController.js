var app = angular.module("buzzer", ["firebase"]);

function ApplicationController($scope, angularFire) {
    var fbUsersRef = new Firebase("https://alsolos.firebaseIO.com/buzzer/game/users");
    $scope.users = [];
    angularFire(fbUsersRef, $scope, "users");

    var fbWinnerRef = new Firebase("https://alsolos.firebaseIO.com/buzzer/game/winner");
    $scope.winner = "";
    angularFire(fbWinnerRef, $scope, "winner");

    $scope.isJoinDisabled = false;
    $scope.isBuzzerDisabled = true;
    $scope.isResetDisabled = true;

    $scope.addUser = function () {
        if ($scope.users.indexOf($scope.userName) != -1) {
            return;
        }
        $scope.isJoinDisabled = true;
        $scope.isBuzzerDisabled = false;
        $scope.users.push($scope.userName);
        $scope.msg = "";
    };

    $scope.buzzer = function () {
        $scope.isBuzzerDisabled = true;
        if ($scope.winner == "") {
            $scope.winner = $scope.userName;
        }
    };

    $scope.reset = function () {
        $scope.isBuzzerDisabled = false;
        $scope.winner = "";
    };
}