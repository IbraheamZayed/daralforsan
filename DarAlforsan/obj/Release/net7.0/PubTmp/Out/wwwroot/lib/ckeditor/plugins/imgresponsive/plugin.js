/*
* Youtube Embed Plugin
*
* @author Jonnas Fonini <jonnasfonini@gmail.com>
* @version 2.1.13
*/
(function () {
    CKEDITOR.plugins.add('imgresponsive', {
        lang: ['en', 'ar'],
        icons: "icon",
        init: function (editor) {
            editor.addCommand('imgresponsive', new CKEDITOR.command(editor, {
                exec: function (editor) {
                    $("#CKImageContent").load("/Home/CkImage?name=" + editor.name + "&IsResponsiveImg=" + true, function (response, status, xhr) {
                        DragDrop.InitDropZone();
                        $('#CKImageModal').bsModal('show');
                    });
                }
            })
            );
            editor.ui.addButton('imgresponsive', {
                label: editor.lang.imgresponsive.InsertImage,
                command: 'imgresponsive',
                toolbar: 'insert,0',
                icon: this.path + 'images/icon3.png'
               
            });
          
		}
	});
})();








