(function () {
    'use strict';

    window.app.factory('httpService', ['$http', function ($http) {
        var HOST = "";
        return {
            get: function (url, data) {
                var req = {
                    method: 'GET',
                    cache: false,
                    url: url,
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    data: data
                }
                var http = $http(req);
                //http.error(function (error, status) {
                //    //location.href = location.href;
                //});
                return http;
            },
            download: function (url) {
                var req = {
                    method: 'GET',
                    cache: false,
                    url: url,
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    },
                    responseType: 'arraybuffer'
                }
                var http = $http(req);
                http.error(function (error, status) {
                    //location.href = location.href;
                });
                return http;
            },
            post: function (url, data) {
                var req = {
                    method: 'POST',
                    url: url,
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    data: data
                }
                var http = $http(req);
                //http.error(function (error, status) {
                //    switch (status) {
                //    }
                //});
                return http;
            },
            putFile: function (url, data, authorization) {
                var fd = new FormData();
                fd.append('file', file);

                var http = $http({
                    method: 'PUT',
                    url: url,
                    headers: {
                        'Content-Type': 'multipart/form-data',
                        'Authorization': 'Bearer ' + authorization
                    },
                    data: data,
                    transformRequest: function (data, headersGetter) {
                        var formData = new FormData();
                        angular.forEach(data, function (value, key) {
                            formData.append(key, value);
                        });

                        var headers = headersGetter();
                        delete headers['Content-Type'];

                        return formData;
                    }
                });

                http.error(function (error, status) {
                    switch (status) {
                        case 500:
                            NotificationService.ShowError("Internal server error")
                            break;
                        default:
                            NotificationService.ShowError("an error occurred while processing your request. Please try again...")
                            break;
                    }
                });
                return http;
            },
            put: function (url, data) {
                var req = {
                    method: 'PUT',
                    url: url,
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    data: data
                }
                var http = $http(req);
                http.error(function (error, status) {
                    location.href = location.href;
                });
                return http;
            },
            delete: function (url, data) {
                var req = {
                    method: 'DELETE',
                    url: url,
                    headers: {
                        'Content-Type': undefined
                    },
                    data: data
                }
                var http = $http(req);
                http.error(function (error, status) {
                    location.href = location.href;
                });
                return http;
            },
            upload: function (url, method, data, token) {
                var http = Upload.upload({
                    method: method,
                    url: url,
                    data: data,
                    headers: {
                        'Content-Type': 'multipart/form-data',
                        'Authorization': 'Bearer ' + token
                    },
                });

                return http;
            },
            exportFile: function (url, data) {
            var req = {
                method: 'POST',
                url: url,
                headers: {
                    'Content-Type': 'application/json'
                },
                data: data,
                responseType: 'blob'
            }
            var http = $http(req);
            http.error(function (error, status) {
                return $q.reject(response.data);
            });
            return http;
        }
        };
    }]);
})();