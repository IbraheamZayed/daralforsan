let saidAboutUs = {
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
                    data: "MessageAr",
                    className: "local-font local-column",
                    render: function (data) {
                        let message = saidAboutUs.extractContent(data);
                        let str = message.length > 30 ? message.substring(0, 30) : message;
                        return `<span title="${message}">${str}</span>`;
                    }
                },
                {
                    data: "MessageEn",
                    className: "latin-font latin-column",
                    render: function (data) {
                        let message = saidAboutUs.extractContent(data);
                        let str = message.length > 30 ? message.substring(0, 30) : message;
                        return `<span title="${message}">${str}</span>`;
                    }
                },
                {
                    data: "ID",
                    render: function (data) {
                        return `<a class="center-block" onclick="saidAboutUs.initEdit(${data})"><i class="fas fa-pencil-alt text-primary"></i></a>`;
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
                    $("#divSaidAboutUsBody").html(data);
                    DragDrop.InitDropZone();
                    $("#AddSaidAboutUs").bsModal("show");
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
            $("#AddSaidAboutUs").bsModal("hide");
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
                    $("#divSaidAboutUsBody").html(data);
                    DragDrop.InitDropZone();
                    $("#AddSaidAboutUs").bsModal("show");
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
            saidAboutUs.save(formName);
        }
    },
    //---------------------------------------------------------------------------------------------
    save: function save(formId) {
        try {
            let name = $("#Name").val();
            let messageAr = $("#MessageAr").val();
            let messageEn = $("#MessageEn").val();
            let img = $("#Img").val();

            if (name == "") {
                $("#Name").addClass("input-validation-error");
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
            if (img == "") {
                $("#FileName-attachment").addClass("input-validation-error");
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
                            saidAboutUs.refreshDatatable();
                            $("#AddSaidAboutUs").bsModal("hide");
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