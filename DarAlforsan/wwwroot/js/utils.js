const AscOrder = "asc";
const DescOrder = "desc";
let isminify = false;
let language = '';
isfullscrean = false;
var spinner = {
    enabled: true
}
var dataTable =
{
    table: null
}
var idle = {
    isEnabled: false
}
//EXResultStatus
var EXResultStatus = { None: "-1", Fail: "0", Success: '1' }
document.addEventListener("DOMContentLoaded", function (event) {
    // initalize ajax setup config
    initAjaxSetup();
    setInterval(updateCurrentTime, 1000);

});
function updateCurrentTime() {
    $("#current-time").text(DateFormat.getTime(new Date), language, true);
}
function changeBranch() {
    let value = $('#User-Branch').val();
    window.location.href = `/Common/Account/ChangeBranch?BranchID=${value}`;
}
function changeUILanguage(lang) {

    window.location.href = `/Common/Account/ChangeUILanguage?UILanguageID=${lang}`;
}
function delete_cookie(name) {
    document.cookie = name + '=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;';
}
function createMenuCookie(title, img, id, url) {
    debugger;
    delete_cookie(title);
    var expires = ";expires=" + 60;
    var path = ";path=" + "/";
    document.cookie = 'SubMenuTitle' + "=" + title + expires + path;
    document.cookie = 'SubMenuID' + "=" + id + expires + path;
    document.cookie = 'SubMenuImg' + "=" + img + expires + path;
    if (url != "") {
        location.href = url;
    }
}
const ui = {
    confirm: async (message) => createConfirm(message)
}
const createConfirm = (message) => {
    return new Promise((complete, failed) => {
        $('#confirmMessage').text(message)


        debugger;
        $('#confirmYes').on('click', () => { $('.confirm').hide(); complete(true); });
        $('#confirmNo').on('click', () => { $('.confirm').hide(); complete(false); });

        $("#modalconfirmation").bsModal("show");
    });
}
const saveForm = async () => {
    $("#modalconfirmation").bsModal("show");
    const confirm = await ui.confirm('Are you sure you want to do this?');

    if (confirm) {
        alert('yes clicked');
        $("#modalconfirmation").bsModal("hide");
    } else {
        alert('no clicked');
        $("#modalconfirmation").bsModal("hide");
    }
}
$(document).ready(function () {
    //JQUERY UI DATEPICKER
    $('#confirmYes').on('click', function () {
        $("#modalconfirmation").bsModal("hide");
    });
    $('#confirmNo').on('click', function () {
        $("#modalconfirmation").bsModal("hide");
    });

    $(".jq-datepicker").datepicker(
        {
            dateFormat: "dd/mm/yyyy",
            format: "dd/mm/yyyy",
            changeMonth: "true", /*drop down list for month*/
            changeYear: "true",/*drop down list for year*/
            yearRange: "1950:2100", /*-25:+50*/
            //minDate: new Date(2017, 0, 1),
            //maxDate: new Date(2019, 0, 1),
            /*showOn:"both"*/
        }
    );


    //CKEDITOR
    //convert any text area with attribute ckeditor to html ck editor
    $(function () {
        $('[ckeditor]').ckeditor();
    });

    //CUSTOM FILE INPUT USING BOOTSTRAP
    //Fix Bootstrap bug (Label text - choose file - does not change after file selection)
    $('input[type="file"]').change(function (e) {
        //get selected file name
        var fileName = e.target.files[0].name;
        //get ID
        var id = $(this).attr("id");
        //change its label text to file name
        $(' .custom-file-label[for=\'' + id + '\']').html(fileName);
    });

    //CASCADING LIST
    initCascadingList();
    //copy Tex

});
function filterMenu() {
    var filter;
    filter = $(".filter-menu-search").val().toLowerCase();
    $(".parent-menu-item").each(function (index, el) {
        var value = $(el).text();
        if (value.toLowerCase().indexOf(filter) > -1) {
            $(el).closest('li').show();;

        } else {
            $(el).closest('li').hide();
        }
    })

};
function filterHeaderchat() {
    var filter;
    filter = $(".filter-user-chat").val().toLowerCase();
    $(".clearfix").each(function (index, el) {
        debugger;
        var value = $(el).attr('data-text');
        if (value.toLowerCase().indexOf(filter) > -1) {
            $(el).show();;

        } else {
            $(el).hide();
        }
    })
};

