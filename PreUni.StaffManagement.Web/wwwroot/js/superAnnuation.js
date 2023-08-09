let superAnnuationType = 0;
let id = 0;
var isAge = false;
$(document).ready(function () {
  $.LoadingOverlay("show");
  $.ajax({
    url:
      "/JobApplication/GetTFNDeclareDetail/?jobApplicationId=" +
      $("#jobApplicationId").val(),
    typr: "GET",
    contentType: "application/json;charset=UTF-8",
    dataType: "json",
    success: function (result) {
      $.LoadingOverlay("hide");

      console.log(result);
      if (result.status != 0) {
        var payee = result.data;
        console.log(result.data);
        if (result.data != null) {
          if (
            checkAge(payee.dayOfBirth, payee.monthOfBirth, payee.yearOfBirth)
          ) {
            isAge = false;
            $("#step-parent-consent").addClass("disabled-div");
          } else {
            $("#step-parent-consent").removeClass("disabled-div");
            isAge = true;
          }
        } else {
          isAge = false;
          $("#step-parent-consent").addClass("disabled-div");
        }
      }
    },
    error: function (errormessage) {
      toastr.error(errormessage.responseText, "Error occurred");
      $.LoadingOverlay("hide");
    },
  });

  activeStepper("superannution");
  //$.LoadingOverlay("show");
  scollToTop();

  $.ajax({
    url:
      "/JobApplication/GetSuperAnnuationDetail/?jobApplicationId=" +
      $("#jobApplicationId").val(),
    typr: "GET",
    contentType: "application/json;charset=UTF-8",
    dataType: "json",
    success: function (result) {
      $.LoadingOverlay("hide");
      console.log(result.data);
      if (result.data != null) {
        var superAnnuation = result.data;
        id = superAnnuation.id;
        superAnnuationType = superAnnuation.superAnnuationType;
        check(superAnnuationType);
        $(
          "input[name=question][value=" +
            superAnnuation.superAnnuationType +
            "]"
        ).prop("checked", true);
        $("#employeeName").val(superAnnuation.fullName),
          $("#employeeNumber").val(superAnnuation.employeeNumber),
          $("#employeeTaxFileNumber").val(superAnnuation.taxFileNumber),
          $("#employeeFundName").val(superAnnuation.fundName),
          $("#fundBusinessNumber").val(superAnnuation.fundBusinessNumber),
          $("#fundAnnuationIdentifi").val(superAnnuation.fundAnnuationIdentifi),
          $("#fundAccountNumber").val(superAnnuation.fundAccountNumber),
          $("#fundAppearName").val(superAnnuation.fundAppearName),
          $("#fundAttachLetter").prop(
            "checked",
            superAnnuation.fundAttachLetter
          ),
          $("#fundSignature").val(superAnnuation.fundSignature),
          $("#dateFund").val(formatDate(superAnnuation.dateFundSignature)),
          $("#businessName").val(superAnnuation.businessName),
          $("#businessNumber").val(superAnnuation.businessNumber),
          $("#businessFundName").val(superAnnuation.businessFundName),
          $("#businessFundNumber").val(superAnnuation.businessFundNumber),
          $("#businessAnnuationIdentifi").val(
            superAnnuation.businessAnnuationIdentifi
          ),
          $("#businessNewAccount").prop(
            "checked",
            superAnnuation.businessNewAccount
          ),
          $("#signatureEmloyee").val(superAnnuation.signatureEmloyee),
          $("#dateBusiness").val(
            formatDate(superAnnuation.dateSignatureEmloyee)
          ),
          $("#smsfName").val(superAnnuation.smsfName),
          $("#smsfNumber").val(superAnnuation.smsfNumber),
          $("#smsfServiceAddress").val(superAnnuation.smsfServiceAddress),
          $("#smsfAppearAccount").val(superAnnuation.smsfAppearAccount),
          $("#bankAccountName").val(superAnnuation.bankAccountName),
          $("#bsbCode").val(superAnnuation.bsbCode),
          $("#smsfAcountNumber").val(superAnnuation.smsfAcountNumber),
          $("#haveAtoSmsf").prop("checked", superAnnuation.haveAtoSmsf),
          $("#signatureSmsf").val(superAnnuation.signatureSmsf),
          $("#dateSmsf").val(formatDate(superAnnuation.dateSignatureSmsf));
      }
    },
    error: function (errormessage) {
      toastr.error(errormessage.responseText, "Error occurred");
      $.LoadingOverlay("hide");
    },
  });
  // define mask ruler
  $.mask.definitions["S"] = "[A-Za-z0-9]";
  $("#employeeTaxFileNumber").mask("999-999-999");
  $("#fundBusinessNumber").mask("99-999-999-999");
  $("#businessNumber").mask("99-999-999-999");
  $("#businessFundNumber").mask("99-999-999-999");
  $("#smsfNumber").mask("99-999-999-999");
  $("#bsbCode").mask("999-999");
  $("#smsfAcountNumber").mask("9999-999-999");
  $("#businessAnnuationIdentifi").mask("SSS-SSSS-SSSS-SSS");
  $("#fundAccountNumber").mask("9999-9999-9999-9999");
  $("#fundAnnuationIdentifi").mask("SSS-SSSS-SSSS-SSS");
  $("#employeeNumber").mask("9999-9999-9999-9999");
});

