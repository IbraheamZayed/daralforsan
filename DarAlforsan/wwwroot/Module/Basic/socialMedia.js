let socialMedia = {
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
                    data: "Url",
                    name: "Url",
                    className: "text-center"
                },
                {
                    data: "ID",
                    render: function (data) {
                        return `<a class="center-block" onclick="socialMedia.initEdit(${data})"><i class="fas fa-pencil-alt text-primary"></i></a>`;
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
                    $("#divSocialMediaBody").html(data);
                    DragDrop.InitDropZone();
                    $("#AddSocialMedia").bsModal("show");
                }
            })
        }
        catch (error) {
            console.log(error);
        }
        finally {
            $("#AddSocialMedia").bsModal("hide");
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
                    $("#divSocialMediaBody").html(data);
                    DragDrop.InitDropZone();
                    $("#AddSocialMedia").bsModal("show");
                }
            })
        } catch (error) {
            console.log(error);
        }
    },
    //---------------------------------------------------------------------------------------------
    onformKeypress: function (formName, event) {
        if (event.keyCode === 13) {
            socialMedia.save(formName);
        }
    },
    //---------------------------------------------------------------------------------------------
    save: function save(formId) {
        try {
            let name = $("#Name").val();
            let url = $("#Url").val();
            let img = $("#Img").val();

            if (name == "") {
                $("#Name").addClass("input-validation-error");
                return;
            }
            if (url == "") {
                $("#Url").addClass("input-validation-error");
                return;
            }
            if (img == "") {
                $("#FileName1-attachment").addClass("input-validation-error");
                return;
            }

            let form = document.getElementById(formId);
            let data = new FormData(form);

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
                            socialMedia.refreshDatatable();
                            $("#AddSocialMedia").bsModal("hide");
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
    }
    //---------------------------------------------------------------------------------------------
}