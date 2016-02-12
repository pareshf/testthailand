/*
Copyright (c) 2003-2011, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

//CKEDITOR.editorConfig = function (config) {

    // Add WIRIS to the plugin list
  //  config.extraPlugins += (config.extraPlugins.length == 0 ? '' : ',') + 'ckeditor_wiris';

    // Add WIRIS buttons to the "Full toolbar"
    // Optionally, you can remove the following line and follow
    // http://docs.cksource.com/CKEditor_3.x/Developers_Guide/Toolbar
    // config.toolbar_Full.push(['ckeditor_wiris_formulaEditor', 'ckeditor_wiris_CAS']);
// };

    CKEDITOR.editorConfig = function (config) {
        config.toolbar = 'MyToolbar';

        config.toolbar_MyToolbar =
	[
		{ name: 'document', items: ['NewPage', 'Preview'] },
	//	{ name: 'clipboard', items: ['Cut', 'Copy', 'Paste', '-', 'Undo', 'Redo'] },
		{ name: 'editing', items: ['Find', 'Replace', '-', 'SelectAll', '-', 'Scayt'] },
		{ name: 'insert', items: ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak'
                 , 'Iframe']
		},
                '/',
		{ name: 'styles', items: ['Styles', 'Format'] },
		{ name: 'basicstyles', items: ['Bold', 'Italic', 'Strike', '-', 'RemoveFormat'] },
		{ name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote'] },
		{ name: 'links', items: ['Link', 'Unlink', 'Anchor'] },
		{ name: 'tools', items: ['Maximize', '-', 'About'] }
	];
    };

