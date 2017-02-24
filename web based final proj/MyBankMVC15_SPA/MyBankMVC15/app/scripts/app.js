'use strict';
var app = angular.module('app', ['ngRoute', 'ngResource']);

app.config(function ($routeProvider) {
    $routeProvider.when('/', { templateUrl: 'views/home.html', controller: 'homeController' });
    $routeProvider.when('/news', {
        templateUrl: 'views/news.html', controller: 'newsController',
        resolve: {
            auth: ["$q", function ($q) {
                var userName = window.sessionStorage.getItem('authToken');

                if (userName) {
                    return $q.when(userName);
                } else {
                    return $q.reject({ authenticated: false });
                }
            }]
        }
    });
    $routeProvider.when('/login', { templateUrl: 'views/login.html', controller: 'loginController' });
    $routeProvider.when('/logoff', { templateUrl: 'views/login.html', controller: 'loginController' });
    $routeProvider.when('/transfer', {
        templateUrl: 'views/transfer.html', controller: 'transferController',
        resolve: {
            auth: ["$q", function ($q) {
                var userName = window.sessionStorage.getItem('authToken');

                if (userName) {
                    return $q.when(userName);
                } else {
                    return $q.reject({ authenticated: false });
                }
            }]
        }
    });
    $routeProvider.when('/transaction', {
        templateUrl: 'views/transaction.html', controller: 'transactionController',
        resolve: {
            auth: ["$q", function ($q) {
                var userName = window.sessionStorage.getItem('authToken');

                if (userName) {
                    return $q.when(userName);
                } else {
                    return $q.reject({ authenticated: false });
                }
            }]
        }
    });
    $routeProvider.otherwise({ redirectTo: '/' });
});

app.constant('Settings', {
    BaseUrl: document.location.origin
});

app.run(["$rootScope", "$location", function ($rootScope, $location) {
    $rootScope.$on("$routeChangeSuccess", function (userInfo) {
        //console.log(userInfo);
    });

    $rootScope.$on("$routeChangeError", function (event, current, previous, eventObj) {
        if (eventObj.authenticated === false) {
            $location.path("/login");
        }
    });
}]);