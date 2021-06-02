function handleFileSelect(event) {
    var selectedFile = event.target.files; // FileList object
    var file = selectedFile[0];
    // Only process image files.
    if (!file.type.match('image.*')) {
        alert("Image only please");
    }
    var reader = new FileReader();
    // Closure to capture the file information.
    reader.onload = (function (theFile) {
        return function (e) {
            // Render thumbnail.
            var locationPreview = document.getElementById('preview');
            var newImage = document.createElement('img');
            locationPreview.innerHTML = '';
            newImage.src = e.target.result;
            newImage.setAttribute("style", "width: 100%; max-width:400px;");
            locationPreview.appendChild(newImage);

        };
    })(file);
    // Read in the image file as a data URL.
    reader.readAsDataURL(file);
}