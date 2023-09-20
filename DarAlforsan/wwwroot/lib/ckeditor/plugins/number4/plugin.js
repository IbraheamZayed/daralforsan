/*
* Youtube Embed Plugin
*
* @author Jonnas Fonini <jonnasfonini@gmail.com>
* @version 2.1.13
*/
(function () {
	CKEDITOR.plugins.add('number4', {
        lang: ['en', 'ar'],
        icons:"",
        init: function (editor) {
            editor.addCommand('number4', new CKEDITOR.command(editor, {
                exec: function (editor) {
                    editor.insertHtml("&#x0664;");
                }
            })
            );

            editor.ui.addButton('number4', {
                label: "&#x0664;",
                command: 'number4',
                toolbar: 'arNumber,4',
                icon:''
                
            });
        
		}
	});
})();








