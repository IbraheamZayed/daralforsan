/**
 * @license Copyright (c) 2003-2019, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here.
	// For complete reference see:
	// https://ckeditor.com/docs/ckeditor4/latest/api/CKEDITOR_config.html

	// The toolbar groups arrangement, optimized for two toolbar rows.
	config.toolbarGroups = [
		{ name: 'clipboard',   groups: [ 'clipboard', 'undo' ] },
		{ name: 'editing',     groups: [ 'find', 'selection', 'spellchecker' ] },
		{ name: 'links' },
		{ name: 'insert' },
		{ name: 'forms' },
		{ name: 'tools' },
		{ name: 'document',	   groups: [ 'mode', 'document', 'doctools' ] },
        { name: 'others' },
        { name: "arNumber" },
		'/',
        { name: 'basicstyles', groups: [ 'basicstyles', 'cleanup' ] },
		{ name: 'paragraph',   groups: [ 'list', 'indent', 'blocks', 'align', 'bidi' ] },
		{ name: 'styles' },
		{ name: 'colors' },
        { name: 'about' },
    ];
    
    config.height = '300px';
	// Remove some buttons provided by the standard plugins, which are
	// not needed in the Standard(s) toolbar.
    config.removeButtons = 'Image,Subscript,Superscript';

	// Set the most common block elements.
    config.format_tags = 'p;h1;h2;h3;pre';
    config.allowedContent = true;

	// Simplify the dialog windows.
    config.removeDialogTabs = 'image:advanced;image:Link;link:advanced;link:target;link:upload;';
    config.defaultLanguage = 'ar';
    config.language = 'ar';
    // youtube plugin 
    config.extraPlugins = 'user,justify,colorbutton,colordialog,font,darsimage,imgresponsive,audio,youtube,ckeditor_wiris,number0,number1,number2,number3,number4,number5,number6,number7,number8,number9,uploadimage';
    config.youtube_width = '640';
    config.youtube_height = '480';
    config.youtube_responsive = true;
    config.youtube_related = true;
    config.youtube_older = false;
    config.youtube_autoplay = false;
    config.youtube_controls = true;
    config.contentsCss = '/lib/ckeditor/font.css';
    /*config.pasteFilter = 'p; a[!href]';*/
    config.uploadUrl = '/Home/UploadImage';

    //config.extraAllowedContent = 'img[class]';
        
};
CKEDITOR.on('dialogDefinition', function (ev) {
    var dialogName = ev.data.name;
    var dialogDefinition = ev.data.definition;
    ev.data.definition.resizable = CKEDITOR.DIALOG_RESIZE_NONE;

    if (dialogName == 'link') {
        var infoTab = dialogDefinition.getContents('info');
        infoTab.remove('protocol');
    }
    //if (dialogName == 'image') {
    //    var infoTab = dialogDefinition.getContents('info');
    //    infoTab.remove('txtUrl');
    //    //infoTab.remove('htmlPreview');
    //}
});
//CKEDITOR.on('instanceReady', function (ev) {
//    debugger;
//    ev.editor.dataProcessor.htmlFilter.addRules({
//        elements: {
//            img: function (el) {
//                debugger;
//                alert("sss");
//                // Add bootstrap "img-responsive" class to each inserted image
//                el.addClass('img-responsive');

               
//            }
//        }
//    });
//});
//CKEDITOR.plugins.addExternal('ckeditor_wiris', '/plugins/ckeditor_wiris/plugin.js');
CKEDITOR.plugins.addExternal('ckeditor_wiris', '@wiris/mathtype-ckeditor4/', 'plugin.js');
