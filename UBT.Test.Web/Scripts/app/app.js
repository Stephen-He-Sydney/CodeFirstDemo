(function () {
    'use strict';
    window.app = angular
       .module('ubtTestModel', ['ui.bootstrap']);

    window.app.run(['$rootScope', function ($rootScope) {
        $rootScope.showModal = function (id) {
            var height = $(window).height();
            var heightModal = 205;
            $("#" + id + " " + ".modal-dialog").height(heightModal)
            var marginTop = (height - heightModal) / 2;
            $("#" + id + " " + ".modal-dialog").css("margin-top", marginTop - marginTop * 0.2);
            $("#" + id).modal("show");
        }
    }]);
})();