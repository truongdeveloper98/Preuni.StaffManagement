$("#step-parent-consent").addClass("disabled-div");

$(document).ready(function () {
  validatePayee();
  $("#taxFileNumber").click(() => {
    $("#taxFileNumberOpt1").prop("checked", true);
    $("#taxFileNumber").prop("readonly", false);
  });
  $("#taxFileNumberOpt1").click(() => {
    $("#taxFileNumber").prop("readonly", false);
  });
  $("#taxFileNumberOpt2").click(() => {
    $("#taxFileNumber").prop("readonly", true);
    $("#taxFileNumber").val("");
  });
  $("#taxFileNumberOpt3").click(() => {
    $("#taxFileNumber").prop("readonly", true);
    $("#taxFileNumber").val("");
  });
  $("#taxFileNumberOpt4").click(() => {
    $("#taxFileNumber").prop("readonly", true);
    $("#taxFileNumber").val("");
  });

  activeStepper("payee");
  $.LoadingOverlay("show");
  scollToTop();

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
        $("#tfn-Id").val(payee.id);
        $(
          "input[name=taxFileNumberOpt][value=" + payee.taxNumberOption + "]"
        ).prop("checked", true);
        $("#taxFileNumber").val(payee.taxNumber);
        $("#surname").val(payee.surname);
        $("#firstName").val(payee.firstName);
        $("#otherName").val(payee.otherName);
        $("#homeAddress").val(payee.address);
        $("#suburb").val(payee.suburb);
        $("#state").val(payee.state);
        $("#postCode").val(payee.postCode);
        $("#previousFamilyName").val(payee.previousLastName);
        $(
          "input[name=claimSenior][value=" +
            payee.isTaxOffsetForPensioners +
            "]"
        ).prop("checked", true);
        $("#dayOfBirth").val(payee.dayOfBirth);
        $("#monthOfBirth").val(payee.monthOfBirth);
        $("#yearOfBirth").val(payee.yearOfBirth);
        $("input[name=nameTitle][value=" + payee.nameTitle + "]").prop(
          "checked",
          true
        );
        $("input[name=paidBasis][value=" + payee.workType + "]").prop(
          "checked",
          true
        );
        $(
          "input[name=residencyStatus][value=" + payee.isAustraliaResident + "]"
        ).prop("checked", true);
        $("input[name=claimTaxFree][value=" + payee.isTaxFee + "]").prop(
          "checked",
          true
        );
        $(
          "input[name=haveLoanProgram][value=" + payee.isEducationLoan + "]"
        ).prop("checked", true);
        $(
          "input[name=haveFinancialSupplement][value=" +
            payee.isFinancialSupplementSupport +
            "]"
        ).prop("checked", true);
        $(
          "input[name=claimZone][value=" + payee.isTaxOffsetForCarer + "]"
        ).prop("checked", true);
        $("#SignatureTFN").val(payee.signature);
        $("#signDateTFN").val(payee.signDate.split("T")[0]);
        if (result.data != null) {
          if (
            checkAge(payee.dayOfBirth, payee.monthOfBirth, payee.yearOfBirth)
          ) {
            $("#step-parent-consent").addClass("disabled-div");
            $("#side-parent-step").addClass("disabled-div");
          } else {
            $("#step-parent-consent").removeClass("disabled-div");
            $("#side-parent-step").removeClass("disabled-div");
          }
        } else {
          $("#step-parent-consent").addClass("disabled-div");
          $("#side-parent-step").addClass("disabled-div");
        }
      }
    },
    error: function (errormessage) {
      toastr.error(errormessage.responseText, "Error occurred");
      $.LoadingOverlay("hide");
    },
  });
  $("#taxFileNumber").mask("999-999-999");
});

