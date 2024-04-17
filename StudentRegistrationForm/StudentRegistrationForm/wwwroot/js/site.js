$(function () {
    GetStudentData();
    function updateCharCount() {
        var maxLength = $('#AboutYourself').attr('maxlength');
        var textLength = $('#AboutYourself').val().length;
        $('#charCount').text(textLength + '/' + maxLength);
    }

    // Event listener for input in textarea
    $('#AboutYourself').on('input', function () {
        updateCharCount();
    });

    // Call updateCharCount initially to show character count
    updateCharCount();

});
let ParseState = "";
let ParseCity = "";
function GetStateAndCity() {
    let url = "/Home/GetStateandCity";
    let Result = doAjax(url, "", "GET");
    if (Result != null || Result != "") {

        ParseState = JSON.parse(Result).table;
        ParseCity = JSON.parse(Result).table1;

        if (ParseState != null && ParseState != "") {
            let stateOptions = "";
            for (let i = 0; ParseState.length > i; i++) {
                stateOptions = stateOptions + '<option value="' + ParseState[i].id + '">' + ParseState[i].statename + '</option>';
            }
            let select = '<select class="form-control" id="State" placeholder=""><option value="">--Select State--</option>\
            '+ stateOptions + '\
            </select>\
            <label for="State">State<sup class="text-danger">*</sup></label>';
            $("#selectStateAppend").empty();
            $("#selectStateAppend").append(select);
        }

        if (ParseCity != null && ParseCity != "") {
            let stateOptions = "";
            for (let i = 0; ParseCity.length > i; i++) {
                let selectedstateid = $("#State option:selected").val();
                if (selectedstateid == ParseCity[i].stateid) {
                    stateOptions = stateOptions + '<option value="' + ParseCity[i].stateid + '">' + ParseCity[i].cityname + '</option>';
                } else {
                    stateOptions = stateOptions + '';
                }

            }
            let select = '<select class="form-control" id="City" placeholder=""><option value="">--Select City--</option>\
            '+ stateOptions + '\
            </select>\
            <label for="City">City<sup class="text-danger">*</sup></label>';
            $("#selectCityAppend").empty();
            $("#selectCityAppend").append(select);
        }

        $("#State").change(function (e) {
            let stateid = $("#State option:selected").val();
            if (ParseCity != null && ParseCity != "") {
                let stateOptions = "";
                for (let i = 0; ParseCity.length > i; i++) {

                    if (stateid == ParseCity[i].stateid) {
                        stateOptions = stateOptions + '<option value="' + ParseCity[i].stateid + '">' + ParseCity[i].cityname + '</option>';
                    } else {
                        stateOptions = stateOptions + '';
                    }

                }
                let select = '<select class="form-control" id="City" placeholder="">\
            '+ stateOptions + '\
            </select>\
            <label for="City">City<sup class="text-danger">*</sup></label>';
                $("#selectCityAppend").empty();
                $("#selectCityAppend").append(select);
            }

        });
    }

    $("#exampleModalLabel").text("Registration Form")
    $("#SubmitBtn").text('Submit');
    $("#SubmitBtn").attr('onclick', 'fnSaveUserData($(this))');
    $('#myForm').find('input[type=text], input[type=select], input[type=textarea], input[type=checkbox], input[type=radio]').val('');
    $('#myForm').find('textarea').val('');
    $('#myForm').find('select').prop('selectedIndex', 0);
    $('#myForm').find('input[type=checkbox], input[type=radio]').prop('checked', false);
}

$('#Email').on('input', function () {
    var email = $(this).val();
    if (validateEmail(email)) {
        $('#validationMessage').text('Email is valid').css('color', 'green');
    } else {
        $('#validationMessage').text('Email is not valid').css('color', 'red');
    }
});

function validateEmail(email) {
    var re = /\S+@\S+\.\S+/;
    return re.test(email);
}

$('#Mobile').on('input', function () {
    var phoneNumber = $(this).val();
    // Remove non-numeric characters from input
    phoneNumber = phoneNumber.replace(/\D/g, '');
    // Limit input to 10 characters
    phoneNumber = phoneNumber.slice(0, 10);
    $(this).val(phoneNumber);
    if (validatePhoneNumber(phoneNumber)) {
        $('#validationmobileMessage').text('Phone number is valid').css('color', 'green');
    } else {
        $('#validationmobileMessage').text('Please enter 10 digits').css('color', 'red');
    }
});

// Prevent 'e' character input in number field
$('#Mobile').on('keydown', function (e) {
    if (e.key === 'e') {
        e.preventDefault();
    }
});

function validatePhoneNumber(phoneNumber) {
    return phoneNumber.length === 10;
}

function fnSelectStateFirst() {
    alert("Please select state first");
}

