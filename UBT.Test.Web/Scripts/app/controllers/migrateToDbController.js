(function () {
    'use strict';

    window.app.controller('migrateToDbController', migrateToDbController);
    migrateToDbController.$inject = ['httpService', '$rootScope'];

    function migrateToDbController(httpService, $rootScope) {
        var vm = this;
        vm.init = init;
        vm.importData = importData;

        var testApi = "https://api.edmunds.com/api/vehicle/v2/makes?state=used&year=2014&view=basic&fmt=json&api_key=4sn3tcysbp367np9sktcena5";
        var AddMakesApi = "/api/makesApi/addMakes";

        function init() {

            //httpService.post(AddMakesApi, null)
            //         .then(function (response) {
            //             if (response.status == 200) {
            //                 $rootScope.showModal("importSuccess");

            //                 var $confirm = $("#importSuccess");
            //                 $("#redirectToIndex").off('click').click(function () {
            //                     $confirm.modal("hide");
            //                     window.location.href = "/Home/Index";
            //                 });
            //             }
            //         });

            httpService.get(testApi)
            .then(function (response) {
                return response.data;
            }).then(function (data) {
                if (data.makes && data.makesCount > 0) {
                    httpService.post(AddMakesApi, data.makes)
                      .then(function (response) {
                          if (response.status == 200) {
                              $rootScope.showModal("importSuccess");

                              var $confirm = $("#importSuccess");
                              $("#redirectToIndex").off('click').click(function () {
                                  $confirm.modal("hide");
                                  window.location.href = "/Home/Index";
                              });
                          }
                          else {
                              $rootScope.showModal("importFail");

                              var $confirm = $("#importFail");
                              $("#redirectToIndex").off('click').click(function () {
                                  $confirm.modal("hide");
                              });
                          }
                      });
                }
            });
        }

        function importData() {
            init();
        }
    }
})();