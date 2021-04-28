class ImageFileSelector {
    private static readonly ERROR_MSG = "Error loading file";

    private elementId: string;
    private handleImageSelected: (readerResult: string | ArrayBuffer) => void;
    private handleError: (error: string) => void;

    constructor(elementId: string, handleImageSelected: (readerResult: string | ArrayBuffer) => void, handleError: (error: string) => void) {
        this.elementId = elementId;
        this.handleImageSelected = handleImageSelected;
        this.handleError = handleError;
    }

    startListen(this: ImageFileSelector)
    {
        let fileSelector = document.getElementById(this.elementId);
        fileSelector?.addEventListener('change', (event) => {
            const fileList = (<HTMLInputElement>event.target)?.files;

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