/*
* Youtube Embed Plugin
*
* @author Jonnas Fonini <jonnasfonini@gmail.com>
* @version 2.1.13
*/
(function () {
    CKEDITOR.plugins.add('number2', {
        lang: ['en', 'ar'],
        icons:"",
        init: function (editor) {
            editor.addCommand('number2', new CKEDITOR.command(editor, {
                exec: function (editor) {
                    editor.insertHtml("&#x0662;");
                }
            })
            );

            editor.ui.addButton('number2', {
                label: "&#x0662;",
                command: 'number2',
                toolbar: 'arNumber,2',
                icon:''
                    
               
            });

		}
	});
})();








