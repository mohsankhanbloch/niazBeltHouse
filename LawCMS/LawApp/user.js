myApp.controller("userController", ['$scope', '$http', '$window', function ($scope, $http, $window) {
    $scope.User = {};
    $scope.AbsolutePath = '';

    $scope.getAllUsers = function () {
        $scope.data = {};
        $http({
            method: "POST",
            url: "" + $scope.AbsolutePath + "/User/GetUsers",
            //data: $.param($scope.data),
            headers: {
                "Content-Type": "application/x-www-form-urlencoded"
            }
        }).then(function (response) {
            $scope.users = response.data;
        });
    }

    $scope.getAllUsers();

    $scope.editUser = function (user) {
        $scope.UserId = user.Id;
        $window.location.href = '/User/Create?param1=' + $scope.UserId;
    }

    $scope.deleteUser = function (data) {

        if (confirm("Do you want to delete " + data.FirstName + " ?")) {
            $scope.data = {};
            $scope.data.userId = data.Id;
            $http({
                method: "POST",
                url: "" + $scope.AbsolutePath + "/User/DeleteUser",
                data: $.param($scope.data),
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded"
                }
            }).then(function (response) {
                if (response.data.Success) {
                    alert("User Delete Successfully");
                    $scope.getAllUsers();
                }
                else {
                    alert("User Not Delete");
                }

            });
        }
    }
}]);