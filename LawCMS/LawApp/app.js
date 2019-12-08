
var app = angular.module('myApp', [])
    .config(function ($locationProvider) {
        $locationProvider.html5Mode(true);
    });

var myApp = angular.module('myApp', ['naif.base64']);

myApp.directive('stringToNumber', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            ngModel.$parsers.push(function (value) {
                return '' + value;
            });
            ngModel.$formatters.push(function (value) {
                return parseFloat(value);
            });
        }
    };
});

myApp.directive('allowOnlyCurrency', function () {
    return {
        restrict: 'A',
        link: function (scope, elm, attrs, ctrl) {
            elm.on('keydown', function (event) {
                var $input = $(this);
                var value = $input.val();
                value = value.replace(/[^0-9]/g, '')
                //$input.val(value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
                if (event.which == 64 || event.which == 16) {
                    // to allow numbers
                    return false;
                } else if (event.which >= 48 && event.which <= 57) {
                    // to allow numbers
                    return true;
                } else if (event.which >= 96 && event.which <= 105) {
                    // to allow numpad number
                    return true;
                } else if ([8, 9, 13, 27, 37, 38, 39, 40].indexOf(event.which) > -1) {
                    // to allow backspace, enter, escape, arrows
                    return true;
                } else {
                    event.preventDefault();
                    // to stop others
                    //alert("Sorry Only Numbers Allowed");
                    return false;
                }
            });
        }
    }
});

myApp.directive('loading', ['$http', function ($http) {
    return {
        restrict: 'A',
        template: '<div class="loading-spiner"><img src="/Content/Rolling.gif" /> </div>',
        link: function (scope, elm, attrs) {
            scope.isLoading = function () {
                return $http.pendingRequests.length > 0;
            };

            scope.$watch(scope.isLoading, function (v) {
                if (v) {
                    elm.show();
                } else {
                    elm.hide();
                }
            });
        }
    };
}]);  