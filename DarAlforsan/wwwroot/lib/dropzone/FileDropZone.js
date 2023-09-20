
Dropzone.autoDiscover = false;

class FileDropZone {

    divID;
    targetControl;
    messageTitle;
    maxFiles;
    acceptedFiles;
    uploadUrl;
    dropzoneRecorces;

    constructor(divID, targetControl, messageTitle, dropzoneRecorces, uploadUrl, accept, maxFiles) {
        this.divID = divID;
        this.targetControl = targetControl;
        this.messageTitle = messageTitle;
        this.dropzoneRecorces = dropzoneRecorces;
        this.maxFiles = maxFiles;
        this.acceptedFiles = accept;
        this.uploadUrl = uploadUrl == "" ? "/Core.IO/File/UploadSingleFile" : uploadUrl;
        this.initDropZone();
    }
    initDropZone() {
        var target = this.targetControl;
        var maxfileCount = this.maxFiles;
        var contianerDiv = this.divID;
        new Dropzone(
            "div#" + this.divID, {
            url: this.uploadUrl,
            maxFiles: this.maxFiles,
            addRemoveLinks: true,
            thumbnailWidth: 90,
            thumbnailHeight: 90,
            acceptedFiles: this.acceptedFiles,
            dictDefaultMessage: "<span class='text-secondary h4'><i class='fad fa-cloud-upload-alt text-theme'></i> " + this.messageTitle + "</span>",
            // dictFallbackMessage: "Your browser does not support drag'n'drop file uploads.",
            //  dictFallbackText: "Please use the fallback form below to upload your files like in the olden days.",
            //dictRemoveFileConfirmation: this.dropzoneRecorces.dictCancelUploadConfirmation,
            dictFileTooBig: this.dropzoneRecorces.dictFileTooBig,
            dictInvalidFileType: this.dropzoneRecorces.dictInvalidFileType,
            dictResponseError: this.dropzoneRecorces.dictResponseError,
            dictCancelUpload: this.dropzoneRecorces.dictCancelUpload,
            dictCancelUploadConfirmation: this.dropzoneRecorces.dictCancelUploadConfirmation,
            dictRemoveFile: this.dropzoneRecorces.dictCancelUploadConfirmation,
            dictMaxFilesExceeded: this.dropzoneRecorces.dictCancelUploadConfirmation,

            init: function () {
                this.on("complete", function (file) {
                    $("div#" + contianerDiv + " .dz-remove").html("<i class='fas fa-times  text-danger mt-2 fa-lg' style='cursor: pointer !important;' ></i>");
                    $(file.previewElement).append("<a class='d-inline' href='/FileUpload/Download?FileName=" + file.serverId + "' ><i class='fas fa-download  text-theme ' style='cursor: pointer !important;' ></i></a>");
                });

                this.on("processing", function (file) {

                    $("div#" + contianerDiv + ".dz-remove").html("<i class='fas fa-times  text-danger mt-2 fa-lg' style='cursor: pointer !important;' ></i>");
                });

                this.on("success", function (file, response) {
                    if (response.status == EXResultStatus.Success) {
                        var data = JSON.parse(response.data);
                        if (maxfileCount == 1) {
                            $("#" + target).val(data.FileName);
                            $(`#${target}_aliasName`).val(data.FileAliasName);
                        } else {
                            var attchmentsStr = $("#" + target).val();
                            var attchmentsArr = attchmentsStr == '' ? [] : JSON.parse(attchmentsStr);
                            attchmentsArr.push({
                                FileName: data.FileName,
                                FileAliasName: data.FileAliasName
                            });

                            $("#" + target).val(JSON.stringify(attchmentsArr));
                        }
                        file.serverId = data.FileName;

                    }
                    else {
                        this.removeFile(file);
                        toastr.error(response.message);
                    }

                });

                this.on("removedfile", function (file) {
                    if (maxfileCount == 1) {
                        var itemVal = $("#" + target).val();
                        itemVal = itemVal.replace(file.serverId, '');
                        $("#" + target).val(itemVal);
                    }
                    else {
                        var attchmentsStr = $("#" + target).val();
                        var attchmentsArr = attchmentsStr == '' ? [] : JSON.parse(attchmentsStr);
                        attchmentsArr = attchmentsArr.filter(function (obj) {
                            return obj.FileName !== file.serverId;
                        });
                        $("#" + target).val(JSON.stringify(attchmentsArr));
                    }
                });
                this.on("maxfilesexceeded", function (file) {
                    this.removeFile(file);

                });


                if (maxfileCount > 1) {
                    debugger;
                    var fileName = $("#" + target).val();
                    if (fileName) {
                        var arr = JSON.parse(fileName)
                        for (var i = 0; i < arr.length; i++) {
                            let mockFile = { name: arr[i].FileName, size: 12000, status: "success", accepted: true, serverId: arr[i].FileName };
                            if (DragDrop.isImage(mockFile.name)) {
                                this.displayExistingFile(mockFile, `/${DragDrop.fileStorage}/${mockFile.name}`);
                            }
                            else {
                                this.displayExistingFile(mockFile, `/lib/dropzone/docs.png`,);
                            }
                            this.files.push(mockFile);
                        }
                    }
                }
                else {
                    var fileName = $("#" + target).val();
                    if (fileName) {
                        let mockFile = { name: fileName, size: 12000, status: "success", accepted: true, serverId: fileName };
                        if (DragDrop.isImage(mockFile.name)) {
                            this.displayExistingFile(mockFile, `/${DragDrop.fileStorage}/${mockFile.name}`);
                        }
                        else {
                            this.displayExistingFile(mockFile, `/lib/dropzone/docs.png`,);
                        }
                        this.files.push(mockFile);
                    }

                }
                this.on("addedfile", function (file) {
                    if (!DragDrop.isImage(file.name)) {
                        $(file.previewElement).removeClass('dz-file-preview');
                        $(file.previewElement).addClass('dz-image-preview');
                        $(file.previewElement).find(".dz-image img").attr("src", "/lib/dropzone/docs.png");
                        $(file.previewElement).find(".dz-image img").attr("width", "90");
                    }
                });
                //this.on("addedfile", function (file) {
                //    var ext = file.name.split('.').pop();
                //    if (ext == "pdf") {
                //        $(file.previewElement).removeClass('dz-file-preview');
                //        $(file.previewElement).addClass('dz-image-preview');
                //        $(file.previewElement).find(".dz-image img").attr("src", "/lib/dropzone/pdf.png");
                //    }
                //    else if (ext.indexOf("doc") != -1) {
                //        $(file.previewElement).removeClass('dz-file-preview');
                //        $(file.previewElement).addClass('dz-image-preview');
                //        $(file.previewElement).find(".dz-image img").attr("src", "/lib/dropzone/word.png");
                //    }
                //    else if (ext.indexOf("xls") != -1) {
                //        $(file.previewElement).removeClass('dz-file-preview');
                //        $(file.previewElement).addClass('dz-image-preview');
                //        $(file.previewElement).find(".dz-image img").attr("src", "/lib/dropzone/excel.png");
                //    }
                //});
            }

        });
    }
}
class DropZoneResource {

