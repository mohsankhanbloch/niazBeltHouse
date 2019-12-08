myApp.controller("viewCaseController", ['$scope', '$http', '$window', function ($scope, $http, $window) {
    $scope.case = {};
    $scope.cases = {};
    $scope.AbsolutePath = '';

   
    $scope.getAllCases = function () {
        debugger;
        $scope.data = {};
        //$scope.data["__RequestVerificationToken"] = angular.element('input[name=__RequestVerificationToken]').attr('value');
        $scope.data = $scope.case;
        $http({
            method: "POST",
            url: "" + $scope.AbsolutePath + "/Case/GetAllCases",
            //data: $.param($scope.data),
            headers: {
                "Content-Type": "application/x-www-form-urlencoded"
            }
        }).then(function (response) {
            debugger;
            console.log(response.data);
            $scope.cases = response.data.Result;
        });
    }
    $scope.getAllCases();

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