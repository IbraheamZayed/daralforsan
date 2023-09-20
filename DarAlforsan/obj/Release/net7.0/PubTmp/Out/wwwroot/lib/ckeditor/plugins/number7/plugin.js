/*
* Youtube Embed Plugin
*
* @author Jonnas Fonini <jonnasfonini@gmail.com>
* @version 2.1.13
*/
(function () {
	CKEDITOR.plugins.add('number7', {
        lang: ['en', 'ar'],
        icons:"",
        init: function (editor) {
            editor.addCommand('number7', new CKEDITOR.command(editor, {
                exec: function (editor) {
                    editor.insertHtml("&#x0667;");
                }
            })
            );

            editor.ui.addButton('number7', {
                label: "&#x0667;",
                command: 'number7',
                toolbar: 'arNumber,7',
                icon:''
             
            });
    
		}
	});
})();








