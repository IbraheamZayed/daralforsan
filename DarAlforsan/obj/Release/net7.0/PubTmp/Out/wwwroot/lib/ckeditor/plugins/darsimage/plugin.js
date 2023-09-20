/*
* Youtube Embed Plugin
*
* @author Jonnas Fonini <jonnasfonini@gmail.com>
* @version 2.1.13
*/
(function () {
	CKEDITOR.plugins.add('darsimage', {
        lang: ['en', 'ar'],
        icons:"icon",
        init: function (editor) {
            editor.addCommand('darsimage', new CKEDITOR.command(editor, {
                exec: function (editor) {
                    $("#CKImageContent").load("/Home/CkImage?name=" + editor.name, function (response, status, xhr) {
                        DragDrop.InitDropZone();
                        $('#CKImageModal').bsModal('show');
                    });
                }
            })
            );

            editor.ui.addButton('darsimage', {
                label: editor.lang.darsimage.InsertImage,
                command: 'darsimage',
                toolbar: 'insert,0',
                icon:this.path + 'images/icon.png'
                //icon: CKEDITOR.getUrl("plugins"+ "logo_ckeditor.png")
                //icon:'/lib/ckeditor/plugins/icons.png?t=J8Q8'
            });
            //editor.addCommand('darsimage', new CKEDITOR.dialogCommand('darsimageDialog'));
         //   CKEDITOR.dialog.add('darsimageDialog', this.path + 'dialogs/darsImage.js');
            //CKEDITOR.dialog.add('darsimageDialog', function (instance) {
               
            //    return {
            //        title: "upload image",
            //        minWidth: 600,
            //        minHeight: 300,
            //        onShow: function () {
            //            $("[name='ckimage']").load("CkImage", function (response, status, xhr) {
            //                DragDrop.InitDropZone();
                          
            //            });
            //        },
            //        contents:
            //            [{
            //                id: 'ckimage',
            //                expand: true,
            //                elements:
            //                    []
            //            }
            //            ],
            //        onOk: function () {

            //            var imgPath = "/FileStorage/" + $("#ck-image").val();
            //            var content = "<img  src='" + imgPath + "' />";

            //            var element = CKEDITOR.dom.element.createFromHtml(content);
            //            var instance = this.getParentEditor();
            //            instance.insertElement(element);
            //        }
            //    };
            //});
		}
	});
})();