function formatDate(date) {
  var d = new Date(date),
    month = "" + (d.getMonth() + 1),
    day = "" + d.getDate(),
    year = d.getFullYear();

  if (month.length < 2) month = "0" + month;
  if (day.length < 2) day = "0" + day;

  return [year, month, day].join("-");
}

function format_date(dt) {
  if (isNaN(dt.getFullYear())) return "";
}

function submitSuperAnnuation() {
  if ($("#superannuationForm").valid()) {
    var date1 = isNaN($("#dateFund").val());

    console.log(81, formatDate($("#dateFund").val()));
    let obj = {
      SuperAnnuationType: String($("input[name='question']:checked").val()),
      FullName: $("#employeeName").val(),
      EmployeeNumber: $("#employeeNumber").val(),
      TaxFileNumber: $("#employeeTaxFileNumber").val(),
      FundName: $("#employeeFundName").val(),
      FundBusinessNumber: $("#fundBusinessNumber").val(),
      FundAnnuationIdentifi: $("#fundAnnuationIdentifi").val(),
      FundAccountNumber: $("#fundAccountNumber").val(),
      FundAppearName: $("#fundAppearName").val(),
      FundAttachLetter: $("#fundAttachLetter").is(":checked"),
      FundSignature: $("#fundSignature").val(),
      DateFundSignature:
        $("#dateFund").val() != "" ? $("#dateFund").val() : undefined,
      BusinessName: $("#businessName").val(),
      BusinessNumber: $("#businessNumber").val(),
      BusinessFundName: $("#businessFundName").val(),
      BusinessFundNumber: $("#businessFundNumber").val(),
      BusinessAnnuationIdentifi: $("#businessAnnuationIdentifi").val(),
      BusinessNewAccount: $("#businessNewAccount").is(":checked"),
      SignatureEmloyee: $("#signatureEmloyee").val(),
      DateSignatureEmloyee:
        $("#dateBusiness").val() != "" ? $("#dateBusiness").val() : undefined,
      SmsfName: $("#smsfName").val(),
      SmsfNumber: $("#smsfNumber").val(),
      SmsfServiceAddress: $("#smsfServiceAddress").val(),
      SmsfAppearAccount: $("#smsfAppearAccount").val(),
      BankAccountName: $("#bankAccountName").val(),
      BsbCode: $("#bsbCode").val(),
      SmsfAcountNumber: $("#smsfAcountNumber").val(),
      HaveAtoSmsf: $("#haveAtoSmsf").is(":checked"),
      SignatureSmsf: $("#signatureSmsf").val(),
      DateSignatureSmsf:
        $("#dateSmsf").val() != "" ? $("#dateSmsf").val() : undefined,
      JobApplicationId: parseInt($("#jobApplicationId").val()),
    };
    if (id) {
      $(".error").text("");
      $.ajax({
        url: `/JobApplication/UpdateSuperAnnuation/${id}`,
        data: JSON.stringify(obj),
        type: "PUT",
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
          inputChangeFlag = false;
          if (isAge) {
            window.location = "/JobApplication/ParentConsent";
          } else {
            window.location = "/JobApplication/IdentityProoImage";
          }
        },
        error: function (errormessage) {
          //toastr.error("Error occurred");
          handleError(errormessage);
          $.LoadingOverlay("hide");
        },
      });
    } else {
      $(".error").text("");
      $.ajax({
        url: "/JobApplication/SubmitSuperAnnuation/",
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
          inputChangeFlag = false;
          if (isAge) {
            window.location = "/JobApplication/ParentConsent";
          } else {
            window.location = "/JobApplication/IdentityProoImage";
          }
          isComplete3 = true;
        },
        error: function (errormessage) {
          //toastr.error("Error occurred");
          handleError(errormessage);
          $.LoadingOverlay("hide");
        },
      });
    }
  }
}

