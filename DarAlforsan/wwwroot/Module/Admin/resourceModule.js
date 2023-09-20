let resourceModule = {
    initDatatable: function () {
        let columns =
            [
                {
                    data: "ModuleID",
                    name: "ModuleID",
                    className: "d-none"
                },
                {
                    data: "Name",
                    name: "Name",
                    className: "text-center"
                },
                {
                    data: "LocalName",
                    name: "LocalName",
                    className: "local-font text-center"
                },
                {
                    data: "LatinName",
                    name: "LatinName",
                    className: "local-font text-center"
                },
                {
                    data: "ModuleID",
                    render: function (data) {
                        return `<a class="center-block" onclick="resourceModule.initEdit(${data})"><i class="fas fa-pencil-alt text-primary"></i></a>`;
                    },
                    className: "text-center grid-cmd",
                    orderable: false
                },
                {
                    data: "ModuleID",
                    render: function (data) {
                        return `<a class="center-block"  onclick="deleteRecord(${data})"><i class="fa fa-trash text-danger"></i></a>`;
                    },
                    className: "text-center grid-cmd",
                    orderable: false
                }
            ];
        initDataTable("list", "ResourceModule/GetList", "", "ResourceModule/Remove", "", columns, 0, DescOrder);
    },
    //---------------------------------------------------------------------------------------------
    initAdd: function initAdd() {
        try {
            $.ajax({
                url: "ResourceModule/New",
                contentType: "application/html ; charset:utf-8",
                type: "Get",
                dataType: "html",
                success: function (data) {
                    console.log(data);
                    $("#divResourceModuleBody").html(data);
                    $("#AddResourceModule").bsModal("show");
                    $("form-ResourceModule").each(function () { $.data($(this)[0], "validator", false); });
                    $.validator.unobtrusive.parse("form");
                }
            })
        }
        catch (error) {
            console.log(error);
        }
        finally {
            $("#AddResourceModule").bsModal("hide");
        }
    },
    //---------------------------------------------------------------------------------------------
    initEdit: function initEdit(id) {
        try {
            $.ajax({
                url: "ResourceModule/Edit",
                data: { ID: id },
                contentType: 'application/html ; charset:utf-8',
                type: "Get",
                dataType: "html",
                success: function (data) {
                    $("#divResourceModuleBody").html(data);
                    $("#AddResourceModule").bsModal("show");
                    $.validator.unobtrusive.parse($("#form-ResourceModule"));
                }
            })
        } catch (error) {
            console.log(error);
        }
    },
    //---------------------------------------------------------------------------------------------
    onformKeypress: function (formName, event) {
        if (event.keyCode === 13) {
            resourceModule.save(formName);
        }
    },
    //---------------------------------------------------------------------------------------------
    save: function save() {
        try {
            let moduleID = $("#ModuleID").val();
            let name = $("#Name").val();
            let localName = $("#LocalName").val();
            let LatinName = $("#LatinName").val();
        
            if (name == "") {
                $("#Name").addClass("input-validation-error");
                return;
            }
            if (localName == "") {
                $("#LocalName").addClass("input-validation-error");
                return;
            }
            if (LatinName == "") {
                $("#LatinName").addClass("input-validation-error");
                return;
            }

            let obj = {
                ModuleID: moduleID,
                Name: name,
                LocalName: localName,
                LatinName: LatinName
            };

            $.ajax(
                {
                    type: "Post",
                    url: "ResourceModule/Save",
                    data: obj,
                    success: function (result) {
                        if (String(result.status) === EXResultStatus.Fail) {
                            alert(result.message);
                        }
                        else {
                            toastr.success(result.message);
                            resourceModule.refreshDatatable();
                            $("#AddResourceModule").bsModal("hide");
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