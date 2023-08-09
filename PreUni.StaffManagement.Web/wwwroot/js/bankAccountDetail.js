$(document).ready(function () {
  // icon
  validateBankAccountDetail();
  activeStepper("bank-account");

  $.LoadingOverlay("show");
  scollToTop();

  $.ajax({
    url:
      "/JobApplication/GetBankAccountDetail/?jobApplicationId=" +
      $("#jobApplicationId").val(),
    typr: "GET",
    contentType: "application/json;charset=UTF-8",
    dataType: "json",
    success: function (result) {
      $.LoadingOverlay("hide");

      console.log(result.data);
      if (result.data != null) {
        $("#bankName").val(result.data.bankName);
        $("#accountHolderName").val(result.data.accountHolderName);
        $("#BSBNumber").val(result.data.bsbNumber);
        $("#emailAddress").val(result.data.emailAddress);
        $("#phoneNumber").val(result.data.phoneNumber);
        $("#accountNumber").val(result.data.accountNumber);
        $("#bank-account-Id").val(result.data.id);
        $("#SignatureBank").val(result.data.signature);
        $("#signDateBank").val(result.data.signDate.split("T")[0]);
      }
    },
    error: function (errormessage) {
      toastr.error(errormessage.responseText, "Error occurred");
      $.LoadingOverlay("hide");
    }
  });
  $("#BSBNumber").mask("999-999");
  $("#accountNumber").mask("9999-999-999");
  $("#phoneNumber").mask("(+61) 0999 999 999");
});

function submitBankAccount() {
  $(".errorMessage").text("");
  validateBankAccountDetail();
  var obj = {
    BankName: $("#bankName").val(),
    AccountHolderName: $("#accountHolderName").val(),
    BSBNumber: $("#BSBNumber").val(),
    AccountNumber: $("#accountNumber").val(),
    EmailAddress: $("#emailAddress").val(),
    PhoneNumber: $("#phoneNumber").val(),
    JobApplicationId: parseInt($("#jobApplicationId").val()),
    NameApplicant: $("#nameApplicant").val(),
    Signature: $("#SignatureBank").val(),
    SignDate: $("#signDateBank").val(),
  };
  if ($("#bankAccountDetailForm").valid()) {
    if ($("#bank-account-Id").val() != 0) {
      $.LoadingOverlay("show");
      $.ajax({
        url:
          "/JobApplication/UpdateBankAccountDetail?jobApplicationId=" +
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
          window.location = "/JobApplication/Superannuation";
          inputChangeFlag = false;
        },
        error: function (errormessage) {
          $.LoadingOverlay("hide");
          if (errormessage.hasOwnProperty("responseJSON")) {
            $("#error-bankName").text(
              errormessage.responseJSON.Errors.BankName
            );
            $("#error-accountHolderName").text(
              errormessage.responseJSON.Errors.AccountHolderName
            );
            $("#error-BSBNumber").text(
              errormessage.responseJSON.Errors.BSBNumber
            );
            $("#error-accountNumber").text(
              errormessage.responseJSON.Errors.AccountNumber
            );
            $("#error-emailAddress").text(
              errormessage.responseJSON.Errors.EmailAddress
            );
            $("#error-phoneNumber").text(
              errormessage.responseJSON.Errors.PhoneNumber
            );
            $("#error-signatureBank").text(
              errormessage.responseJSON.Errors.Signature
            );
            $("#error-signDateBank").text(
              errormessage.responseJSON.Errors.SignDate
            );
            $("#error-nameApplicant").text(
              errormessage.responseJSON.Errors.NameApplicant
            );
            toastr.error("Validate Error", "Error occurred");
          } else {
            toastr.error(errormessage.responseText, "Error occurred");
          }
        },
      });
    } else {
      $.LoadingOverlay("show");
      $.ajax({
        url: "/JobApplication/SubmitBankAccount",
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
          window.location = "/JobApplication/Superannuation";
          inputChangeFlag = false;
          isComplete2 = true;
        },
        error: function (errormessage) {
          $.LoadingOverlay("hide");
          if (errormessage.hasOwnProperty("responseJSON")) {
            $("#error-bankName").text(
              errormessage.responseJSON.Errors.BankName
            );
            $("#error-accountHolderName").text(
              errormessage.responseJSON.Errors.AccountHolderName
            );
            $("#error-BSBNumber").text(
              errormessage.responseJSON.Errors.BSBNumber
            );
            $("#error-accountNumber").text(
              errormessage.responseJSON.Errors.AccountNumber
            );
            $("#error-emailAddress").text(
              errormessage.responseJSON.Errors.EmailAddress
            );
            $("#error-phoneNumber").text(
              errormessage.responseJSON.Errors.PhoneNumber
            );
            $("#error-signatureBank").text(
              errormessage.responseJSON.Errors.Signature
            );
            $("#error-signDateBank").text(
              errormessage.responseJSON.Errors.SignDate
            );
            $("#error-nameApplicant").text(
              errormessage.responseJSON.Errors.NameApplicant
            );
            toastr.error("Validate Error", "Error occurred");
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

function validateBankAccountDetail() {
  $("#bankAccountDetailForm").validate({
    rules: {
      bankName: {
        required: true,
        maxlength: 30,
        lettersonly: true,
        normalizer: function (value) {
          return $.trim(value);
        },
      },
      accountHolderName: {
        required: true,
        maxlength: 30,
        lettersonly: true,
        normalizer: function (value) {
          return $.trim(value);
        },
      },
      BSBNumber: {
        required: true,
        maxlength: 15,
      },
      accountNumber: {
        required: true,
        maxlength: 15,
      },
      emailAddress: {
        required: true,
        normalizer: function (value) {
          return $.trim(value);
        },
        email: true,
      },
      phoneNumber: {
        required: true,

        minlength: 6,
      },
      nameApplicant: {
        required: true,
        normalizer: function (value) {
          return $.trim(value);
        },
      },
      SignatureBank: {
        required: true,
      },
      signDateBank: {
        required: true,
      },
    },
    messages: {
      bankName: {
        required: "BankName cannot be empty",
      },
      accountHolderName: {
        required: "AccountHolderName cannot be empty",
      },
      BSBNumber: {
        required: "BSBNumber cannot be empty",
      },
      accountNumber: {
        required: "AccountNumber cannot be empty",
      },
      emailAddress: {
        required: "EmailAddress cannot be empty",
        email: "Email in invalid",
      },
      phoneNumber: {
        required: "PhoneNumber cannot be empty",
      },
      SignatureBank: {
        required: "Signature cannot be empty",
      },
      signDateBank: {
        required: "Signature Date cannot be empty",
      },
      nameApplicant: {
        required: "Name Applicant cannot be empty",
      },
    },
    errorPlacement: function (error, element) {
      if (element.attr("type") == "radio") {
        error.insertAfter("#error-" + element.attr("name"));
      } else {
        error.insertAfter(element);
      }
    },
  });
}
