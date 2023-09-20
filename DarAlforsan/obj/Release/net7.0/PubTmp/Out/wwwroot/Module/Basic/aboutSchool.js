let aboutSchool = {
    initDatatable: function () {
        let columns =
            [
                {
                    data: "ID",
                    name: "ID",
                    className: "d-none"
                },
                {
                    data: "AddressAr",
                    name: "AddressAr",
                    className: "local-font local-column"
                },
                {
                    data: "AddressEn",
                    name: "AddressEn",
                    className: "latin-font latin-column"
                },
                {
                    data: "MobileNo",
                    name: "MobileNo",
                    className: "text-center"
                },
                {
                    data: "WhatsAppNo",
                    name: "WhatsAppNo",
                    className: "text-center"
                },
                {
                    data: "Email",
                    name: "Email",
                    className: "text-center"
                },
                {
                    data: "DetailsAr",
                    className: "local-font local-column",
                    render: function (data) {
                        let details = aboutSchool.extractContent(data);
                        let str = details.length > 30 ? details.substring(0, 30) : details;
                        return `<span title="${details}">${str}</span>`;
                    }
                },
                {
                    data: "DetailsEn",
                    className: "latin-font latin-column",
                    render: function (data) {
                        let details = aboutSchool.extractContent(data);
                        let str = details.length > 30 ? details.substring(0, 30) : details;
                        return `<span title="${details}">${str}</span>`;
                    }
                },
                {
                    data: "ID",
                    render: function (data) {
                        return `<a class="center-block" onclick="aboutSchool.initEdit(${data})"><i class="fas fa-pencil-alt text-primary"></i></a>`;
                    },
                    className: "text-center grid-cmd",
                    orderable: false
                },
                {
                    data: "ID",
                    render: function (data) {
                        return `<a class="center-block"  onclick="deleteRecord(${data})"><i class="fa fa-trash text-danger"></i></a>`;
                    },
                    className: "text-center grid-cmd",
                    orderable: false
                }
            ];
        initDataTable("list", "GetList", "", "Remove", "", columns, 0, DescOrder);
    },
    //---------------------------------------------------------------------------------------------
    initAdd: function initAdd() {
        try {
            $.ajax({
                url: "New",
                contentType: "application/html ; charset:utf-8",
                type: "Get",
                dataType: "html",
                success: function (data) {
                    $("#divAboutSchoolBody").html(data);
                    DragDrop.InitDropZone();
                    $("#AddAboutSchool").bsModal("show");
                    let editorAr = CKEDITOR.instances['DetailsAr'];
                    let editorEn = CKEDITOR.instances['DetailsEn'];
                    if (editorAr) {
                        CKEDITOR.instances['DetailsAr'].destroy(true);
                    }
                    if (editorEn) {
                        CKEDITOR.instances['DetailsEn'].destroy(true);
                    }
                    $('#DetailsAr').ckeditor();
                    $('#DetailsEn').ckeditor();
                }
            })
        }
        catch (error) {
            console.log(error);
        }
        finally {
            $("#AddAboutSchool").bsModal("hide");
        }
    },
    //---------------------------------------------------------------------------------------------
    initEdit: function initEdit(id) {
        try {
            $.ajax({
                url: "Edit",
                data: { ID: id },
                contentType: 'application/html ; charset:utf-8',
                type: "Get",
                dataType: "html",
                success: function (data) {
                    $("#divAboutSchoolBody").html(data);
                    DragDrop.InitDropZone();
                    $("#AddAboutSchool").bsModal("show");
                    let editorAr = CKEDITOR.instances['DetailsAr'];
                    let editorEn = CKEDITOR.instances['DetailsEn'];
                    if (editorAr) {
                        CKEDITOR.instances['DetailsAr'].destroy(true);
                    }
                    if (editorEn) {
                        CKEDITOR.instances['DetailsEn'].destroy(true);
                    }
                    $('#DetailsAr').ckeditor();
                    $('#DetailsEn').ckeditor();
                }
            })
        } catch (error) {
            console.log(error);
        }
    },
    //---------------------------------------------------------------------------------------------
    onformKeypress: function (formName, event) {
        if (event.keyCode === 13) {
            aboutSchool.save(formName);
        }
    },
    //---------------------------------------------------------------------------------------------
    save: function save(formId) {
        try {
            let addressAr = $("#AddressAr").val();
            let addressEn = $("#AddressEn").val();
            let mobileNo = $("#MobileNo").val();
            let whatsAppNo = $("#WhatsAppNo").val();
            let email = $("#Email").val();
            let lat = $("#Lat").val();
            let lng = $("#Lng").val();
            let logo = $("#Logo").val();
            let detailsAr = $("#DetailsAr").val();
            let detailsEn = $("#DetailsEn").val();

            if (addressAr == "") {
                $("#AddressAr").addClass("input-validation-error");
                return;
            }
            if (addressEn == "") {
                $("#AddressEn").addClass("input-validation-error");
                return;
            }
            if (mobileNo == "") {
                $("#MobileNo").addClass("input-validation-error");
                return;
            }
            if (whatsAppNo == "") {
                $("#WhatsAppNo").addClass("input-validation-error");
                return;
            }
            if (email == "") {
                $("#Email").addClass("input-validation-error");
                return;
            }
            if (lat == "") {
                $("#Lat").addClass("input-validation-error");
                return;
            }
            if (lng == "") {
                $("#Lng").addClass("input-validation-error");
                return;
            }
            if (detailsAr == "") {
                $("#DetailsAr").addClass("input-validation-error");
                return;
            }
            if (detailsEn == "") {
                $("#DetailsEn").addClass("input-validation-error");
                return;
            }
            if (logo == "") {
                $("#FileName-attachment").addClass("input-validation-error");
                return;
            }

            let form = document.getElementById(formId);
            let data = new FormData(form);

            data.delete("DetailsAr");
            data.delete("DetailsEn");
            data.append("DetailsAr", detailsAr);
            data.append("DetailsEn", detailsEn);

            $.ajax(
                {
                    type: "Post",
                    url: "Save",
                    data: data,
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (result) {
                        if (String(result.status) === EXResultStatus.Fail) {
                            alert(result.message);
                        }
                        else {
                            toastr.success(result.message);
                            aboutSchool.refreshDatatable();
                            $("#AddAboutSchool").bsModal("hide");
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
        let span = document.createElement('span');
        span.innerHTML = s;
        return span.textContent || span.innerText;
    }
    //---------------------------------------------------------------------------------------------
}