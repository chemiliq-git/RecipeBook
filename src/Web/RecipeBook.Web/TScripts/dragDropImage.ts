class DragDropImage {
    private static readonly DRAG_ENTER_EVENT_NAME = 'dragenter';
    private static readonly DRAG_OVER_EVENT_NAME = 'dragover';
    private static readonly DRAG_DROP_EVENT_NAME = 'drop';
    private static readonly ERROR_MSG = 'error';
    

    private handleImageDroped: (readerResult: string | ArrayBuffer) => void;
    private handleError: (error: string) => void;
    private elementId: string;

    constructor(elementId: string, handleImageDroped: (readerResult: string | ArrayBuffer) => void, handleError: (error: string) => void) {
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

    private handleEnter(e: DragEvent, context: DragDropImage) {
        e.preventDefault();
    }

    private handleOver(e: DragEvent, context: DragDropImage) {
        e.preventDefault();
    }

    private handleDrop(e: DragEvent, context: DragDropImage) {
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
