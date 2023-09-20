/*
* Youtube Embed Plugin
*
* @author Jonnas Fonini <jonnasfonini@gmail.com>
* @version 2.1.13
*/
(function () {
	CKEDITOR.plugins.add('user', {
        lang: ['en', 'ar'],
        icons:"",
        init: function (editor) {
            editor.addCommand('user', new CKEDITOR.command(editor, {
                exec: function (editor) {
                    editor.insertHtml("{0}");
                }
            })
            );

            editor.ui.addButton('user', {
                //label: editor.lang.user.usercode,
                command: 'user',
                toolbar: 'insert,1000',
                icon: this.path + 'images/icon.png'
                    
            });
         
		}
	});
})();








