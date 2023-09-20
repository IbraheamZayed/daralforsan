/*
* Youtube Embed Plugin
*
* @author Jonnas Fonini <jonnasfonini@gmail.com>
* @version 2.1.13
*/
(function () {
	CKEDITOR.plugins.add('number3', {
        lang: ['en', 'ar'],
        icons:"",
        init: function (editor) {
            editor.addCommand('number3', new CKEDITOR.command(editor, {
                exec: function (editor) {
                    editor.insertHtml("&#x0663;");
                }
            })
            );

            editor.ui.addButton('number3', {
                label: "&#x0663;",
                command: 'number3',
                toolbar: 'arNumber,3',
                icon:''
                    
           
            });
        
		}
	});
})();








