let contactUs = {
    initDatatable: function () {
        let columns =
            [
                {
                    data: "ID",
                    name: "ID",
                    className: "d-none"
                },
                {
                    data: "Name",
                    name: "Name",
                    className: "text-center"
                },
                {
                    data: "Email",
                    name: "Email",
                    className: "text-center"
                },
                {
                    data: "MobileNo",
                    name: "MobileNo",
                    className: "text-center"
                },
                {
                    data: "Message",
                    className: "text-center",
                    render: function (data) {
                        let message = contactUs.extractContent(data);
                        let str = message.length > 30 ? message.substring(0, 30) + " ..." : message;
                        return `<span title="${message}">${str}</span>`;
                    }
                },
                {
                    data: "ID",
                    render: function (data) {
                        return `<a class="center-block" onclick="deleteRecord(${data})"><i class="fa fa-trash text-danger"></i></a>`;
                    },
                    className: "text-center grid-cmd",
                    orderable: false
                }
            ];
        initDataTable("list", "ContactUs/GetList", "", "ContactUs/Remove", "", columns, 0, DescOrder);
    },
    //---------------------------------------------------------------------------------------------
    initAdd: function initAdd() {
        try {
            $.ajax({
                url: "Basic/ContactUs/New",
                contentType: "application/html ; charset:utf-8",
                type: "Get",
                dataType: "html",
                success: function (data) {
                    $("#divContactUsBody").html(data);
                    $("#AddContactUs").bsModal("show");
                }
            })
        }
        catch (error) {
            console.log(error);
        }
        finally {
            $("#AddContactUs").bsModal("hide");
        }
    },
    //---------------------------------------------------------------------------------------------
    //initEdit: function initEdit(id) {
    //    try {
    //        $.ajax({
    //            url: "ContactUs/Edit",
    //            data: { ID: id },
    //            contentType: 'application/html ; charset:utf-8',
    //            type: "Get",
    //            dataType: "html",
    //            success: function (data) {
    //                $("#divContactUsBody").html(data);
    //                DragDrop.InitDropZone();
    //                $("#AddContactUs").bsModal("show");
    //                $.validator.unobtrusive.parse($("#form-ContactUs"));
    //            }
    //        })
    //    } catch (error) {
    //        console.log(error);
    //    }
    //},
    //---------------------------------------------------------------------------------------------
    onformKeypress: function (formName, event) {
        if (event.keyCode === 13) {
            contactUs.save(formName);
        }
    },
    //---------------------------------------------------------------------------------------------
    save: function save(formId) {
        try {
            let name = $("#Name").val();
            let email = $("#Email").val();
            let mobileNo = $("#MobileNo").val();
            let message = $("#Message").val();

            if (name == "") {
                $("#Name").addClass("input-validation-error");
                return;
            }
            else
                $("#Name").removeClass("input-validation-error");
            if (email == "") {
                $("#Email").addClass("input-validation-error");
                return;
            }
            else
                $("#Email").removeClass("input-validation-error");
            if (mobileNo == "") {
                $("#MobileNo").addClass("input-validation-error");
                return;
            }
            else
                $("#MobileNo").removeClass("input-validation-error");
            if (message == "") {
                $("#Message").addClass("input-validation-error");
                return;
            }
            else
                $("#Message").removeClass("input-validation-error");

            let form = document.getElementById(formId);
            let data = new FormData(form);

            $.ajax(
                {
                    type: "Post",
                    url: "Basic/ContactUs/Save",
                    data: data,
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (result) {
                        if (result.status === 0) {
                            alert(result.message);
                        }
                        else {
                            //alert(result.message);
                            $("#AddContactUs").bsModal("hide");
                        }
                    }
                });
        } catch (error) {
            console.log(error);
        }
    },
    //---------------------------------------------------------------------------------------------
    refreshDatatable: function () {
        $("#list").DataTable().ajax.reload(null, false);
    },
    //---------------------------------------------------------------------------------------------
    extractContent: function (s) {
        let span = document.createElement("span");
        span.innerHTML = s;
        return span.textContent || span.innerText;
    }
    //---------------------------------------------------------------------------------------------
}