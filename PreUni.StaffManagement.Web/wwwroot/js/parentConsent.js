$(document).ready(function () {
  $.ajax({
    url:
      "/JobApplication/GetTFNDeclareDetail/?jobApplicationId=" +
      $("#jobApplicationId").val(),
    typr: "GET",
    contentType: "application/json;charset=UTF-8",
    dataType: "json",
    success: function (result) {
      $.LoadingOverlay("hide");
      if (result.status != 0) {
        var payee = result.data;
        console.log(result.data);
        if (result.data != null) {
          if (
            checkAge(payee.dayOfBirth, payee.monthOfBirth, payee.yearOfBirth)
          ) {
            $("#step-parent-consent").addClass("disabled-div");
            window.location = "/JobApplication/TFNDeclare";
          } else $("#step-parent-consent").removeClass("disabled-div");
        } else {
          $("#step-parent-consent").addClass("disabled-div");
          window.location = "/JobApplication/TFNDeclare";
        }
      }
    },
    error: function (errormessage) {
      toastr.error(errormessage.responseText, "Error occurred");
      $.LoadingOverlay("hide");
    },
  });

  validateParentConsent();
  $.LoadingOverlay("show");
  scollToTop();
  activeStepper("parent-consent");
  $.ajax({
    url:
      "/JobApplication/GetParentConsentDetail/?jobApplicationId=" +
      $("#jobApplicationId").val(),
    typr: "GET",
    contentType: "application/json;charset=UTF-8",
    dataType: "json",
    success: function (result) {
      $.LoadingOverlay("hide");

      console.log(result.data);
      if (result.data != null) {
        $("#contactName1").val(result.data.contactNameFirst);
        $("#contactName2").val(result.data.contactNameSecond);
        $("#contactNumber1").val(result.data.contactNameNumberFirst);
        $("#contactNumber2").val(result.data.contactNameNumberSecond);
        $("#parentName").val(result.data.parentName);
        $("#parent-consent-Id").val(result.data.id);
        $("#relationship1").val(result.data.relationshipApplicantFirst);
        $("#relationship2").val(result.data.relationshipApplicantSecond);
        $("#SignatureParent").val(result.data.signature);
        $("#signDateParent").val(result.data.signDate.split("T")[0]);
      }
    },
    error: function (errormessage) {
      toastr.error(errormessage.responseText, "Error occurred");
      $.LoadingOverlay("hide");
    },
  });
  $("#contactNumber1").mask("(+61) 0999 999 999");
  $("#contactNumber2").mask("(+61) 0999 999 999");
});

