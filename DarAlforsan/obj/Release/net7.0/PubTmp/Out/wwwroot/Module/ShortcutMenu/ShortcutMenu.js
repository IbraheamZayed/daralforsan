var shortcutMenu = {
    fromOutMenuContext: false,
    onShowAddMenu: function () {
        $.ajax({
            url: '/Common/ShortcutMenu/New',
            method: 'GET',
            success: function (res) {
                $("#ShortcutMenuBody").html(res);
                $("#ShortcutMenuModal").bsModal('show');
                if (!$(".shortcut-context-menu").hasClass('d-none')) {
                    $(".shortcut-context-menu").addClass('menu-slide-down');
                    setTimeout(() => {
                        $(".shortcut-context-menu").removeClass('menu-slide-up').addClass('d-none');
                    }, 150);

                }
                initFormValidation("addShortcutMenuFrom");
            }
        });
    },
    onSaveMenu: function (event) {
        event.preventDefault();
        var data = shortcutMenu.getSerializedFormData($("#addShortcutMenuFrom"));
        data.URL = window.location.pathname + window.location.search;
        if ($("#addShortcutMenuFrom").valid()) {
            $.ajax({
                url: '/Common/ShortcutMenu/Save',
                method: 'POST',
                data: data,
                success: function (res) {
                    shortcutMenu.onCloseAddMenuModal();
                    if (Number(data["ItemID"]) > 0)
                        refreshDataTable();
                    $("#shotcutMenu").html(res);
                    if (res.status !== undefined && res.status == EXResultStatus.Fail) {
                        toastr.error(res.message);
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }
    },
    onCloseAddMenuModal: function () {
        $("#ShortcutMenuModal").bsModal('hide');
    },
    onShowShortcutMenu: function () {
        if (!$(".shortcut-context-menu").hasClass('d-none')) {
            $(".shortcut-context-menu").addClass('menu-slide-down');
        }
        setTimeout(() => {
            $(".shortcut-context-menu").removeClass('menu-slide-down').toggleClass('d-none menu-slide-up');
        }, 150);
        shortcutMenu.fromOutMenuContext = false;

    },
    getSerializedFormData: function getSerializedFormData(form) {
        var list = form.serializeArray();
        var obj = {};
        $.each(list, function (i, v) {
            if (v["name"] == "__RequestVerificationToken")
                return;
            obj[v['name']] = v["value"];
        });
        return obj;
    },
    reOrder: function (element, itemID) {
        $.ajax({
            url: '/Common/shortcutmenu/UpdateOrder',
            data: { ItemID: itemID, OrderNo: $(element).val() },
            method: 'GET',
            success: function (res) {
                console.log(res);
                if (res.status = EXResultStatus.Success) {
                    toastr.success(res.message);
                } else {
                    toastr.error(res.message);

                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    onShowEdit: function (itemId) {
        $.ajax({
            url: '/Common/shortcutmenu/InitEdit',
            data: { ItemID: itemId },
            method: 'GET',
            success: function (res) {
                $("#ShortcutMenuBody").html(res);
                $("#ShortcutMenuModal").bsModal('show');
                initFormValidation("addShortcutMenuFrom");
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
};
$(window).click(function () {
    //Hide the menus if visible
    if (shortcutMenu.fromOutMenuContext) {
        shortcutMenu.fromOutMenuContext = false;
        $(".shortcut-context-menu").addClass('menu-slide-down');
        setTimeout(() => {
            $(".shortcut-context-menu").removeClass('menu-slide-up').addClass('d-none');
        }, 150);
    }
    shortcutMenu.fromOutMenuContext = true;
});
$('.shortcut-context-menu').click(function (event) {
    event.stopPropagation();
});