function submitPayee() {
  $(".errorMessage").text("");
  validatePayee();
  var date = new Date($("#signDateTFN").val());

  var obj = {
    TaxNumberOption: $("input[name='taxFileNumberOpt']:checked").val(),
    TaxNumber: $("#taxFileNumber").val(),
    NameTitle: $("input[name='nameTitle']:checked").next().text(),
    Surname: $("#surname").val(),
    FirstName: $("#firstName").val(),
    OtherName: $("#otherName").val(),
    PreviousLastName: $("#previousFamilyName").val(),
    Address: $("#homeAddress").val(),
    Suburb: $("#suburb").val(),
    State: $("#state").val(),
    PostCode: $("#postCode").val(),
    DayOfBirth: parseInt($("#dayOfBirth").val()),
    MonthOfBirth: parseInt($("#monthOfBirth").val()),
    YearOfBirth: parseInt($("#yearOfBirth").val()),
    WorkType: parseInt($("input[name='paidBasis']:checked").val()),
    IsAustraliaResident:
      $("input[name='residencyStatus']:checked").val() === "true"
        ? true
        : false,
    IsTaxFee:
      $("input[name='claimTaxFree']:checked").val() === "true" ? true : false,
    IsTaxOffsetForPensioners:
      $("input[name='claimSenior']:checked").val() === "true" ? true : false,
    IsTaxOffsetForCarer:
      $("input[name='claimZone']:checked").val() === "true" ? true : false,
    IsEducationLoan:
      $("input[name='haveLoanProgram']:checked").val() === "true"
        ? true
        : false,
    IsFinancialSupplementSupport:
      $("input[name='haveFinancialSupplement']:checked").val() === "true"
        ? true
        : false,
    JobApplicationId: parseInt($("#jobApplicationId").val()),
    Signature: $("#SignatureTFN").val(),
    SignDate: date.toISOString(),
  };
  if ($("#payeeForm").valid()) {
    if ($("#tfn-Id").val() != 0) {
      $.LoadingOverlay("show");
      $.ajax({
        url:
          "/JobApplication/UpdateTFNDeclare?jobApplicationId=" +
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
          inputChangeFlag = false;
          window.location = "/JobApplication/BankAccountDetail";
        },
        error: function (errormessage) {
          $.LoadingOverlay("hide");
          if (errormessage.hasOwnProperty("responseJSON")) {
            toastr.error("Validate error", "Error occurred");
            $("#error-taxFileNumberOpt").text(
              errormessage.responseJSON.Errors.TaxNumberOption
            );
            $("#error-nameTitle").text(
              errormessage.responseJSON.Errors.NameTitle
            );
            $("#error-surname").text(errormessage.responseJSON.Errors.Surname);
            $("#error-firstName").text(
              errormessage.responseJSON.Errors.FirstName
            );
            $("#error-dayOfBirth").text(
              errormessage.responseJSON.Errors.DayOfBirth
            );
            $("#error-monthOfBirth").text(
              errormessage.responseJSON.Errors.MonthOfBirth
            );
            $("#error-yearOfBirth").text(
              errormessage.responseJSON.Errors.YearOfBirth
            );
            $("#error-homeAddress").text(
              errormessage.responseJSON.Errors.Address
            );
            $("#error-suburb").text(errormessage.responseJSON.Errors.Suburb);
            $("#error-state").text(errormessage.responseJSON.Errors.State);
            $("#error-postCode").text(
              errormessage.responseJSON.Errors.PostCode
            );
            $("#error-paidBasis").text(
              errormessage.responseJSON.Errors.WorkType
            );
            $("#error-residencyStatus").text(
              errormessage.responseJSON.Errors.IsAustraliaResident
            );
            $("#error-claimTaxFree").text(
              errormessage.responseJSON.Errors.IsTaxFee
            );
            $("#error-claimSenior").text(
              errormessage.responseJSON.Errors.IsTaxOffsetForPensioners
            );
            $("#error-claimZone").text(
              errormessage.responseJSON.Errors.IsTaxOffsetForCarer
            );
            $("#error-haveLoanProgram").text(
              errormessage.responseJSON.Errors.IsEducationLoan
            );
            $("#error-haveFinancialSupplement").text(
              errormessage.responseJSON.Errors.IsFinancialSupplementSupport
            );
            $("#error-signatureTFN").text(
              errormessage.responseJSON.Errors.Signature
            );
            $("#error-signaDateTFN").text(
              errormessage.responseJSON.Errors.SignDate
            );
            $("#error-dayOfBirth").text(
              errormessage.responseJSON.Errors.DayOfBirth
            );
            $("#error-monthOfBirth").text(
              errormessage.responseJSON.Errors.MonthOfBirth
            );
            $("#error-yearOfBirth").text(
              errormessage.responseJSON.Errors.YearOfBirth
            );
            console.log(errormessage);
          } else {
            toastr.error(errormessage.responseText, "Error occurred");
          }
        },
      });
    } else {
      $.LoadingOverlay("show");
      $.ajax({
        url: "/JobApplication/SubmitTFNDeclare",
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
          window.location = "/JobApplication/BankAccountDetail";
          isComplete1 = true;
        },
        error: function (errormessage) {
          $.LoadingOverlay("hide");
          if (errormessage.hasOwnProperty("responseJSON")) {
            toastr.error("Validate error", "Error occurred");
            $("#error-taxFileNumberOpt").text(
              errormessage.responseJSON.Errors.TaxNumberOption
            );
            $("#error-nameTitle").text(
              errormessage.responseJSON.Errors.NameTitle
            );
            $("#error-surname").text(errormessage.responseJSON.Errors.Surname);
            $("#error-firstName").text(
              errormessage.responseJSON.Errors.FirstName
            );
            $("#error-dayOfBirth").text(
              errormessage.responseJSON.Errors.DayOfBirth
            );
            $("#error-monthOfBirth").text(
              errormessage.responseJSON.Errors.MonthOfBirth
            );
            $("#error-yearOfBirth").text(
              errormessage.responseJSON.Errors.YearOfBirth
            );
            $("#error-homeAddress").text(
              errormessage.responseJSON.Errors.Address
            );
            $("#error-suburb").text(errormessage.responseJSON.Errors.Suburb);
            $("#error-state").text(errormessage.responseJSON.Errors.State);
            $("#error-postCode").text(
              errormessage.responseJSON.Errors.PostCode
            );
            $("#error-paidBasis").text(
              errormessage.responseJSON.Errors.WorkType
            );
            $("#error-residencyStatus").text(
              errormessage.responseJSON.Errors.IsAustraliaResident
            );
            $("#error-claimTaxFree").text(
              errormessage.responseJSON.Errors.IsTaxFee
            );
            $("#error-claimSenior").text(
              errormessage.responseJSON.Errors.IsTaxOffsetForPensioners
            );
            $("#error-claimZone").text(
              errormessage.responseJSON.Errors.IsTaxOffsetForCarer
            );
            $("#error-haveLoanProgram").text(
              errormessage.responseJSON.Errors.IsEducationLoan
            );
            $("#error-haveFinancialSupplement").text(
              errormessage.responseJSON.Errors.IsFinancialSupplementSupport
            );
            $("#error-signatureTFN").text(
              errormessage.responseJSON.Errors.Signature
            );
            $("#error-signaDateTFN").text(
              errormessage.responseJSON.Errors.SignDate
            );
            $("#error-dayOfBirth").text(
              errormessage.responseJSON.Errors.DayOfBirth
            );
            $("#error-monthOfBirth").text(
              errormessage.responseJSON.Errors.MonthOfBirth
            );
            $("#error-yearOfBirth").text(
              errormessage.responseJSON.Errors.YearOfBirth
            );
            console.log(errormessage);
          } else {
            toastr.error(errormessage.responseText, "Error occurred");
          }
        },
      });
    }
  } else {
    toastr.error("Please enter full information!", "Error");
  }
}

