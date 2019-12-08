myApp.controller("caseCreateController", ['$scope', '$http', '$window', function ($scope, $http, $window) {
    $scope.case = {};
    $scope.AbsolutePath = '';

   
    $scope.getAllCases = function () {
        
        $scope.data = {};
        $http({
            method: "POST",
            url: "" + $scope.AbsolutePath + "/Case/GetUsers",
            //data: $.param($scope.data),
            headers: {
                "Content-Type": "application/x-www-form-urlencoded"
            }
        }).then(function (response) {
            debugger;
            console.log(response.data);
            $scope.users = response.data;
        });
    }

     $scope.getAllUsers();

    $scope.editUser = function (user) {
        $scope.User = user;
        $scope.isEditMode = true;
    }

    $scope.createUser = function () {
        if ($scope.User.Password != $scope.User.ConfirmPassword) {
            Toastr.warning("Password not match");
        }
        else {
            $scope.data = {};
            $scope.data["__RequestVerificationToken"] = angular.element('input[name=__RequestVerificationToken]').attr('value');
            $scope.data.model = $scope.User;
            $http({
                method: "POST",
                url: "" + $scope.AbsolutePath + "/Admin/Users/Create",
                data: $.param($scope.data),
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded"
                }
            }).then(function (response) {
                console.log(response);
                if (response.data.Success) {
                    Messages.SuccessMessage();
                    $scope.UserForm.$setPristine();
                    $scope.UserForm.$setUntouched();
                    $scope.User = {};
                    $scope.hideUserForm();
                    $scope.getAllUsers();
                }
                else {
                    //Toastr.warning('Duplicate Product');
                    Messages.CustomWarningMessage(response.data.Message);
                }

            });
        }
    }

    $scope.updateUser = function () {

        if ($scope.User.Password != $scope.User.ConfirmPassword) {
            Toastr.warning("Password not match");
        }
        else {
            $scope.data = {};
            $scope.data["__RequestVerificationToken"] = angular.element('input[name=__RequestVerificationToken]').attr('value');
            $scope.data.aspNetUser = $scope.User;
            $http({
                method: "POST",
                url: "" + $scope.AbsolutePath + "/Admin/Users/Update",
                data: $.param($scope.data),
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded"
                }
            }).then(function (response) {
                console.log(response);
                if (response.data.Success) {
                    Messages.SuccessMessage();
                    $scope.UserForm.$setPristine();
                    $scope.UserForm.$setUntouched();
                    $scope.User = {};
                    $scope.hideUserForm();
                    $scope.getAllUsers();
                }
                else {
                    Messages.CustomWarningMessage(response.data.Message);
                }

            });
        }
    }

    $scope.deleteUser = function (userId, userName) {
        if (confirm("Do you want to delete " + userName + " ?")) {

            $scope.data = {};
            $scope.data["__RequestVerificationToken"] = angular.element('input[name=__RequestVerificationToken]').attr('value');
            $scope.data.id = userId;
            $http({
                method: "POST",
                url: "" + $scope.AbsolutePath + "/Admin/Users/DeleteUser",
                data: $.param($scope.data),
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded"
                }
            }).then(function (response) {
                //console.log(response);
                if (response.data == 1) {
                    Messages.SuccessMessage();
                    $scope.UserForm.$setPristine();
                    $scope.UserForm.$setUntouched();
                    $scope.User = {};
                    $scope.hideUserForm();
                    $scope.getAllUsers();
                }
                else {
                    Toastr.productFailure();
                }

            });
        }
    }
}]);