function handleError(errormessage) {
  toastr.error("Error occurred");
  if (
    errormessage.responseJSON.Errors &&
    errormessage.responseJSON.Errors.SuperAnnuationType
  ) {
    errormessage.responseJSON.Errors.SuperAnnuationType.forEach((item) => {
      $("#superAnnuationTypeS").append(`${item} <br/>`);
    });
  }
  if (
    errormessage.responseJSON.Errors &&
    errormessage.responseJSON.Errors.FullName
  ) {
    errormessage.responseJSON.Errors.FullName.forEach((item) => {
      $("#employeeNameS").append(`${item} <br/>`);
    });
  }
  if (
    errormessage.responseJSON.Errors &&
    errormessage.responseJSON.Errors.EmployeeNumber
  ) {
    errormessage.responseJSON.Errors.EmployeeNumber.forEach((item) => {
      $("#employeeNumberS").append(`${item} <br/>`);
    });
  }
  if (
    errormessage.responseJSON.Errors &&
    errormessage.responseJSON.Errors.FundName
  ) {
    errormessage.responseJSON.Errors.FundName.forEach((item) => {
      $("#employeeFundNameS").append(`${item} <br/>`);
    });
  }
  if (
    errormessage.responseJSON.Errors &&
    errormessage.responseJSON.Errors.FundBusinessNumber
  ) {
    errormessage.responseJSON.Errors.FundBusinessNumber.forEach((item) => {
      $("#fundBusinessNumberS").append(`${item} <br/>`);
    });
  }
  if (
    errormessage.responseJSON.Errors &&
    errormessage.responseJSON.Errors.FundAnnuationIdentifi
  ) {
    errormessage.responseJSON.Errors.FundAnnuationIdentifi.forEach((item) => {
      $("#fundAnnuationIdentifiS").append(`${item} <br/>`);
    });
  }
  if (
    errormessage.responseJSON.Errors &&
    errormessage.responseJSON.Errors.FundAccountNumber
  ) {
    errormessage.responseJSON.Errors.FundAccountNumber.forEach((item) => {
      $("#fundAccountNumberS").append(`${item} <br/>`);
    });
  }
  if (
    errormessage.responseJSON.Errors &&
    errormessage.responseJSON.Errors.FundAppearName
  ) {
    errormessage.responseJSON.Errors.FundAppearName.forEach((item) => {
      $("#fundAppearNameS").append(`${item} <br/>`);
    });
  }
  if (
    errormessage.responseJSON.Errors &&
    errormessage.responseJSON.Errors.FundSignature
  ) {
    errormessage.responseJSON.Errors.FundSignature.forEach((item) => {
      $("#fundSignatureS").append(`${item} <br/>`);
    });
  }
  if (
    errormessage.responseJSON.Errors &&
    errormessage.responseJSON.Errors.DateFundSignature
  ) {
    errormessage.responseJSON.Errors.DateFundSignature.forEach((item) => {
      $("#dateFundS").append(`${item} <br/>`);
    });
  }
  if (
    errormessage.responseJSON.Errors &&
    errormessage.responseJSON.Errors.BusinessName
  ) {
    errormessage.responseJSON.Errors.BusinessName.forEach((item) => {
      $("#businessNameS").append(`${item} <br/>`);
    });
  }
  if (
    errormessage.responseJSON.Errors &&
    errormessage.responseJSON.Errors.BusinessNumber
  ) {
    errormessage.responseJSON.Errors.BusinessNumber.forEach((item) => {
      $("#businessNumberS").append(`${item} <br/>`);
    });
  }
  if (
    errormessage.responseJSON.Errors &&
    errormessage.responseJSON.Errors.BusinessFundName
  ) {
    errormessage.responseJSON.Errors.BusinessFundName.forEach((item) => {
      $("#businessFundNameS").append(`${item} <br/>`);
    });
  }
  if (
    errormessage.responseJSON.Errors &&
    errormessage.responseJSON.Errors.BusinessFundNumber
  ) {
    errormessage.responseJSON.Errors.BusinessFundNumber.forEach((item) => {
      $("#businessFundNumberS").append(`${item} <br/>`);
    });
  }
  if (
    errormessage.responseJSON.Errors &&
    errormessage.responseJSON.Errors.BusinessAnnuationIdentifi
  ) {
    errormessage.responseJSON.Errors.BusinessAnnuationIdentifi.forEach(
      (item) => {
        $("#businessAnnuationIdentifiS").append(`${item} <br/>`);
      }
    );
  }
  if (
    errormessage.responseJSON.Errors &&
    errormessage.responseJSON.Errors.SignatureEmloyee
  ) {
    errormessage.responseJSON.Errors.SignatureEmloyee.forEach((item) => {
      $("#signatureEmloyeeS").append(`${item} <br/>`);
    });
  }
  if (
    errormessage.responseJSON.Errors &&
    errormessage.responseJSON.Errors.DateSignatureEmloyee
  ) {
    errormessage.responseJSON.Errors.DateSignatureEmloyee.forEach((item) => {
      $("#dateBusinessS").append(`${item} <br/>`);
    });
  }
  if (
    errormessage.responseJSON.Errors &&
    errormessage.responseJSON.Errors.SmsfName
  ) {
    errormessage.responseJSON.Errors.SmsfName.forEach((item) => {
      $("#smsfNameS").append(`${item} <br/>`);
    });
  }
  if (
    errormessage.responseJSON.Errors &&
    errormessage.responseJSON.Errors.SmsfNumber
  ) {
    errormessage.responseJSON.Errors.SmsfNumber.forEach((item) => {
      $("#smsfNumberS").append(`${item} <br/>`);
    });
  }
  if (
    errormessage.responseJSON.Errors &&
    errormessage.responseJSON.Errors.SmsfServiceAddress
  ) {
    errormessage.responseJSON.Errors.SmsfServiceAddress.forEach((item) => {
      $("#smsfServiceAddressS").append(`${item} <br/>`);
    });
  }
  if (
    errormessage.responseJSON.Errors &&
    errormessage.responseJSON.Errors.SmsfAppearAccount
  ) {
    errormessage.responseJSON.Errors.SmsfAppearAccount.forEach((item) => {
      $("#smsfAppearAccountS").append(`${item} <br/>`);
    });
  }
  if (
    errormessage.responseJSON.Errors &&
    errormessage.responseJSON.Errors.BankAccountName
  ) {
    errormessage.responseJSON.Errors.BankAccountName.forEach((item) => {
      $("#bankAccountNameS").append(`${item} <br/>`);
    });
  }
  if (
    errormessage.responseJSON.Errors &&
    errormessage.responseJSON.Errors.BsbCode
  ) {
    errormessage.responseJSON.Errors.BsbCode.forEach((item) => {
      $("#bsbCodeS").append(`${item} <br/>`);
    });
  }
  if (
    errormessage.responseJSON.Errors &&
    errormessage.responseJSON.Errors.SmsfAcountNumber
  ) {
    errormessage.responseJSON.Errors.SmsfAcountNumber.forEach((item) => {
      $("#smsfAcountNumberS").append(`${item} <br/>`);
    });
  }
  if (
    errormessage.responseJSON.Errors &&
    errormessage.responseJSON.Errors.SignatureSmsf
  ) {
    errormessage.responseJSON.Errors.SignatureSmsf.forEach((item) => {
      $("#signatureSmsfS").append(`${item} <br/>`);
    });
  }
  if (
    errormessage.responseJSON.Errors &&
    errormessage.responseJSON.Errors.DateSignatureSmsf
  ) {
    errormessage.responseJSON.Errors.DateSignatureSmsf.forEach((item) => {
      $("#dateSmsfS").append(`${item} <br/>`);
    });
  }
}

