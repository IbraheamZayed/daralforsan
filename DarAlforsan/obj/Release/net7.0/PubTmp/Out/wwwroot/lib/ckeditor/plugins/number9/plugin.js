/*
* Youtube Embed Plugin
*
* @author Jonnas Fonini <jonnasfonini@gmail.com>
* @version 2.1.13
*/
(function () {
	CKEDITOR.plugins.add('number9', {
        lang: ['en', 'ar'],
        icons:"",
        init: function (editor) {
            editor.addCommand('number9', new CKEDITOR.command(editor, {
                exec: function (editor) {
                    editor.insertHtml("&#x0669;");
                }
            })
            );

            editor.ui.addButton('number9', {
                label: "&#x0669"/*editor.lang.number9.number*/,
                command: 'number9',
                toolbar: 'arNumber,9',
                icon:""

            });
            //this.path + 'images/icon.png'
		}
	});
})();








