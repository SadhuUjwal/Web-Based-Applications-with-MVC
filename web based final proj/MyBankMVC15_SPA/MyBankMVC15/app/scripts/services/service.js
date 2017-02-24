'use strict';
app.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.defaults.useXDomain = true;
    /**
     * Just setting useXDomain to true is not enough. AJAX request are also
     * send with the X-Requested-With header, which indicate them as being
     * AJAX. Removing the header is necessary, so the server is not
     * rejecting the incoming request.
     **/
    delete $httpProvider.defaults.headers.common['X-Requested-With'];
}
]).factory('loginService', function ($http, $q, Settings) {
    var url = Settings.BaseUrl + '/Auth/Login';
    var self = this;
    self.LoginUser = function (data) {
        var defer = $q.defer();
        $http.post(url, {
            "Username": data.UserName, "Password": data.Password
        }).success(function (data, status, headers, config) {
            return defer.resolve(data);
        }).error(function (failedResponse, status, headers, config) {
            return defer.reject(failedResponse);
        });

        return defer.promise;
    }

    return self;
}).factory('transferService', function ($http, $q, Settings) {
    var url = Settings.BaseUrl + '/Transfer/XferChkToSav';
    var self = this;
    self.GetAccount = function () {
        var defer = $q.defer();
        $http.get(url, {
            headers: {}
        }).success(function (data, status, headers, config) {
            return defer.resolve(data);
        }).error(function (failedResponse, status, headers, config) {
            return defer.reject(failedResponse);
        });

        return defer.promise;
    }

    self.TransferAmount = function (data) {
        var defer = $q.defer();
        debugger$http.post(url, { "TransAmt": data.TransAmt })
            .success(function (data, status, headers, config) {
                return defer.resolve(data);
            }).error(function (failedResponse, status, headers, config) {
                return defer.reject(failedResponse);
            });

        return defer.promise;
    }

    return self;
}).factory('transactionService', function ($http, $q, Settings) {
    var url = Settings.BaseUrl + '/History/History';
    var self = this;
    self.GetHistory = function () {
        var defer = $q.defer();
        $http.get(url, {
            headers: {}
        }).success(function (data, status, headers, config) {
            return defer.resolve(data);
        }).error(function (failedResponse, status, headers, config) {
            return defer.reject(failedResponse);
        });

        return defer.promise;
    }

    return self;
});