var app = angular.module("myApp", []);
app.controller("myCtrl", function($scope, $http) {
    debugger;
    $scope.Create = function() {
        var Action = document.getElementById("btnSave").getAttribute("value");
        if (Action == "Submit") {
            $scope.BookLogDto = {};
            $scope.BookLogDto.StartTime = $scope.StartTime;
            $scope.BookLogDto.EndTime= $scope.EndTime;
            $scope.BookLogDto.BookId = $scope.BookId;
            $scope.BookLogDto.MemberId = $scope.MemberId;
            $scope.BookLogDto.Status = $scope.Status;
            $http({
                method: "post",
                url: "/BookLog/Create",
                datatype: "json",
                data: JSON.stringify($scope.BookLogDto)
            }).then(function(response) {
                alert(response.data);
                $scope.GetAllData();
                $scope.StartTime = "";
                $scope.EndTime = "";
                $scope.BookId = "";
                $scope.MemberId = "";
                $scope.Status = "";
            })
        } else {
            $scope.BookLogDto = {};
            $scope.BookLogDto.StartTime = $scope.StartTime;
            $scope.BookLogDto.EndTime= $scope.EndTime;
            $scope.BookLogDto.BookId = $scope.BookId;
            $scope.BookLogDto.MemberId = $scope.MemberId;
            $scope.BookLogDto.Status = $scope.Status;
            $scope.BookLogDto.Id = document.getElementById("BookLogID_").value;
            $http({
                method: "post",
                url: "/BookLog/Edit",
                datatype: "json",
                data: JSON.stringify($scope.BookLogDto)
            }).then(function(response) {
                alert(response.data);
                $scope.GetAllData();
                $scope.StartTime = "";
                $scope.EndTime = "";
                $scope.BookId = "";
                $scope.MemberId = "";
                $scope.Status = "";
                document.getElementById("btnSave").setAttribute("value", "Submit");
                document.getElementById("btnSave").style.backgroundColor = "cornflowerblue";
                document.getElementById("spn").innerHTML = "Add New BookLog";
            })
        }
    }
    $scope.GetAllData = function() {
        $http({
            method: "get",
            url: "/BookLog/Get_AllBookLog"
        }).then(function(response) {
            $scope.BookLogDto = response.data;
        }, function() {
            alert("Error Occur");
        })
    };
    $scope.Delete = function(BookLog) {
        $http({
            method: "post",
            url: "/BookLog/Delete",
            datatype: "json",
            data: JSON.stringify(BookLog)
        }).then(function(response) {
            alert(response.data);
            $scope.GetAllData();
        })
    };
    $scope.EditBookLog = function(BookLog) {
        document.getElementById("BookLogID_").value = BookLog.Id;
        $scope.StartTime = BookLog.StartTime;
        $scope.EndTime = BookLog.EndTime;
        $scope.BookId = BookLog.BookId;
        $scope.MemberId = BookLog.MemberId;
        $scope.Status = BookLog.Status;
        document.getElementById("btnSave").setAttribute("value", "Update");
        document.getElementById("btnSave").style.backgroundColor = "Yellow";
        document.getElementById("spn").innerHTML = "Update BookLog Information";
    }
})  