function validatePayee() {
  $("#payeeForm").validate({
    rules: {
      taxFileNumberOpt: {
        required: true,
      },
      taxFileNumber: {
        maxlength: 11,

        normalizer: function (value) {
          return $.trim(value);
        },
      },
      surname: {
        required: true,
        maxlength: 19,
        lettersonly: true,
        normalizer: function (value) {
          return $.trim(value);
        },
      },
      firstName: {
        required: true,
        lettersonly: true,
        normalizer: function (value) {
          return $.trim(value);
        },
      },
      nameTitle: {
        required: true,
      },
      homeAddress: {
        required: true,
        maxlength: 38,
        lettersonly: true,
        normalizer: function (value) {
          return $.trim(value);
        },
      },
      suburb: {
        required: true,
        maxlength: 19,
        lettersonly: true,
        normalizer: function (value) {
          return $.trim(value);
        },
      },
      state: {
        required: true,
        maxlength: 3,
        normalizer: function (value) {
          return $.trim(value);
        },
      },
      postCode: {
        required: true,
        maxlength: 4,
        digits: true,
        normalizer: function (value) {
          return $.trim(value);
        },
      },
      dayOfBirth: {
        required: true,
        maxlength: 2,
      },
      monthOfBirth: {
        required: true,
        maxlength: 2,
      },
      yearOfBirth: {
        required: true,
        maxlength: 4,
      },
      paidBasis: {
        required: true,
      },
      residencyStatus: {
        required: true,
      },
      IsTaxOffsetForCarer: {
        required: true,
      },
      claimTaxFree: {
        required: true,
      },
      haveLoanProgram: {
        required: true,
      },
      haveFinancialSupplement: {
        required: true,
      },
      claimZone: {
        required: true,
      },
      claimSenior: {
        required: true,
      },
      SignatureTFN: {
        required: true,
      },
      signDateTFN: {
        required: true,
      },
    },

    messages: {
      taxFileNumberOpt: {
        required: "Please select one",
      },
      taxFileNumber: {
        maxlength: "tax file number not too 9 number",
      },
      surname: {
        required: "Surname cannot be empty",
      },
      firstName: {
        required: "Max student number cannot be empty",
      },
      nameTitle: {
        required: "Please select a name title",
      },
      homeAddress: {
        required: "Home address cannot be empty",
      },
      suburb: {
        required: "Suburb/town/locality cannot be empty",
      },
      state: {
        required: "State/territory cannot be empty",
      },
      postCode: {
        required: "PostCode cannot be empty",
      },
      email: {
        required: "Primary e-mail address cannot be empty",
        email: "Email is invalid",
      },
      dayOfBirth: {
        required: "Choose day of birth",
      },
      monthOfBirth: {
        required: "Choose month of birth",
      },
      yearOfBirth: {
        required: "Choose year of birth",
      },
      paidBasis: {
        required: "Please select one",
      },
      residencyStatus: {
        required: "Please select one",
      },
      IsTaxOffsetForCarer: {
        required: "Please select one",
      },
      claimTaxFree: {
        required: "Please select one",
      },
      haveLoanProgram: {
        required: "Please select one",
      },
      haveFinancialSupplement: {
        required: "Please select one",
      },
      claimZone: {
        required: "Please select one",
      },
      claimSenior: {
        required: "Please select one",
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