function check(superAnnuationType) {
  let kq = parseInt(superAnnuationType);
  console.log("#hidden_fields" + String(kq));
  $("#hidden_fields" + String(kq)).show();
}

$(function () {
  // Get the form fields and hidden div
  let checkbox1 = $("#trigger1");
  let hidden1 = $("#hidden_fields1");
  let checkbox2 = $("#trigger2");
  let hidden2 = $("#hidden_fields2");
  let checkbox3 = $("#trigger3");
  let hidden3 = $("#hidden_fields3");

  hidden1.hide();
  checkbox1.change(function () {
    if (checkbox1.is(":checked")) {
      hidden1.show();
      hidden2.hide();
      hidden3.hide();
    } else {
      hidden1.hide();
    }
  });

  hidden2.hide();
  checkbox2.change(function () {
    if (checkbox2.is(":checked")) {
      hidden2.show();
      hidden1.hide();
      hidden3.hide();
    } else {
      hidden2.hide();
    }
  });
  hidden3.hide();
  checkbox3.change(function () {
    if (checkbox3.is(":checked")) {
      hidden1.hide();
      hidden2.hide();
      hidden3.show();
    } else {
      hidden3.hide();
    }
  });
});

$("input:checkbox").on("click", function () {
  var $box = $(this);
  if ($box.is(":checked")) {
    var group = "input:checkbox[name='" + $box.attr("name") + "']";
    $(group).prop("checked", false);
    $box.prop("checked", true);
  } else {
    $box.prop("checked", false);
  }
});
