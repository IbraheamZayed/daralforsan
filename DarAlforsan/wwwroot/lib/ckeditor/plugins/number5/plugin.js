/*
* Youtube Embed Plugin
*
* @author Jonnas Fonini <jonnasfonini@gmail.com>
* @version 2.1.13
*/
(function () {
	CKEDITOR.plugins.add('number5', {
        lang: ['en', 'ar'],
        icons:"",
        init: function (editor) {
            editor.addCommand('number5', new CKEDITOR.command(editor, {
                exec: function (editor) {
                    editor.insertHtml("&#x0665;");
                }
            })
            );

            editor.ui.addButton('number5', {
                label: "&#x0665;",
                command: 'number5',
                toolbar: 'arNumber,5',
                icon:''
                    
               
            });
           
		}
	});
})();








