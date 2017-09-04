(function () {
    'use strict';

    window.app.controller('searchController', searchController);
    searchController.$inject = ['$scope', 'httpService'];

    function searchController($scope, httpService) {
        var vm = $scope;

        var models = [];
        var makeNiceName = "";
        vm.photos = [];

        vm.getMakes = function (val) {
            var url = '/api/makesApi/searchMakes/{keyword?}';
            url = url.replace('{keyword?}', val);
            return httpService.get(url)
            .then(function (response) {
                return response.data;
            });
        };

        vm.onSelectMake = function (selected) {
            var makeId = selected.Id;
            models = [];
            vm.photos = [];
            makeNiceName = selected.NiceName;

            vm.selectedModel = null;
            var url = '/api/makesApi/searchModelsByMakeId/{makeId}';
            url = url.replace('{makeId}', makeId);
            httpService.get(url)
            .then(function (response) {
                models = response.data.Models;
            });
        }

        vm.onSelectModel = function (selected) {
            var url = "https://api.edmunds.com/api/media/v2/{makeNiceName}/{modelNiceName}/{year}/photos?pagenum=1&pagesize=10&view=basic&fmt=json&api_key=4sn3tcysbp367np9sktcena5";
            url = url.replace('{makeNiceName}', makeNiceName);
            url = url.replace('{modelNiceName}', selected.NiceName);
            url = url.replace('{year}', selected.Year);

            httpService.get(url)
           .then(function (response) {
               vm.photos = response.data.photos;
           });
        }

        vm.getModels = function () {
            return models;
        }
    }
})();