function initAjaxSetup() {
    //Global Ajax Functions
    $.ajaxSetup({
        beforeSend: function () {
            if (spinner.enabled == true) {
                // show progress spinner
                $('#spinner').show();
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            if (idle.isEnabled) {
                if (jqXHR.status == 401) {
                    window.location.href = "/common/account/login";
                } else {
                    console.log("Error: " + textStatus + ": " + errorThrown);
                }
            }
        },
        complete: function () {
            // hide progress spinner
            $('#spinner').hide();
            //if (sessionChecker != undefined ) {
            //    sessionChecker.sessionStartDate = new Date();
            //}
        }
    });
}

//CASCADING LIST Initialization
function initCascadingList() {
    //get all list with attribute cascading-list
    $('[cascading-list]').on('change', '', function (e) {

        try {
            var list = $(this);
            $.ajax({
                type: "GET",
                url: list.attr('data-url'),//action url
                contentType: "application/json; charset=utf-8",
                data: list.attr('data-param') + "=" + list.val(), //action params
                dataType: "html",
                success: function (result, status, xhr) {
                    //remove all items from the list except the first item
                    $("#" + list.attr('dest-id')).find('option:not(:first)').remove();

                    //parse json result
                    data = $.parseJSON(result);

                    $.each(data, function (i, item) {
                        //console.log(item[list.attr('dest-data-text')]);
                        $("#" + list.attr('dest-id')).append($('<option>', {
                            value: item[list.attr('dest-data-val')],
                            text: item[list.attr('dest-data-text')]
                        }));
                    });

                    //clear related list
                    rListIDs = list.attr('related-list-id');
                    if (rListIDs != undefined) {
                        IDs = rListIDs.split(',');
                        $.each(IDs, function (i, ID) {
                            $("#" + ID).find('option:not(:first)').remove();
                        });
                    }
                },
                error: function (xhr, status, error) {
                    alert("Result: " + status + " " + error + " " + xhr.status + " " + xhr.statusText)
                }
            });
        }
        catch (error) {
            console.log(error);
        }
        finally {
        }
    });
}

//Re initialize form validation when form exist in partial view 
function initFormValidation(formID) {
    $("from#" + formID).removeData("validator");
    $("form#" + formID).removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse($("form#" + formID));
}

//Custom Check Box - Radio & Option Box
function changeOptionBoxStatus(container, defaultCss, checkedCss) {

    val = $(container).find("input[type=hidden]").val();

    //clear previous selection
    var groupName = $(container).attr("group-name");

    if (val.toLowerCase() == 'true') {
        //if item check and the user click on a checked item with group name, then do nothing
        if (groupName != undefined) {
            return;
        }
        $(container).find("input[type=hidden]").val("False");
        $(container).attr('class', defaultCss);
    }
    else {


        if (groupName != undefined) {

            //remove all checks 
            $("div[group-name='" + groupName + "']").each(function (index) {
                $(this).find("input[type=hidden]").val("False");
                $(this).attr('class', defaultCss);
            });
        }

        $(container).find("input[type=hidden]").val("True");
        $(container).attr('class', checkedCss);
    }
}

//custom Date Time Picker 

function onDateTimeChanged(ctrl) {

    var newDateParts = new Array();
    var name = ctrl.name.substring(0, ctrl.name.lastIndexOf("."));

    var day = $("select[name='" + name + ".DAY']").val();
    var month = $("select[name='" + name + ".MONTH']").val();
    var year = $("select[name='" + name + ".YEAR']").val();

    var hour = $("select[name='" + name + ".HOUR']").val();
    var minute = $("select[name='" + name + ".MINUTE']").val();

    if (hour == null || hour.length == 0) {
        hour = 0
    }
    if (minute == null || minute.length == 0) {
        minute = 0
    }

    newDateParts.push(month);
    newDateParts.push(day);
    newDateParts.push(year);

    var cDateTime = $("input[name='" + name + "']").val();
    var cDateParts = cDateTime.split(" ");

    var newDateTime = newDateParts[0] + "/" + newDateParts[1] + "/" + newDateParts[2] + " " + hour + ":" + minute + ":00";

    $("input[name='" + name + "']").val();
    $("input[name='" + name + "']").val(newDateTime);
}
// Form object 
var forms = {
    // Get Form Body _ help's for ajax requests 
    getSerializedFormData: function getSerializedFormData(form) {
        var list = form.serializeArray();
        var obj = {};
        $.each(list, function (i, v) {
            if (v["name"] != "__RequestVerificationToken") {
                obj[v['name']] = v["value"];
            }
        });
        return obj;
    }
};

function validateSelect2(form) {
    $("#" + form + " select.select2.input-validation-error").each(function (i, val) {
        var ele = $(val).nextAll('span.select2').first();
        $(ele).addClass("input-validation-error");
    });
    $("#" + form + " select.select2.input-validation-error").change(function () {
        var ele = $(this).nextAll('span.select2').first();
        $(ele).removeClass("input-validation-error");
    });
}



$(document).ready(function () {
    $(".lists_top_links2 .nav-pills .nav-link").on("click", function (e) {
        e.preventDefault();
        $(".lists_top_links2 .nav-pills .nav-link").removeClass("active");
        $(this).addClass("active");
        $("." + $(this).attr("data-tab")).show().siblings().hide();
    });
});

$(document).ready(function () {

    $(".open_lists").on("click", function (e) {
        e.stopPropagation();
        $(".outer_lists").removeClass("active");
        $(this).toggleClass("active");
        if ($(".open_lists").hasClass("active")) {
            $(this).next(".outer_lists").addClass("active");
        } else {
            $(this).next(".outer_lists").removeClass("active");
        }
    });
    $("body").on("click", function () {
        $(".open_lists.active").click();
        $(".open_setting.active").click();
    });

    $(".outer_lists").on("click", function (e) {
        e.stopPropagation();
    });

    $(".open_setting").on("click", function (e) {
        e.stopPropagation();
        $(".setting_fixed").addClass("active");

    });

    $(".close_setting").on("click", function () {
        debugger;
        $(".setting_fixed.active").removeClass("active");
    });

    $(".dropdown-icon-menu .header-btn").on("click", function (e) {
        debugger;
        e.preventDefault();
        $(".dropdown-icon-menu .header-btn").toggleClass("active");
        sessionStorage.setItem("Minify", $(".dropdown-icon-menu .header-btn").attr("class"));
        if ($(this).hasClass("active")) {
            $(".side-menu-wrapper").addClass("active");
            $(".page-wrapper").addClass("active");
            $(".header-fixed").addClass("active");
            $(".btn_terminal , .dropdown-icon-menu:hover > ul").hide();
        } else {

            $(".side-menu-wrapper").removeClass("active");
            $(".page-wrapper").removeClass("active");
            $(".header-fixed").removeClass("active");
            $(".btn_terminal , .dropdown-icon-menu:hover > ul").show();
        }
    });

    $(".btn_terminal").on("click", function (e) {
        $(".menuitems").each(function () {
            $(this).css('height', '');
            $(this).removeClass("in");
        });
        e.preventDefault();
        $(".btn_terminal").toggleClass("active2");
        debugger;
        sessionStorage.setItem("UnMinify", $(".btn_terminal").attr("class"));
        if ($(this).hasClass("active2")) {
            $(".parentMenu").each(function (e) {
                $(this).removeClass("active");
            });
            $(".menuitems  ").each(function (e) {
                $(this).removeClass("in");
            });
            $(".terminalicon").addClass("d-none");
            $(".arrowicon").removeClass("d-none");
            $(".side-menu-wrapper").addClass("active2");
            $(".side-menu-wrapper").removeClass("active1");
            $(".page-wrapper").addClass("active2");
            $(".header-fixed").addClass("active2");
            $(".filter-menu-search").removeAttr("placeholder");
            $(".today-wrapperDate").addClass("d-none");
        }
        else {
            $(".terminalicon").removeClass("d-none");
            $(".arrowicon").addClass("d-none");
            $(".parentMenu").each(function (e) {
                $(this).removeClass("active");
            });
            $(".side-menu-wrapper").removeClass("active2");
            $(".side-menu-wrapper").addClass("active1");
            $(".page-wrapper").removeClass("active2");
            $(".header-fixed").removeClass("active2");
            $(".today-wrapperDate").removeClass("d-none");
        }
    });
});

$(window).on('resize', function () {
    if (window.innerWidth <= 768) {
        if ($('.side-menu-wrapper').hasClass('active2')) {
            isminify = true;
            $('.side-menu-wrapper').removeClass('active2');
        }
    }
    else {
        if (isminify) {
            $('.side-menu-wrapper').addClass('active2');
            isminify = false;
        }
    }
});
function toggelMenu() {
    debugger
    var unMinify = sessionStorage.getItem("UnMinify");
    $(".btn_terminal").addClass(unMinify);
    if ($(".btn_terminal").hasClass("active2")) {
        debugger;
        $(".terminalicon").addClass("d-none");
        $(".arrowicon").removeClass("d-none");
        $(".side-menu-wrapper").addClass("active2");
        $(".side-menu-wrapper").removeClass("active1");
        $(".page-wrapper").addClass("active2");
        $(".header-fixed").addClass("active2");
        $(".menuitems").each(function (e) {

            if ($(this).hasClass("in")) {
                debugger;
                $(this).removeClass("in");
            }
        });
        $(".filter-menu-search").removeAttr("placeholder");

        $(".today-wrapperDate").addClass("d-none");
    } else {
        debugger;
        $(".terminalicon").removeClass("d-none");
        $(".arrowicon").addClass("d-none");
        $(".side-menu-wrapper").removeClass("active2");
        $(".page-wrapper").removeClass("active2");
        $(".header-fixed").removeClass("active2");
        $(".today-wrapperDate").removeClass("d-none");
    }
}
$(window).on('load', function () {
    toggelMenu();
    spinner.enabled = true;
});
//$(function () {
//    toggelMenue();
//});