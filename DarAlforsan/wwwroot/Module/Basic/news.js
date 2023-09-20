let news = {
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
                    data: "DetailsAr",
                    className: "local-font local-column",
                    render: function (data) {
                        let details = news.extractContent(data);
                        let str = details.length > 30 ? details.substring(0, 30) : details;
                        return `<span title="${details}">${str}</span>`;
                    }
                },
                {
                    data: "DetailsEn",
                    className: "latin-font latin-column",
                    render: function (data) {
                        let details = news.extractContent(data);
                        let str = details.length > 30 ? details.substring(0, 30) : details;
                        return `<span title="${details}">${str}</span>`;
                    }
                },
                {
                    data: "AddDate",
                    name: "AddDate",
                    className: "text-center"
                },
                {
                    data: "ViewsCount",
                    name: "Views Count",
                    className: "text-center"
                },
                {
                    data: "ID",
                    render: function (data) {
                        return `<a class="center-block" onclick="news.initEdit(${data})"><i class="fas fa-pencil-alt text-primary"></i></a>`;
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
                    $("#divNewsBody").html(data);
                    DragDrop.InitDropZone();
                    $("#AddNews").bsModal("show");
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
            $("#AddNews").bsModal("hide");
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
                    $("#divNewsBody").html(data);
                    DragDrop.InitDropZone();
                    $("#AddNews").bsModal("show");
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
            news.save(formName);
        }
    },
    //---------------------------------------------------------------------------------------------
    save: function save(formId) {
        try {
            let titleAr = $("#TitleAr").val();
            let titleEn = $("#TitleEn").val();
            let detailsAr = $("#DetailsAr").val();
            let detailsEn = $("#DetailsEn").val();
            let mainImg = $("#MainImg").val();
            let imgs = $("#Imgs").val();

            if (titleAr == "") {
                $("#TitleAr").addClass("input-validation-error");
                return;
            }
            if (titleEn == "") {
                $("#TitleEn").addClass("input-validation-error");
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
            if (mainImg == "") {
                $("#FileName1-attachment").addClass("input-validation-error");
                return;
            }
            if (imgs == "") {
                $("#FileName2-attachment").addClass("input-validation-error");
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
                            news.refreshDatatable();
                            $("#AddNews").bsModal("hide");
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