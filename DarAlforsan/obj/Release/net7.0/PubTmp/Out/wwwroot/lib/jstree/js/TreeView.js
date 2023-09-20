class TreeView {
    constructor() {
        // property contains all the events that happens on the tree
        this.events = {};
        // data array to display 
        this.data = [];
        this.isMultiySelect = false;
        this.isDots = false;
        this.isCheckBox = true;
        this.selectedNodes = [];
        // to pass the selected id in as hidden filed in from collection 
        this.targetControle = '';
        // target text area to display message 
        this.targetTextAreaControl = '';
        // it should be a uniqe value for every tree view in the same page 
        this.modalID = '';
        // when you set a value to this property , you should see the previous seleced Node 
        this.selectedNode = 0;
        this.treeType = treeViewType.popup;
        this.openControlName = '';
    }
    // initializing the tree-view 
    initialize() {
        var nodes = [];
        var nodesText = [];
        var selected_node = this.data.find(node => node.id == String(this.selectedNode));
        var treeType = this.treeType;
        var emit_falteMode = this.onFlateEmit;
        var opnControlName = this.openControlName;
        if (this.treeType == treeViewType.fromControl) {
            var modalID = this.modalID;
            $("#" + this.openControlName).dblclick(function () {
                $("#" + modalID).bsModal('show');
            });
        }
        if (selected_node)
            $("#jsTree" + this.modalID).closest(".control").find('input[type="text"]').val(selected_node.text);
        $("#jsTree" + this.modalID).on("changed.jstree", function (e, data) {
            var i, j;
            nodes = [];
            nodesText = [];
            $(this).find('.checker').removeClass('checkbox-checked')
            for (i = 0, j = data.selected.length; i < j; i++) {
                var oldId = data.selected[i];
                var parent;
                if (data.node)
                    parent = data.node.parent[0];
                var id = data.selected[i];
                if (oldId.indexOf('-') != -1) {
                    id = oldId.split('-')[1];
                    parent = oldId.split('-')[0];
                }
                $("#" + data.selected[i] + "_anchor").find('.checker').addClass('checkbox-checked');
                $.each(data.instance.get_node(data.selected[i]).children, function (i, v) {

                    $("#" + v + "_anchor").find('.checker').addClass('checkbox-checked');
                });
                nodes.push({
                    text: data.instance.get_node(data.selected[i]).text,
                    id: id,
                    parent: parent,
                    value1: data.instance.get_node(data.selected[i]).original.value1,
                    value2: data.instance.get_node(data.selected[i]).original.value2,
                    value3: data.instance.get_node(data.selected[i]).original.value3,
                    isleaf: data.instance.get_node(data.selected[i]).original.isleaf
                });
                $(this).closest(".control").find('input[type="text"]').val(data.instance.get_node(data.selected[i]).text)
            }
            $.each(nodes, function (i, v) {
                if (v.isleaf)
                    nodesText.push(v.text);
            });
            if (nodesText.length > 0)
                $(this).closest(".control").find('input[type="text"]').val(nodesText.join(','))
            $(this).attr('data-selected-values', JSON.stringify(nodes));
            // if the tree of type flate we have to directly emit the selected data
            if (treeType == treeViewType.flate) {
                $(this).closest('.control').find('button').click();
            }
        }).jstree({
            "core": {
                "multiple": this.isMultiySelect,
                "themes": {
                    "variant": "larg",
                    "rtl": true,
                    "theme": "default-rtl",
                    //"icon ": false,
                    "dots": this.isDots,
                    "responsive": true,
                    //"stripes": true
                },
                "data": this.data,
                "animation": 200,
                "initially_open": [selected_node],
                "initially_select": [selected_node],
            },
            // types to assign icon to spsific type like use user font icon when the type is contact member for example
            "types": {
                "root": {
                    "icon": "fas fa-folder fa-fw text-folder"
                }
                ,
                "open": {
                    "icon": "fad fa-folder-open fa-fw text-folder"
                },
                "child": {
                    "icon": "/lib/jstree/image/folder.png"
                },
                "template": {
                    "icon": "fad fa-comment-alt-lines"
                },
                "contactMember": {
                    "icon": "fad fa-user fa-fw text-secondary"
                },
                "default": {
                    "icon": "fas fa-folder fa-fw text-folder"
                }
            },
            "checkbox": {
                'deselect_all': true,
                'three_state': true,
            },
            "plugins": [
                this.isCheckBox ? "checkbox" : "",
                "dnd",
                
                "types",
                "unique",
                "changed",
                "conditionalselect",
                "ui"
            ]
        })
            .bind('loaded.jstree', function (e, data) {
                $(this).jstree("select_node", selected_node);
              
            });
        $("#jsTree" + this.modalID).bind('ready.jstree', function (event, data) {
            var $tree = $(this);
            $tree.find('.jstree-anchor').find(".jstree-checkbox").each(function (i, v) {
                $(v).removeClass("jstree-icon").removeClass("jstree-checkbox")
                    .addClass("fas fa-check-square checker checkbox-unchecked");
                $(v).click(function () {
                    $(v).toggleClass('checkbox-checked')
                })
            });
        });
        $("#jsTree" + this.modalID).on('open_node.jstree', function (event, data) {
            var $tree = $(this);
            $tree.find('.jstree-anchor').find(".jstree-checkbox").each(function (i, v) {
                $(v).removeClass("jstree-icon").removeClass("jstree-checkbox")
                    .addClass("fas fa-check-square checker checkbox-unchecked ");
                var openedNodes = $(v).closest('ul').closest('li').find('a.jstree-anchor').find('i.checker');
                $.each(openedNodes, function (n_index, n_value) {
                    if ($(openedNodes[0]).hasClass('checkbox-checked'))
                        $(n_value).addClass('checkbox-checked')
                    else
                        $(n_value).removeClass('checkbox-checked')
                })
                $(v).click(function () {
                    $(v).toggleClass('checkbox-checked')
                })

            })

            data.instance.set_type(data.node, 'open');
        });
        $("#jsTree" + this.modalID).on('close_node.jstree', function (event, data) {
            data.instance.set_type(data.node, 'default');
        });
    }
    // flate view emit function 
    onFlateEmit() {
        this.selectedNodes = JSON.parse($("#jsTree" + this.modalID).attr('data-selected-values'));
        this.emit('selectedNode_changed', this.selectedNodes);
    }
    // thats call in the modal popup to set  the selected nodes in the modal div attribute data-selected-values
    onSelectNode() {
        var selectedNodes = [];
        try {
            selectedNodes = JSON.parse($("#jsTree" + this.modalID).attr('data-selected-values'));
        } catch (e) {
        }
        if (selectedNodes.length <= 0)
            return
        this.selectedNodes = selectedNodes;
        this.emit('selectedNode_changed', this.selectedNodes);
        if (this.targetTextAreaControl != '' && this.selectedNodes[0].templateText) {
            $("#" + this.targetTextAreaControl).val(this.selectedNodes[0].templateText);
        }
        if (this.selectedNodes.length > 0)
            $('#selectedNode' + this.modalID).val(this.selectedNodes[this.selectedNodes.length - 1].text);
        $("#" + this.targetControle).val(this.selectedNodes[this.selectedNodes.length - 1].id);
        this.closeTreeViewModal();
    }
    // to show modal 
    treeViewShowModal() {
        $("#" + this.modalID).bsModal('show');
        return;
    }
    // to hide modal
    closeTreeViewModal() {
        $("#" + this.modalID).bsModal('hide');
    }
    // js tree clear selection 
    clearSelection() {
        $("#jsTree" + this.modalID).jstree("deselect_all");
        $("#jsTree" + this.modalID).closest(".control").find('input[type="text"]').val('')
    }
    // to subscribe to the selected nodes in somewhere
    subscribe(eventName, fn) {
        if (!this.events[eventName]) {
            this.events[eventName] = [];
        }
        this.events[eventName].push(fn);
    }
    // emit the selected to data to use it in somewhere 
    emit(eventName, data) {
        const event = this.events[eventName];
        if (event) {
            event.forEach(fn => {
                fn.call(null, data);
            });
        }
    }
}
var treeViewType = {
    popup: 1,
    flate: 2,
    fromControl: 3
};
