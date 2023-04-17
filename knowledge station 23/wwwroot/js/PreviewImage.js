$(document).ready(function () {
    $('#ChoseImage').change(function (e) {



        let url = $('#ChoseImage').val();
        let ext = url.substring(url.lastIndexOf('.') + 1).toLowerCase();
        if (ChoseImage.files && ChoseImage.files[0] && (ext == "gif" || ext == "jpg" || ext == "jfif" || ext == "png" || ext == "bmp")) {
            let reader = new FileReader();
            reader.onload = function () {
                let output = document.getElementById('PreviewImage');
                output.src = reader.result;
            }
            reader.readAsDataURL(e.target.files[0]);
        }
    });
});