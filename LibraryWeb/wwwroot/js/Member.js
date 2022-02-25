var app = angular.module("myApp", []);
app.controller("myCtrl", function($scope, $http) {
    debugger;
    $scope.Create = function() {
        var Action = document.getElementById("btnSave").getAttribute("value");
        if (Action == "Submit") {
            $scope.MemberDto = {};
            $scope.MemberDto.Name = $scope.Name;
            $scope.MemberDto.Gender = $scope.Gender;
            $scope.MemberDto.Phone = $scope.Phone;
            $scope.MemberDto.Occupation = $scope.Occupation;
            $scope.MemberDto.Email = $scope.Email;
            $http({
                method: "post",
                url: "/Member/Create",
                datatype: "json",
                data: JSON.stringify($scope.MemberDto)
            }).then(function(response) {
                alert(response.data);
                $scope.GetAllData();
                $scope.Name = "";
                $scope.Gender = "";
                $scope.Phone = "";
                $scope.Occupation = "";
                $scope.Email = "";
            })
        } else {
            $scope.MemberDto = {};
            $scope.MemberDto.Name = $scope.Name;
            $scope.MemberDto.Gender = $scope.Gender;
            $scope.MemberDto.Phone = $scope.Phone;
            $scope.MemberDto.Occupation = $scope.Occupation;
            $scope.MemberDto.Email = $scope.Email;
            $scope.MemberDto.Id = document.getElementById("MemberID_").value;
            $http({
                method: "post",
                url: "/Member/Edit",
                datatype: "json",
                data: JSON.stringify($scope.MemberDto)
            }).then(function(response) {
                alert(response.data);
                $scope.GetAllData();
                $scope.Name = "";
                $scope.Gender = "";
                $scope.Phone = "";
                $scope.Occupation = "";
                $scope.Email = "";
                document.getElementById("btnSave").setAttribute("value", "Submit");
                document.getElementById("btnSave").style.backgroundColor = "cornflowerblue";
                document.getElementById("spn").innerHTML = "Add New Member";
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
            method: "post",
            url: "/Book/Delete",
            datatype: "json",
            data: JSON.stringify(book)
        }).then(function(response) {
            alert(response.data);
            $scope.GetAllData();
        })
    };
    $scope.EditMember = function(book) {
        document.getElementById("bookID_").value = book.Id;
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