


$(document).ready(function () {
    loadData();
});

function ToJavaScriptDate(value) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var dt = new Date(parseFloat(results[1]));
    return (dt.getMonth() + 1) + "/" + dt.getDate() + "/" + dt.getFullYear();
}

function loadData() {
    $.ajax({
        url: "/TestAuthor/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.AuthorID + '</td>';
                html += '<td>' + item.AuthorName + '</td>';
                html += '<td>' + item.Email + '</td>';
                html += '<td>' + item.Phone + '</td>';
                html += '<td>' + ToJavaScriptDate(item.BirthDate) + '</td>';
                html += '<td>' + item.Address + '</td>';
                html += '<td><a href="#" onclick="return getbyID(' + item.AuthorID + ')">Edit</a> | <a href="#" onclick="Delele(' + item.AuthorID + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        AuthorID: $('#AuthorID').val(),
        AuthorName: $('#AuthorName').val(),
        Email: $('#Email').val(),
        Phone: $('#Phone').val(),
        BirthDate: $('#BirthDate').val(),

        Address: $('#Address').val()
    };
    $.ajax({
        url: "/TestAuthor/Add",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function getbyID(Id) {
    $('#AuthorName').css('border-color', 'lightgrey');
    $('#Email').css('border-color', 'lightgrey');
    $('#Phone').css('border-color', 'lightgrey');
    $('#BirthDate').css('border-color', 'lightgrey');
    $('#Address').css('border-color', 'lightgrey');
    $.ajax({
        url: "/TestAuthor/getbyID/" + Id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#AuthorID').val(result.AuthorID);
            $('#AuthorName').val(result.AuthorName);
            $('#Email').val(result.Email);
            $('#Phone').val(result.Phone);

            $('#BirthDate').val(result.BirthDate);
            $('#Address').val(result.Address);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        AuthorID: $('#AuthorID').val(),
        AuthorName: $('#AuthorName').val(),
        Email: $('#Email').val(),
        Phone: $('#Phone').val(),
        BirthDate: $('#BirthDate').val(),
        Address: $('#Address').val(),
    };
    $.ajax({
        url: "/TestAuthor/Update",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#AuthorID').val("");
            $('#AuthorName').val("");
            $('#Email').val("");
            $('#Phone').val("");
            $('#BirthDate').val("");

            $('#Address').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function Delele(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/TestAuthor/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}
function clearTextBox() {
    $('#AuthorID').val("");
    $('#AuthorName').val("");
    $('#Email').val("");
    $('#Phone').val("");
    $('#BirthDate').val("");

    $('#Address').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#AuthorName').css('border-color', 'lightgrey');
    $('#Email').css('border-color', 'lightgrey');
    $('#Phone').css('border-color', 'lightgrey');
    $('#BirthDate').css('border-color', 'lightgrey');

    $('#Address').css('border-color', 'lightgrey');
}
function validate() {
    var isValid = true;
    if ($('#AuthorName').val().trim() == "") {
        $('#AuthorName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#AuthorName').css('border-color', 'lightgrey');
    }
    if ($('#Email').val().trim() == "") {
        $('#Email').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Email').css('border-color', 'lightgrey');
    }
    if ($('#Phone').val().trim() == "") {
        $('#Phone').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Phone').css('border-color', 'lightgrey');
    }

    if ($('#BirthDate').val().trim() == "") {
        $('#BirthDate').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#BirthDate').css('border-color', 'lightgrey');
    }

    if ($('#Address').val().trim() == "") {
        $('#Address').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Address').css('border-color', 'lightgrey');
    }
    return isValid;
}