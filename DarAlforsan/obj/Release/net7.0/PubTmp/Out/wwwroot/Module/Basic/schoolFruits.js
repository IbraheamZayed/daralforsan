let schoolFruits = {
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
                    data: "ID",
                    render: function (data) {
                        return `<a class="center-block" onclick="schoolFruits.initEdit(${data})"><i class="fas fa-pencil-alt text-primary"></i></a>`;
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
                    $("#divSchoolFruitsBody").html(data);
                    DragDrop.InitDropZone();
                    $("#AddSchoolFruits").bsModal("show");
                }
            })
        }
        catch (error) {
            console.log(error);
        }
        finally {
            $("#AddSchoolFruits").bsModal("hide");
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
                    $("#divSchoolFruitsBody").html(data);
                    DragDrop.InitDropZone();
                    $("#AddSchoolFruits").bsModal("show");
                }
            })
        } catch (error) {
            console.log(error);
        }
    },
    //---------------------------------------------------------------------------------------------
    onformKeypress: function (formName, event) {
        if (event.keyCode === 13) {
            schoolFruits.save(formName);
        }
    },
    //---------------------------------------------------------------------------------------------
    save: function save(formId) {
        try {
            let titleAr = $("#TitleAr").val();
            let titleEn = $("#TitleEn").val();
            let img = $("#Img").val();

            if (titleAr == "") {
                $("#TitleAr").addClass("input-validation-error");
                return;
            }
            if (titleEn == "") {
                $("#TitleEn").addClass("input-validation-error");
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
                            schoolFruits.refreshDatatable();
                            $("#AddSchoolFruits").bsModal("hide");
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