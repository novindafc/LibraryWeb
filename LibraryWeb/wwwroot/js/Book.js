var app = angular.module("myApp", []);
app.controller("myCtrl", function($scope, $http) {
    debugger;
    $scope.Create = function() {
        var Action = document.getElementById("btnSave").getAttribute("value");
        if (Action == "Submit") {
            $scope.BookDto = {};
            $scope.BookDto.Title = $scope.Title;
            $scope.BookDto.Author = $scope.Author;
            $scope.BookDto.Position = $scope.Position;
            $scope.BookDto.Qty = $scope.Qty;
            $scope.BookDto.Remains = $scope.Remains;
            $http({
                method: "post",
                url: "/Book/Create",
                datatype: "json",
                data: JSON.stringify($scope.BookDto)
            }).then(function(response) {
                alert(response.data);
                $scope.GetAllData();
                $scope.Title = "";
                $scope.Author = "";
                $scope.Position = "";
                $scope.Qty = "";
                $scope.Remains = "";
            })
        } else {
            $scope.BookDto = {};
            $scope.BookDto.Title = $scope.Title;
            $scope.BookDto.Author = $scope.Author;
            $scope.BookDto.Position = $scope.Position;
            $scope.BookDto.Qty = $scope.Qty;
            $scope.BookDto.Remains = $scope.Remains;
            $scope.BookDto.Id = document.getElementById("Id").value = book.Id;
            $http({
                method: "post",
                url: "/Book/Edit",
                datatype: "json",
                data: JSON.stringify($scope.BookDto)
            }).then(function(response) {
                alert(response.data);
                $scope.GetAllData();
                $scope.Title = "";
                $scope.Author = "";
                $scope.Position = "";
                $scope.Qty = "";
                $scope.Remains = "";
                document.getElementById("btnSave").setAttribute("value", "Submit");
                document.getElementById("btnSave").style.backgroundColor = "cornflowerblue";
                document.getElementById("spn").innerHTML = "Add New Book";
            })
        }
    }
    $scope.GetAllData = function() {
        $http({
            method: "get",
            url: "/Book/Get_AllBook"
        }).then(function(response) {
            $scope.BookDto = response.data;
        }, function() {
            alert("Error Occur");
        })
    };
    $scope.Delete = function(book) {
        $http({
            method: "get",
            url: "/Book/Delete",
            datatype: "json",
            data: JSON.stringify(book)
        }).then(function(response) {
            alert(response.data);
            $scope.GetAllData();
        })
    };
    $scope.EditBook = function(book) {
        document.getElementById("Id").value = book.Id;
        $scope.Id = book.Id;
        $scope.Title = book.Title;
        $scope.Author = book.Author;
        $scope.Position = book.Position;
        $scope.Qty = book.Qty;
        $scope.Remains = book.Remains;
        document.getElementById("btnSave").setAttribute("value", "Update");
        document.getElementById("btnSave").style.backgroundColor = "Yellow";
        document.getElementById("spn").innerHTML = "Update Book Information";
    }
})  