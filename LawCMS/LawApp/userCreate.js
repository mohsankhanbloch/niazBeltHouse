myApp.controller("userCreateController", ['$scope', '$http', '$window', '$location', function ($scope, $http, $window, $location) {
    $scope.User = {};
    $scope.AbsolutePath = "";

    var pathvalue = $location.$$absUrl;
    var UserId = pathvalue.split("=")[1];

    if (UserId != undefined) {
        $scope.isEditMode = true;
        getUser(UserId);
    } else {
        $scope.isEditMode = false;
    }

    $scope.roles = ['Admin', 'Attorney', 'Clerk', 'Client'];

    function getUser(userId) {
        $scope.data = {};
        $scope.data.UserId = userId;
        $http({
            method: "POST",
            url: "" + $scope.AbsolutePath + "/User/GetUser",
            data: $.param($scope.data),
            headers: {
                "Content-Type": "application/x-www-form-urlencoded"
            }
        }).then(function (response) {
            $scope.User = response.data[0];
        })
    }

    $scope.editUser = function (user) {
        $scope.User = user;
    }

    $scope.createUser = function () {

        if ($scope.User.Password != $scope.User.ConfirmPassword) {
            return alert("Password not match");
        }
        $scope.data = {};
        $scope.data = $scope.User;

        $http({
            method: "POST",
            url: "" + $scope.AbsolutePath + "/User/UserCreate",
            data: $.param($scope.data),
            headers: {
                "Content-Type": "application/x-www-form-urlencoded"
            }
        }).then(function (response) {
            if (response.data.Success) {
                $window.location.href = '/User/List';
                $scope.User = {};
            }
            else {
                alert("User Not Created");
            }

        });

    }

    $scope.updateUser = function () {
        if ($scope.User.Password != $scope.User.ConfirmPassword) {
            alert("Password not match");
        }
        else {
            $scope.data = {};
            $scope.data.aspNetUser = $scope.User;
            $http({
                method: "POST",
                url: "" + $scope.AbsolutePath + "/User/Update",
                data: $.param($scope.data),
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded"
                }
            }).then(function (response) {
                console.log(response);
                if (response.data.Success) {
                    alert("User Successfully Updated");
                    $window.location.href = '/User/List';
                }
                else {
                    alert("User Not Updated");
                }

            });
        }
    }

}]);