    dictFileTooBig;
    dictInvalidFileType;
    dictResponseError;
    dictCancelUpload;
    dictCancelUploadConfirmation;
    dictRemoveFile;
    dictMaxFilesExceeded;
    acceptedFiles;

    constructor(
        dictFileTooBig,
        dictInvalidFileType,
        dictResponseError,
        dictCancelUpload,
        dictCancelUploadConfirmation,
        dictRemoveFile,
        // dictRemoveFileConfirmation,
        dictMaxFilesExceeded
    ) {

        this.dictFileTooBig = dictFileTooBig;
        this.dictInvalidFileType = dictInvalidFileType;
        this.dictResponseError = dictResponseError;
        this.dictCancelUpload = dictCancelUpload;
        this.dictCancelUploadConfirmation = dictCancelUploadConfirmation;
        this.dictRemoveFile = dictRemoveFile;
        this.dictMaxFilesExceeded = dictMaxFilesExceeded;
        //  this.dictRemoveFileConfirmation = dictRemoveFileConfirmation;

    }
}
var DragDrop =
{
    fileStorage: 'FileStorage',
    dropZoneResourece: null,
    InitDropZone: function () {
        $(".dropzone[ajax]").each(function (i, val) {
            id = $(val).attr('id');
            targetControl = $(val).attr('target-control');
            maxFiles = $(val).attr('max-files');
            acceptedFiles = $(val).attr('accepted-files');
            messageTitle = $(val).attr('message-title');
            uploadUrl = $(val).attr('upload-url');
            uploadUrl = uploadUrl == undefined ? "" : uploadUrl;
            $(val).removeAttr('ajax');
            new FileDropZone(id, targetControl, messageTitle, DragDrop.dropZoneResourece, uploadUrl, acceptedFiles, maxFiles);
            //if (maxFiles > 1) {

            //}
            //else {
            //    var fileName = $("#" + targetControl).val();
            //    if (fileName) {
            //        DragDrop.displayExist(id, fileName);
            //    }
            //}
        });

    },
    isImage: function (fileName) {
        var fileExtension = ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
        if ($.inArray(fileName.split('.').pop().toLowerCase(), fileExtension) == -1) {
            return false;
        }
        return true;
    }
}