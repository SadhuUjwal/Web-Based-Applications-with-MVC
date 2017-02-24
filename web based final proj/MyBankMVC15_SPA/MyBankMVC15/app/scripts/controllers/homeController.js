'use strict';
app.controller('loginController', ['$scope', 'loginService', function ($scope, loginService) {
    $scope.UserData = { UserName: '', Password: '' };
    window.sessionStorage.removeItem('authToken');

    $scope.Login = function () {
        loginService.LoginUser($scope.UserData).then(function (response) {
            if (response.Success) {
                window.location = '#/';
                window.sessionStorage.setItem('authToken', response.Username);
                window.location.reload();
            }
            else {
                $scope.UserData.Message = response.Message;
            }
        }, function (error) {
        });
    };

}]);

app.controller('mainController', ['$scope', function ($scope) {
    $scope.heading = "Home";
    if (window.sessionStorage.getItem('authToken'))
        $scope.authenticated = true;
}]);

app.controller('homeController', ['$scope', function ($scope) {
    $scope.heading = "Home";
}]);

app.controller('newsController', ['$scope', function ($scope) {
    $scope.Username = window.sessionStorage.getItem('authToken');
}]);

app.controller('transferController', ['$scope', 'transferService', function ($scope, transferService) {
    $scope.GetAccountInfo = function () {
        transferService.GetAccount().then(function (response) {
            if (response.Username)
                $scope.Account = response;
            else
                $scope.Message = "You are not authorize";
        },
            function (error) {

            });
    };

    $scope.Transfer = function () {
        transferService.TransferAmount($scope.Account).then(function (response) {
            $scope.Account = response;
        },
            function (error) {

            });
    };
}]);

app.controller('transactionController', ['$scope', 'transactionService', function ($scope, transactionService) {
    $scope.GetHistory = function () {
        transactionService.GetHistory().then(function (response) {
            if (response[0].CheckingAccountNumber)
                $scope.History = response;
            else
                $scope.Message = "You are not authorize";
        },
            function (error) {

            });
    };
}]);