function submitParentConsent() {
  $(".errorMessage").text("");
  validateParentConsent();
  var obj = {
    ContactNameFirst: $("#contactName1").val(),
    ContactNameNumberFirst: $("#contactNumber1").val(),
    ContactNameSecond: $("#contactName2").val(),
    ContactNameNumberSecond: $("#contactNumber2").val(),
    ParentName: $("#parentName").val(),
    JobApplicationId: parseInt($("#jobApplicationId").val()),
    RelationshipApplicantFirst: $("#relationship1").val(),
    RelationshipApplicantSecond: $("#relationship2").val(),
    Signature: $("#SignatureParent").val(),
    SignDate: $("#signDateParent").val(),
    ApplicantName: $("#nameApplicant").val(),
  };
  if ($("#ParentConsent").valid()) {
    if ($("#parent-consent-Id").val() != 0) {
      $.LoadingOverlay("show");
      $.ajax({
        url:
          "/JobApplication/UpdateParentConsent?jobApplicationId=" +
          parseInt($("#jobApplicationId").val()),
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        processData: false,
        success: function (result) {
          $.LoadingOverlay("hide");
          if (result.status == 0) {
            toastr.error(result.message, "Error");
            return false;
          }

          toastr.success(result.message, "Success");
          window.location = "/JobApplication/IdentityProoImage";
          inputChangeFlag = false;
        },
        error: function (errormessage) {
          $.LoadingOverlay("hide");
          if (errormessage.hasOwnProperty("responseJSON")) {
            console.log(errormessage);
            $("#error-contactName1").text(
              errormessage.responseJSON.Errors.ContactNameFirst
            );
            $("#error-contactNumber1").text(
              errormessage.responseJSON.Errors.ContactNumberFirst
            );
            $("#error-contactName2").text(
              errormessage.responseJSON.Errors.ContactNameSecond
            );
            $("#error-contactNumber2").text(
              errormessage.responseJSON.Errors.ContactNameSecond
            );
            $("#error-parentName").text(
              errormessage.responseJSON.Errors.ParentName
            );
            $("#error-relationship1").text(
              errormessage.responseJSON.Errors.RelationshipApplicantFirst
            );
            $("#error-relationship2").text(
              errormessage.responseJSON.Errors.RelationshipApplicantSecond
            );
            $("#error-signatureParent").text(
              errormessage.responseJSON.Errors.Signature
            );
            $("#error-signDateParent").text(
              errormessage.responseJSON.Errors.SignDate
            );
            toastr.error("Validate error", "Error occurred");
          } else {
            toastr.error(errormessage.responseText, "Error occurred");
          }
        },
      });
    } else {
      $.LoadingOverlay("show");
      $.ajax({
        url: "/JobApplication/SubmitParentConsent",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        processData: false,
        success: function (result) {
          $.LoadingOverlay("hide");
          if (result.status == 0) {
            toastr.error(result.message, "Error");
            return false;
          }

          toastr.success(result.message, "Success");
          window.location = "/JobApplication/IdentityProoImage";
          inputChangeFlag = false;
        },
        error: function (errormessage) {
          $.LoadingOverlay("hide");
          if (errormessage.hasOwnProperty("responseJSON")) {
            console.log(errormessage);
            $("#error-contactName1").text(
              errormessage.responseJSON.Errors.ContactNameFirst
            );
            $("#error-contactNumber1").text(
              errormessage.responseJSON.Errors.ContactNumberFirst
            );
            $("#error-contactName2").text(
              errormessage.responseJSON.Errors.ContactNameSecond
            );
            $("#error-contactNumber2").text(
              errormessage.responseJSON.Errors.ContactNameSecond
            );
            $("#error-parentName").text(
              errormessage.responseJSON.Errors.ParentName
            );
            $("#error-relationship1").text(
              errormessage.responseJSON.Errors.RelationshipApplicantFirst
            );
            $("#error-relationship2").text(
              errormessage.responseJSON.Errors.RelationshipApplicantSecond
            );
            $("#error-signatureParent").text(
              errormessage.responseJSON.Errors.Signature
            );
            $("#error-signDateParent").text(
              errormessage.responseJSON.Errors.SignDate
            );
            toastr.error("Validate error", "Error occurred");
          } else {
            toastr.error(errormessage.responseText, "Error occurred");
          }
        },
      });
    }
  } else {
    toastr.error("Please enter full information", "Error");
  }
}

function validateParentConsent() {
  $("#ParentConsent").validate({
    rules: {
      contactName1: {
        required: true,
        lettersonly: true,
      },
      contactNumber1: {
        required: true,
        minlength: 6,
      },
      contactName2: {
        required: true,
        lettersonly: true,
      },
      contactNumber2: {
        required: true,

        minlength: 6,
      },
      parentName: {
        required: true,
        lettersonly: true,
      },
      SignatureParent: {
        required: true,
      },
      signDateParent: {
        required: true,
      },
      relationship1: {
        required: true,
      },
      relationship2: {
        required: true,
      },
      nameApplicant: {
        required: true,
      },
    },
    messages: {
      contactName1: {
        required: "Contact Name 1 cannot be empty",
      },
      contactNumber1: {
        required: "Contact Number 1 cannot be empty",
      },
      contactName2: {
        required: "Contact Name 2 cannot be empty",
      },
      contactNumber2: {
        required: "Contact Number 2 cannot be empty",
      },
      parentName: {
        required: "Parent's Name cannot be empty",
      },
    },
  });
}
