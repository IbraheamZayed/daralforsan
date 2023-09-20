let schoolVision = {
    initDatatable: function () {
        let columns =
            [
                {
                    data: "ID",
                    name: "ID",
                    className: "d-none"
                },
                {
                    data: "TitleAr",
                    name: "TitleAr",
                    className: "local-font local-column"
                },
                {
                    data: "TitleEn",
                    name: "TitleEn",
                    className: "latin-font latin-column"
                },
                {
                    data: "MessageAr",
                    className: "local-font local-column",
                    render: function (data) {
                        let message = schoolVision.extractContent(data);
                        let str = message.length > 30 ? message.substring(0, 30) : message;
                        return `<span title="${message}">${str}</span>`;
                    }
                },
                {
                    data: "MessageEn",
                    className: "latin-font latin-column",
                    render: function (data) {
                        let message = schoolVision.extractContent(data);
                        let str = message.length > 30 ? message.substring(0, 30) : message;
                        return `<span title="${message}">${str}</span>`;
                    }
                },
                {
                    data: "ID",
                    render: function (data) {
                        return `<a class="center-block" onclick="schoolVision.initEdit(${data})"><i class="fas fa-pencil-alt text-primary"></i></a>`;
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
                    $("#divSchoolVisionBody").html(data);
                    $("#AddSchoolVision").bsModal("show");
                    let editorAr = CKEDITOR.instances['MessageAr'];
                    let editorEn = CKEDITOR.instances['MessageEn'];
                    if (editorAr) {
                        CKEDITOR.instances['MessageAr'].destroy(true);
                    }
                    if (editorEn) {
                        CKEDITOR.instances['MessageEn'].destroy(true);
                    }
                    $('#MessageAr').ckeditor();
                    $('#MessageEn').ckeditor();
                }
            })
        }
        catch (error) {
            console.log(error);
        }
        finally {
            $("#AddSchoolVision").bsModal("hide");
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
                    $("#divSchoolVisionBody").html(data);
                    $("#AddSchoolVision").bsModal("show");
                    let editorAr = CKEDITOR.instances['MessageAr'];
                    let editorEn = CKEDITOR.instances['MessageEn'];
                    if (editorAr) {
                        CKEDITOR.instances['MessageAr'].destroy(true);
                    }
                    if (editorEn) {
                        CKEDITOR.instances['MessageEn'].destroy(true);
                    }
                    $('#MessageAr').ckeditor();
                    $('#MessageEn').ckeditor();
                }
            })
        } catch (error) {
            console.log(error);
        }
    },
    //---------------------------------------------------------------------------------------------
    onformKeypress: function (formName, event) {
        if (event.keyCode === 13) {
            schoolVision.save(formName);
        }
    },
    //---------------------------------------------------------------------------------------------
    save: function save(formId) {
        try {
            let titleAr = $("#TitleAr").val();
            let titleEn = $("#TitleEn").val();
            let messageAr = $("#MessageAr").val();
            let messageEn = $("#MessageEn").val();

            if (titleAr == "") {
                $("#TitleAr").addClass("input-validation-error");
                return;
            }
            if (titleEn == "") {
                $("#TitleEn").addClass("input-validation-error");
                return;
            }
            if (messageAr == "") {
                $("#MessageAr").addClass("input-validation-error");
                return;
            }
            if (messageEn == "") {
                $("#MessageEn").addClass("input-validation-error");
                return;
            }

            let form = document.getElementById(formId);
            let data = new FormData(form);

            data.delete("MessageAr");
            data.delete("MessageEn");
            data.append("MessageAr", messageAr);
            data.append("MessageEn", messageEn);

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
                            schoolVision.refreshDatatable();
                            $("#AddSchoolVision").bsModal("hide");
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