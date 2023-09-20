/*
* Youtube Embed Plugin
*
* @author Jonnas Fonini <jonnasfonini@gmail.com>
* @version 2.1.13
*/
(function () {
	CKEDITOR.plugins.add('number1', {
        lang: ['en', 'ar'],
        icons:"",
        init: function (editor) {
            editor.addCommand('number1', new CKEDITOR.command(editor, {
                exec: function (editor) {
                    editor.insertHtml("&#x06F1;");
                }
            })
            );

            editor.ui.addButton('number1', {
                label: "&#x06F1;" ,
                command: 'number1',
                toolbar: 'arNumber,1',
                icon: '',
                
            });
            
       
		}
	});
})();








