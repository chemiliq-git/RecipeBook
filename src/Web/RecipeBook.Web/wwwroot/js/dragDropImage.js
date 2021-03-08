class dragDropImage {
    constructor(elementId, handleImageDroped, handleError) {
        this.elementId = elementId;
        this.handleImageDroped = handleImageDroped;
        this.handleError = handleError;
    }
    startListen() {
        let context = this;
        let item = document.getElementById(context.elementId);
        item.addEventListener('dragenter', function (event) { context.handleEnter(event, context); }, false);
        item.addEventListener('dragover', function (event) { context.handleOver(event, context); }, false);
        item.addEventListener('drop', function (event) { context.handleDrop(event, context); }, false);
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
        if (files && files.length == 1 && files[0]['type'].split('/')[0] === 'image') {
            let reader = new FileReader();
            reader.onload = function (event) {
                context.handleImageDroped(reader.result);
            };
            reader.readAsDataURL(files[0]);
        }
        else {
            context.handleError('Error');
        }
    }
}
//# sourceMappingURL=dragDropImage.js.map