/*
* Youtube Embed Plugin
*
* @author Jonnas Fonini <jonnasfonini@gmail.com>
* @version 2.1.13
*/
(function () {
	CKEDITOR.plugins.add('number0', {
        lang: ['en', 'ar'],
        icons:"",
        init: function (editor) {
            editor.addCommand('number0', new CKEDITOR.command(editor, {
                exec: function (editor) {
                    editor.insertHtml("&#x06F0;");
                }
            })
            );

            editor.ui.addButton('number0', {
                label: "&#x06F0;",
                command: 'number0',
                toolbar: 'arNumber,0',
                icon:''
                    
            });
         
		}
	});
})();








