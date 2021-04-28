class ImageFileSelector {
    constructor(elementId, handleImageSelected, handleError) {
        this.elementId = elementId;
        this.handleImageSelected = handleImageSelected;
        this.handleError = handleError;
    }
    startListen() {
        let fileSelector = document.getElementById(this.elementId);
        fileSelector?.addEventListener('change', (event) => {
            const fileList = event.target?.files;
            if (fileList && fileList.length == 1 && fileList[0][Const.FILE_MEDIA_TYPE_KEY].split('/')[0] === Const.FILE_IMAGE_TYPE_KEY) {
                let reader = new FileReader();
                reader.onload = (event) => {
                    this.handleImageSelected(reader.result);
                };
                reader.readAsDataURL(fileList[0]);
            }
            else {
                this.handleError(ImageFileSelector.ERROR_MSG);
            }
        });
    }
}
ImageFileSelector.ERROR_MSG = "Error loading file";
//# sourceMappingURL=imageFileSelector.js.map