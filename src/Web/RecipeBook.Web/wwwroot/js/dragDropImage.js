class DragDropImage {
    constructor(elementId, handleImageDroped, handleError) {
        this.elementId = elementId;
        this.handleImageDroped = handleImageDroped;
        this.handleError = handleError;
    }
    startListen() {
        let item = document.getElementById(this.elementId);
        item.addEventListener(DragDropImage.DRAG_ENTER_EVENT_NAME, (e) => { this.handleEnter(e); }, false);
        item.addEventListener(DragDropImage.DRAG_OVER_EVENT_NAME, (e) => { this.handleOver(e); }, false);
        item.addEventListener(DragDropImage.DRAG_DROP_EVENT_NAME, (e) => { this.handleDrop(e); }, false);
    }
    handleEnter(e) {
        e.preventDefault();
    }
    handleOver(e) {
        e.preventDefault();
    }
    handleDrop(e) {
        e.stopPropagation();
        e.preventDefault();
        let files = e.dataTransfer.files;
        if (files && files.length == 1 && files[0][Const.FILE_MEDIA_TYPE_KEY].split('/')[0] === Const.FILE_IMAGE_TYPE_KEY) {
            let reader = new FileReader();
            reader.onload = (event) => {
                this.handleImageDroped(reader.result);
            };
            reader.readAsDataURL(files[0]);
        }
        else {
            this.handleError(DragDropImage.ERROR_MSG);
        }
    }
}
DragDropImage.DRAG_ENTER_EVENT_NAME = 'dragenter';
DragDropImage.DRAG_OVER_EVENT_NAME = 'dragover';
DragDropImage.DRAG_DROP_EVENT_NAME = 'drop';
DragDropImage.ERROR_MSG = 'error';
//# sourceMappingURL=dragDropImage.js.map