myApp.controller("dashboardController", ['$scope', '$http', '$window', function ($scope, $http, $window) {
    $scope.User = {};
    $scope.AbsolutePath = '';

    $scope.getDashboardData = function () {

        $http({
            method: "POST",
            url: "/Home/GetDashboardData",
            //data: $.param($scope.data),
            headers: {
                "Content-Type": "application/x-www-form-urlencoded"
            }
        }).then(function (response) {
            console.log(response.data);
            debugger;
            if (response.data.Success) {
                $scope.data = response.data.Result;
            }
            else {
                alert('Error occur data not load.');
            }
        });
    };
    $scope.getDashboardData();
}]);