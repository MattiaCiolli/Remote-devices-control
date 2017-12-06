var app = angular.module('zeccaApp', []);

app.controller('requestCtrl', function ($scope, $compile) {
    $scope.setupRequest = function () {

        // remove and reinsert the element to force angular
        // to forget about the current element
        $('#button1').replaceWith($('#button1'));

        selectedDevice = $('#dispositivi').val();
        selectedFunctions = $('#funzioni').val();

        // change ng-click
        $('#button1').attr('ng-click', 'requestInfosByFunctions(' + selectedDevice + ', ' + selectedFunctions + ')');

        // compile the element
        $compile($('#button1'))($scope);
    };
});