function fnSaveUserData(current) {
    let Name = $.trim($("#Name").val()),
        Email = $.trim($("#Email").val()),
        Mobile = $.trim($("#Mobile").val()),
        State = $.trim($("#State option:selected").text()),
        City = $.trim($("#City option:selected").text()),
        AboutYourself = $.trim($("#AboutYourself").val()),
        inputGroupFile01 = $.trim($('#imagePreview img').attr('src'));

    let requestData = {
        Name: Name,
        Email: Email,
        Mobile: Mobile,
        State: State,
        City: City,
        AboutYourself: AboutYourself,
        inputGroupFile01: inputGroupFile01
    };

    let url = "/Home/InsertStudentData";
    let Result = doAjax(url, JSON.stringify(requestData), "POST");
    if (Result != "") {
        var splitData = Result.split("|");
        if (splitData[0] == "N") {
            alert(splitData[1]);
        } else {
            var d = JSON.parse(splitData[1]).table;
            alert(d[0].result);
            $("#exampleModalLabel").text("Registration Form");
            $("#SubmitBtn").text('Submit');
            $("#SubmitBtn").attr('onclick', 'fnSaveUserData($(this))');
            $('#myForm').find('input[type=text], input[type=select], input[type=textarea], input[type=checkbox], input[type=radio]').val('');
            $('#myForm').find('textarea').val('');
            $('#myForm').find('select').prop('selectedIndex', 0);
            $('#myForm').find('input[type=checkbox], input[type=radio]').prop('checked', false);
            GetStudentData();
            $("#staticBackdrop").modal('hide');
        }

    }

}



function GetStudentData() {
    let url = "/Home/GetStudentData";
    let Result = doAjax(url, "", "GET");
    if (Result != "" && Result != null) {
        let DataList = JSON.parse(Result).table;
        var i = 1;
        $("#StudentDatatable").DataTable().clear().destroy();
        $("#StudentDatatable").DataTable({
            data: DataList,
            columns: [
                {
                    render: function (data, type, row) {
                        var imgurl = "<img class='w-100 h-100' style='border-radius:100%;' src='https://as2.ftcdn.net/v2/jpg/04/70/29/97/1000_F_470299797_UD0eoVMMSUbHCcNJCdv2t8B2g1GVqYgs.jpg'/>";
                        if (row.uploadphotourl != "" && row.uploadphotourl != null) {
                            imgurl = "<img class='w-100 h-100' style='border-radius:100%;' src='../uploadedimages/" + row.uploadphotourl + "'/>";
                        }
                        return "<div class='d-flex'><div class='image-div'>" + imgurl + "</div>" + row.name + "</div>";
                    }
                },
                {
                    render: function (data, type, row) {
                        return "<div>" + row.email + "</div>";
                    }
                },
                {
                    render: function (data, type, row) {
                        return "<div>" + row.mobile + "</div>";
                    }
                },
                {
                    render: function (data, type, row) {
                        return "<div>" + row.state + "</div>";
                    }
                },
                {
                    render: function (data, type, row) {
                        return "<div>" + row.city + "</div>";
                    }
                },
                {
                    render: function (data, type, row) {
                        return "<div>" + row.aboutyourself + "</div>";
                    }
                },
                {
                    render: function (data, type, row) {
                        return "<div><i updateid='" + row.id + "' onclick='fnUpdateData($(this))' class='fa fa-edit text-primary'></i>&nbsp;&nbsp;<i deleteid='" + row.id + "' onclick='fndeleteData($(this))' class='fa fa-trash text-danger'></i></div>";
                    }
                },

            ],
        });
    }
}



