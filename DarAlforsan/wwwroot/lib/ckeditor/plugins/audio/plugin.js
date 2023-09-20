/*
* Youtube Embed Plugin
*
* @author Jonnas Fonini <jonnasfonini@gmail.com>
* @version 2.1.13
*/
(function () {
    CKEDITOR.plugins.add('audio', {
        lang: ['en', 'ar'],
        icons:"icon",
        init: function (editor) {
            editor.addCommand('audio', new CKEDITOR.command(editor, {
                exec: function (editor) {
                    $("#CKAudioContent").load("/Home/CkAudio?name=" + editor.name, function (response, status, xhr) {
                        DragDrop.InitDropZone();
                        $('#CKAudioModal').bsModal('show');
                    });
                }
            })
            );

            editor.ui.addButton('audio', {
                label: editor.lang.audio.InsertAudio,
                command: 'audio',
                toolbar: 'insert,1',
                icon:this.path + 'images/icon.png'
               
            });
          
		}
	});
})();








