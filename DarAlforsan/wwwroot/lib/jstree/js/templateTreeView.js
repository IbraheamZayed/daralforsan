class TreeView {
    constructor() {
        // property contains all the events that happens on the tree
        this.events = {};
        // data array to display 
        this.data = [];
        this.isMultiySelect = false;
        this.isCheckBox = true;
        this.selectedNodes = [];
        // to pass the selected id in as hidden filed in from collection 
        this.targetControle = '';
        // it should be a uniqe value for every tree view in the same page 
        this.treeViewID = '';
        this.modalID = '';
        // when you set a value to this property , you should see the previous seleced Node 
        this.selectedNode = 0;
    }
    // initializing the tree-view 
    initialize() {
        var nodes = [];
        $("#jsTree" + this.treeViewID).on("changed.jstree", function (e, data) {
            var i, j;
            nodes = [];
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
                nodes.push({
                    text: data.instance.get_node(data.selected[i]).text,
                    id: id,
                    parent: parent,
                    templateText: data.instance.get_node(data.selected[i]).original.templateText
                });
            }
            $(this).attr('data-selected-values', JSON.stringify(nodes));
        })
            .jstree({
                "core": {
                    "multiple": this.isMultiySelect,
                    "themes": {
                        "variant": "larg",
                        "rtl": true,
                        "theme": "default-rtl",
                        //"icon ": false,
                        "dots": false,
                        "responsive": true,
                        //"stripes": true
                    },
                    "data": this.data,
                    "animation": 500,
                },
                "types": {
                    "root": {
                        "icon":"/lib/jstree/image/folder.png"
                    },
                    "child": {
                        "icon":"/lib/jstree/image/folder.png"
                    },
                    "template": {
                        "icon":"/lib/jstree/image/tenplate.jpeg"
                    },
                    "default": {
                        "icon":"/lib/jstree/image/folder.png"
                    }
                },       
                "checkbox": {
                    'deselect_all': true,
                    // 'three_state' : false,
                    //"keep_selected_style": false
                },
                "plugins": [
                    this.isCheckBox ? "checkbox" : "",
                    "dnd",
                    "massload",
                    "search",
                    "sort",
                    "types",
                    "unique",
                    "wholerow",
                    "changed",
                    "conditionalselect",
                    "ui"
                ]
            })
    }
    // thats call in the modal popup to set  the selected nodes in the modal div attribute data-selected-values
    onSelectNode() {
        this.selectedNodes = JSON.parse($("#jsTree" + this.treeViewID).attr('data-selected-values'));
        this.emit('selectedNode_changed', this.selectedNodes);
        $("#" + this.targetControle).val(this.selectedNodes[this.selectedNodes.length - 1].templateText);
        this.closeTreeViewModal();
    }
    // to hide modal
    closeTreeViewModal() {
        if ($("#" + this.modalID))
        $("#" + this.modalID).bsModal('hide');
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