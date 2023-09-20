/*
* Youtube Embed Plugin
*
* @author Jonnas Fonini <jonnasfonini@gmail.com>
* @version 2.1.13
*/
(function () {
	CKEDITOR.plugins.add('number8', {
        lang: ['en', 'ar'],
        icons:"",
        init: function (editor) {
            editor.addCommand('number8', new CKEDITOR.command(editor, {
                exec: function (editor) {
                    editor.insertHtml("&#x0668;");
                }
            })
            );

            editor.ui.addButton('number8', {
                label: "&#x0668;",
                command: 'number8',
                toolbar: 'arNumber,8',
                icon:''
            });
      
		}
	});
})();








