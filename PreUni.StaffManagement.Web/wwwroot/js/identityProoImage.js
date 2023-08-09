$(document).ready(function () {
    activeStepper('identity-proof');
    $.LoadingOverlay("show");
    scollToTop();
   
    
    $.ajax({
        url: "/JobApplication/GetProoImageDetail/?jobApplicationId=" + $('#jobApplicationId').val(),
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $.LoadingOverlay("hide");
            console.log(result.data);
            if (result.data.image != '' && result.data.image != null) {
                inputChangeFlag = false;
                $('#imageFile').attr('src', result.data.image);
                $('#frame').attr('src', '/'+ result.data.image);
            }
        },
        error: function (errormessage) {
            toastr.error(errormessage.responseText, "Error occurred");
            $.LoadingOverlay("hide");
        }
    });


    
})

function preview() {
    frame.src = URL.createObjectURL(event.target.files[0]);
}

function clearImage() {
    document.getElementById('imageFile').value = null;
    frame.src = "";
}

function submitIdentityProofImage() {
    // start submit Image
    validateIdentityProoImage();
    

    var files = $('#imageFile')[0].files; //get files
    var formData = new FormData(); //create form

    formData.append("imageFile", files[0]);
    formData.append("jobApplicationId", parseInt($('#jobApplicationId').val()));
    //formData.append("emailRegister", $('#email-register').val());
    if ($('#identityProofForm').valid()) {
        $.LoadingOverlay("show");
        $.ajax({
            url: "/JobApplication/SubmitProoImage/",
            data: formData,
            type: "POST",
            contentType: false,
            async: true,
            processData: false,
            success: function (result) {
                $.LoadingOverlay("hide");
                if (result.status == 0) {
                    toastr.error(result.message, "Error");
                    return false;
                }
                inputChangeFlag = false;
                if (result.status == 1) {
                    toastr.success(result.message, "Success");
                }
                window.location = "/JobApplication/Complete";
            },
            error: function (errormessage) {
                toastr.error(errormessage.responseText, "Error occurred");
                $.LoadingOverlay("hide");
            }
        });
    }
    
}




function validateIdentityProoImage(){
    $("#identityProofForm").validate({
        rules: {
            imageFile: {
                required: true,
                extension: "png|jpg|jpeg"
            }
        }
    });
}

