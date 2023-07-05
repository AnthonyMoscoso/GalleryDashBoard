export function ImportFile(idInput) {
    document.getElementById(idInput).click();
}

export function initializeFileDropZone(dropZoneElement, inputFile, allowedFileTypes) {



    let isCorrectFile = false;
    let allowedTypes = allowedFileTypes || [];
    // Add a class when the user drags a file over the drop zone
    if (dropZoneElement && inputFile) {
        function onDragHover(e) {
            e.preventDefault();
            dropZoneElement.classList.add("hover");
        }

        function onDragLeave(e) {
            e.preventDefault();
            dropZoneElement.classList.remove("hover");
        }

        // Handle the paste and drop events
        function onDrop(e) {
            e.preventDefault();
            dropZoneElement.classList.remove("hover");

                // Check if file type is allowed
                if (allowedTypes.length > 0) {
                    let file = e.dataTransfer.files[0];
                    let fileType = file.type;
                    console.log(fileType);
                    if (!allowedTypes.includes(fileType)) {
                        isCorrectFile = false;
                        return;
                    }
                    isCorrectFile = true;

                // Set the files property of the input element and raise the change event
                inputFile.files = e.dataTransfer.files;
                const event = new Event('change', { bubbles: true });
                inputFile.dispatchEvent(event);
            }
        }

        function onPaste(e) {
    
                // Check if file type is allowed
                if (allowedTypes.length > 0) {
                    let file = e.clipboardData.files[0];
                    let fileType = file.type;

                    if (!allowedTypes.includes(fileType)) {
                        isCorrectFile = false;
                        return;
                    }

                    isCorrectFile = true;
                }

                // Set the files property of the input element and raise the change event
                inputFile.files = e.clipboardData.files;
                const event = new Event('change', { bubbles: true });
                inputFile.dispatchEvent(event);
            
        }

        // Register all events
        dropZoneElement.addEventListener("dragenter", onDragHover);
        dropZoneElement.addEventListener("dragover", onDragHover);
        dropZoneElement.addEventListener("dragleave", onDragLeave);
        dropZoneElement.addEventListener("drop", onDrop);
        dropZoneElement.addEventListener('paste', onPaste);

        // The returned object allows to unregister the events when the Blazor component is destroyed
 return {
      dispose: () => {
        dropZoneElement.removeEventListener('dragenter', onDragHover);
        dropZoneElement.removeEventListener('dragover', onDragHover);
        dropZoneElement.removeEventListener('dragleave', onDragLeave);
        dropZoneElement.removeEventListener("drop", onDrop);
        dropZoneElement.removeEventListener('paste', handler);
      },
      checkFileInputAllowed: () => {
         return isCorrectFile;
      }
    }
  }
}