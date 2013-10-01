var app = angular.module("buzzer", ["firebase"]);

function ApplicationController($scope, angularFire) {
    var ref = new Firebase("https://alsolos.firebaseIO.com/buzzer");
    $scope.messages = [];
    angularFire(ref, $scope, "messages");

    $scope.addMessage = function (e) {
        if (e.keyCode != 13) return;
        $scope.messages.push({ from: $scope.name, body: $scope.msg });
        $scope.msg = "";
    };
}