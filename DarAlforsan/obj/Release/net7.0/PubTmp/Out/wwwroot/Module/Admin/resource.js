let resource = {
    initDatatable: function () {
        let columns =
            [
                {
                    data: "ResourceID",
                    name: "ResourceID",
                    className: "d-none"
                },
                {
                    data: "Name",
                    name: "Name",
                    className: "local-font text-center"
                },
                {
                    data: "LocalValue",
                    name: "LocalValue",
                    className: "local-font text-center"
                },
                {
                    data: "LatinValue",
                    name: "LatinValue",
                    className: "local-font text-center"
                },
                {
                    data: "Module",
                    name: "Module",
                    className: "local-font text-center"
                },
                {
                    data: "ResourceID",
                    render: function (data) {
                        return `<a class="center-block" onclick="resource.initEdit(${data})"><i class="fas fa-pencil-alt text-primary"></i></a>`;
                    },
                    className: "text-center grid-cmd",
                    orderable: false
                },
                {
                    data: "ResourceID",
                    render: function (data) {
                        return `<a class="center-block"  onclick="deleteRecord(${data})"><i class="fa fa-trash text-danger"></i></a>`;
                    },
                    className: "text-center grid-cmd",
                    orderable: false
                }
            ];
        initDataTable("list", "Resources/GetList", "", "Resources/Remove", "", columns, 0, DescOrder);
    },
    //---------------------------------------------------------------------------------------------
    initAdd: function initAdd() {
        try {
            $.ajax({
                url: "Resources/New",
                contentType: "application/html ; charset:utf-8",
                type: "Get",
                dataType: "html",
                success: function (data) {
                    $("#divResourceBody").html(data);
                    $("#AddResource").bsModal("show");
                    $("form-Resource").each(function () { $.data($(this)[0], "validator", false); });
                    $.validator.unobtrusive.parse("form");
                }
            })
        }
        catch (error) {
            console.log(error);
        }
        finally {
            $("#AddResource").bsModal("hide");
        }
    },
    //---------------------------------------------------------------------------------------------
    initEdit: function initEdit(id) {
        try {
            $.ajax({
                url: "Resources/Edit",
                data: { ID: id },
                contentType: 'application/html ; charset:utf-8',
                type: "Get",
                dataType: "html",
                success: function (data) {
                    $("#divResourceBody").html(data);
                    $("#AddResource").bsModal("show");
                    $.validator.unobtrusive.parse($("#form-Resource"));
                }
            })
        } catch (error) {
            console.log(error);
        }
    },
    //---------------------------------------------------------------------------------------------
    onformKeypress: function (formName, event) {
        if (event.keyCode === 13) {
            resource.save(formName);
        }
    },
    //---------------------------------------------------------------------------------------------
    save: function save(formName) {
        try {
            let resourceID = $("#ResourceID").val();
            let name = $("#Name").val();
            let localValue = $("#LocalValue").val();
            let latinValue = $("#LatinValue").val();
            let moduleID = $("#ModuleID").val();
        
            if (name == "") {
                $("#Name").addClass("input-validation-error");
                return;
            }
            if (localValue == "") {
                $("#LocalValue").addClass("input-validation-error");
                return;
            }
            if (latinValue == "") {
                $("#LatinValue").addClass("input-validation-error");
                return;
            }
            if (moduleID == "") {
                $("#ModuleID").addClass("input-validation-error");
                return;
            }

            let obj = {
                ResourceID: resourceID,
                Name: name,
                LocalValue: localValue,
                LatinValue: latinValue,
                ModuleID: moduleID
            };

            $.ajax(
                {
                    type: "Post",
                    url: "Resources/Save",
                    data: obj,
                    success: function (result) {
                        if (String(result.status) === EXResultStatus.Fail) {
                            alert(result.message);
                        }
                        else {
                            toastr.success(result.message);
                            resource.refreshDatatable();
                            $("#AddResource").bsModal("hide");
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
}