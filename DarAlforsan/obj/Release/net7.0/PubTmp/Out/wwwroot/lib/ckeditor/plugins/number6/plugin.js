/*
* Youtube Embed Plugin
*
* @author Jonnas Fonini <jonnasfonini@gmail.com>
* @version 2.1.13
*/
(function () {
	CKEDITOR.plugins.add('number6', {
        lang: ['en', 'ar'],
        icons:"",
        init: function (editor) {
            editor.addCommand('number6', new CKEDITOR.command(editor, {
                exec: function (editor) {
                    editor.insertHtml("&#x0666;");
                }
            })
            );

            editor.ui.addButton('number6', {
                label: "&#x0666;",
                command: 'number6',
                toolbar: 'arNumber,6',
                icon:''
              
            });
         
		}
	});
})();