function fnUpdateData(current) {
    GetStateAndCity();
    $("#staticBackdrop").modal('show');
    $("#exampleModalLabel").text("Update Student Details")
    $("#SubmitBtn").text('Update');
    $("#SubmitBtn").attr('onclick', 'fnUpdateStudentDetails($(this),' + current.attr("updateid") + ')');
    $("#SubmitBtn").attr('updateid', current.attr("updateid"));
    let url = "/Home/updatestudentdata";

    let requestData = {
        StudentId: current.attr("updateid")
    };


    let Result = doAjax(url, JSON.stringify(requestData));
    if (Result != "" && Result != null) {
        let DataList = JSON.parse(Result).table;
        $("#Name").val(DataList[0].name);
        $("#Email").val(DataList[0].email);
        $("#Mobile").val(DataList[0].mobile);
        $("#State option").filter(function () {
            //may want to use $.trim in here
            return $(this).text() == DataList[0].state;
        }).prop('selected', true);


        if (ParseCity != null && ParseCity != "") {
            let stateOptions = "";
            for (let i = 0; ParseCity.length > i; i++) {
                let selectedstateid = $("#State option:selected").val();
                if (selectedstateid == ParseCity[i].stateid) {
                    stateOptions = stateOptions + '<option value="' + ParseCity[i].stateid + '">' + ParseCity[i].cityname + '</option>';
                } else {
                    stateOptions = stateOptions + '';
                }

            }
            let select = '<select class="form-control" id="City" placeholder=""><option value="">--Select City--</option>\
            '+ stateOptions + '\
            </select>\
            <label for="City">City<sup class="text-danger">*</sup></label>';
            $("#selectCityAppend").empty();
            $("#selectCityAppend").append(select);
        }

        $("#City option").filter(function () {
            //may want to use $.trim in here
            return $(this).text() == DataList[0].city;
        }).prop('selected', true);

        $("#AboutYourself").val(DataList[0].aboutyourself);


        var imgurl = "https://as2.ftcdn.net/v2/jpg/04/70/29/97/1000_F_470299797_UD0eoVMMSUbHCcNJCdv2t8B2g1GVqYgs.jpg";
        if (DataList[0].uploadphotourl != "" && DataList[0].uploadphotourl != null) {
            imgurl = DataList[0].uploadphotourl;
        }
        $('#imagePreview').empty()
        $('#imagePreview').append('<div class="col-6 mb-2"><div style="padding: 5px;background: #e0e0e0;border-radius: 5px;"><div style="height: 90px; background:#fff;"><img style="width:100%;height:100%;" class="" src="../uploadedimages/' + imgurl + '"></div><div class="imageremove"><i class="fa fa-trash"></i>&nbsp;&nbsp;Remove</div></div></div>');
    }

}


function fnUpdateStudentDetails(current) {
    let Name = $.trim($("#Name").val()),
        Email = $.trim($("#Email").val()),
        Mobile = $.trim($("#Mobile").val()),
        State = $.trim($("#State option:selected").text()),
        City = $.trim($("#City option:selected").text()),
        AboutYourself = $.trim($("#AboutYourself").val()),
        inputGroupFile01 = $.trim($('#imagePreview img').attr('src'));

    let requestData = {
        StudentId: current.attr('updateid'),
        Name: Name,
        Email: Email,
        Mobile: Mobile,
        State: State,
        City: City,
        AboutYourself: AboutYourself,
        inputGroupFile01: inputGroupFile01
    };

    let url = "/Home/StudentDataUpdate";
    let Result = doAjax(url, JSON.stringify(requestData), "POST");
    if (Result != "") {
        var splitData = Result.split("|");
        if (splitData[0] == "N") {
            alert(splitData[1]);
        } else {
            var d = JSON.parse(splitData[1]).table;
            alert(d[0].result);
            $("#exampleModalLabel").text("Registration Form");
            $("#SubmitBtn").text('Submit');
            $("#SubmitBtn").attr('onclick', 'fnSaveUserData($(this))');
            $('#myForm').find('input[type=text], input[type=select], input[type=textarea], input[type=checkbox], input[type=radio]').val('');
            $('#myForm').find('textarea').val('');
            $('#myForm').find('select').prop('selectedIndex', 0);
            $('#myForm').find('input[type=checkbox], input[type=radio]').prop('checked', false);
            GetStudentData();
            $("#staticBackdrop").modal('hide');
        }

    }

}


function fndeleteData(current) {
    let url = "/Home/Deletestudentdata";
    let requestData = {
        StudentId: current.attr("deleteid")
    };
    let Result = doAjax(url, JSON.stringify(requestData));
    if (Result != "") {
        var d = JSON.parse(Result).table;
        GetStudentData();
        alert(d[0].result);
    }
}



function readImage(input) {
    if (input.files && input.files[0]) {
        var fileSizeInBytes = input.files[0].size;
        var fileSizeInKB = fileSizeInBytes / 1024; // Convert bytes to KB
        if (fileSizeInKB > 250) {
            alert('Image size is too big. Please select an image size less than 250 KB.');
            return false;
        }
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#imagePreview').empty()
            $('#imagePreview').append('<div class="col-6 mb-2"><div style="padding: 5px;background: #e0e0e0;border-radius: 5px;"><div style="height: 90px; background:#fff;"><img style="width:100%;height:100%;" class="" src="' + e.target.result + '"></div><div class="imageremove"><i class="fa fa-trash"></i>&nbsp;&nbsp;Remove</div></div></div>');
            removeimg();

        }
        reader.readAsDataURL(input.files[0]);
    }
};

$("#inputGroupFile01").change(function (e) {
    readImage(this)
});

function removeimg() {
    $(".imageremove").click(function (e) {
        e.stopImmediatePropagation();
        $(this).parent().parent().remove();
    });
    $("#inputGroupFilevisitingcard").val('');
}
