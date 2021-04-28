class DragDropImage {
    constructor(elementId, handleImageDroped, handleError) {
        this.elementId = elementId;
        this.handleImageDroped = handleImageDroped;
        this.handleError = handleError;
    }
    startListen() {
        let context = this;
        let item = document.getElementById(context.elementId);
        item.addEventListener(DragDropImage.DRAG_ENTER_EVENT_NAME, function (event) { context.handleEnter(event, context); }, false);
        item.addEventListener(DragDropImage.DRAG_OVER_EVENT_NAME, function (event) { context.handleOver(event, context); }, false);
        item.addEventListener(DragDropImage.DRAG_DROP_EVENT_NAME, function (event) { context.handleDrop(event, context); }, false);
    }
    handleEnter(e, context) {
        e.preventDefault();
    }
    handleOver(e, context) {
        e.preventDefault();
    }
    handleDrop(e, context) {
        e.stopPropagation();
        e.preventDefault();
        let files = e.dataTransfer.files;
        if (files && files.length == 1 && files[0][Const.FILE_MEDIA_TYPE_KEY].split('/')[0] === Const.FILE_IMAGE_TYPE_KEY) {
            let reader = new FileReader();
            reader.onload = function (event) {
                context.handleImageDroped(reader.result);
            };
            reader.readAsDataURL(files[0]);
        }
        else {
            context.handleError(DragDropImage.ERROR_MSG);
        }
    }
}
DragDropImage.DRAG_ENTER_EVENT_NAME = 'dragenter';
DragDropImage.DRAG_OVER_EVENT_NAME = 'dragover';
DragDropImage.DRAG_DROP_EVENT_NAME = 'drop';
DragDropImage.ERROR_MSG = 'error';
//# sourceMappingURL=dragDropImage.js.map