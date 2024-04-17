let storage = sessionStorage;
let link = window.location.origin;

function NumberOnly(e) {
    let event = e || window.event;
    let key_code = event.keyCode;
    let oElement = e ? e.target : window.event.srcElement;
    if (!event.shiftKey && !event.ctrlKey && !event.altKey) {
        if ((key_code > 47 && key_code < 58) ||
            (key_code > 95 && key_code < 106)) {
            if (key_code > 95)
                key_code -= (95 - 47);
            oElement.value = oElement.value;
        } else if (key_code == 8) {
            oElement.value = oElement.value;
        } else if (key_code != 9) {
            event.returnValue = false;
        }
    }
}

//// for true or false
function isNullOrEmpty(str) {
    let returnValue = false;
    if (!str
        || str == null
        || str === 'null'
        || str === ''
        || str === '{}'
        || str === 'undefined'
        || str.length === 0) {
        returnValue = true;
    }
    return returnValue;
}

/// for var return empty
function getNullOrEmpty(str) {
    let returnValue = $.trim(str);
    if (!str
        || str == null
        || str === 'null'
        || str === ''
        || str === '{}'
        || str === 'undefined'
        || str.length === 0) {
        returnValue = '';
    }
    return returnValue;
}

function doAjax(url, params, method) {
    let Result = "";
    $.ajax({
        'async': false,        
         url: link + '' + url,
        type: method || 'POST',
        contentType: "application/json",
        dataType: 'json',
        data: params,
        success: function (data) {
            Result = data;
        },
        beforeSend: function () {
            $('.loader').show();
        },
        complete: function () {
            setTimeout(function () { $('.loader').hide(); }, 500);
        },
        error: function (jqXHR, exception) {
            if (jqXHR.responseText.indexOf('session') > -1) {
                Logout();
            }
            else
                if (jqXHR.status === 0) {
                    alert('Not connect.\n Verify Network.');
                } else if (jqXHR.status == 404) {
                    alert('Requested page not found. [404]');
                } else if (jqXHR.status == 500) {
                    alert('Internal Server Error [500].');
                } else if (exception === 'parsererror') {
                    alert('Requested JSON parse failed.');
                } else if (exception === 'timeout') {
                    alert('Time out error.');
                } else if (exception === 'abort') {
                    alert('Ajax request aborted.');
                } else {
                    alert('Uncaught Error.\n' + jqXHR.responseText);
                }
        },
    });
    return Result;
}
 
function Datatable(Current) {
    $(Current).DataTable({
        dom: 'lBfrtip',
        "bSort": true,
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ]
    });
}

function destoryDataTable(Current) {
    if ($.fn.DataTable.isDataTable(Current)) {
        $(Current).DataTable().clear().destroy();
        $(Current).find('thead').empty();
        $(Current).find('tbody').empty();
        $(Current).find('tfoot').empty();
    }
}
 