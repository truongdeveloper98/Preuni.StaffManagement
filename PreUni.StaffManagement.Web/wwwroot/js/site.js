// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var inputChangeFlag = false;
var checkAgeForm = true;
var showSideBar = false;

$("#step-parent-consent").addClass("disabled-div");
$(document).ready(function () {
    setFormChange();
    jQuery.validator.addMethod("lettersonly", function (value, element) {
        return this.optional(element) || /^[a-z," "]+$/i.test(value);
    }, "Letters and spaces only please");
    $.ajax({
        url: "/JobApplication/GetTFNDeclareDetail/?jobApplicationId=" + $('#jobApplicationId').val(),
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $.LoadingOverlay("hide");


            if (result.status != 0) {
                var payee = result.data;
                console.log(result.data);
                dayBirth = payee.dayOfBirth;
                monthBirth = payee.monthOfBirth;
                yearBirth = payee.yearOfBirth;
                $("#nameApplicant").val(fullName = payee.firstName + " " + payee.surname);

                $('#tfn-Id').val(payee.id);
                $("input[name=taxFileNumberOpt][value=" + payee.taxNumberOption + "]").prop('checked', true);
                $('#taxFileNumber').val(payee.taxNumber);
                $('#surname').val(payee.surname);
                $('#firstName').val(payee.firstName);
                $('#otherName').val(payee.otherName);
                $('#homeAddress').val(payee.address);
                $('#suburb').val(payee.suburb);
                $('#state').val(payee.state);
                $('#postCode').val(payee.postCode);
                $('#previousFamilyName').val(payee.previousLastName);
                $("input[name=claimSenior][value=" + payee.isTaxOffsetForPensioners + "]").prop('checked', true);
                $('#dayOfBirth').val(payee.dayOfBirth);
                $('#monthOfBirth').val(payee.monthOfBirth);
                $('#yearOfBirth').val(payee.yearOfBirth);

                $("input[name=nameTitle][value=" + payee.nameTitle + "]").prop('checked', true);
                $("input[name=paidBasis][value=" + payee.workType + "]").prop('checked', true);
                $("input[name=residencyStatus][value=" + payee.isAustraliaResident + "]").prop('checked', true);
                $("input[name=claimTaxFree][value=" + payee.isTaxFee + "]").prop('checked', true);
                $("input[name=haveLoanProgram][value=" + payee.isEducationLoan + "]").prop('checked', true);
                $("input[name=haveFinancialSupplement][value=" + payee.isFinancialSupplementSupport + "]").prop('checked', true);
                $("input[name=claimZone][value=" + payee.isTaxOffsetForCarer + "]").prop('checked', true);
                $("#SignatureTFN").val(payee.signature);
                $("#signDateTFN").val(payee.signDate.split("T")[0]);
                if (result.data != null) {
                    // if applicant over 18 then disable button click form parent consent
                    if (checkAge(payee.dayOfBirth, payee.monthOfBirth, payee.yearOfBirth)) {
                        $("#step-parent-consent").addClass("disabled-div");
                        $("#side-parent-step").addClass("disabled-div");
                        checkAgeForm = true;
                    } else {
                        $("#step-parent-consent").removeClass("disabled-div");
                        $("#side-parent-step").removeClass("disabled-div");
                        checkAgeForm = false
                    }
                }
                else {
                    $("#step-parent-consent").addClass("disabled-div");
                    $("#side-parent-step").addClass("disabled-div");
                    checkAgeForm = true;
                }

            }
        },
        error: function (errormessage) {
            toastr.error(errormessage.responseText, "Error occurred");
            $.LoadingOverlay("hide");
        }
    });


    $(window).on("resize", function () {
        if ($(window).width() > 991) {
            hideSideBar()
        }
    });

})


function hideSideBar() {
    if (showSideBar) {
        $('#openSidebarMenu').click();
        showSideBar = false;
    }
}

function clickSidebar() {
    showSideBar = !showSideBar;
}
function setFormChange() {
    $('#content-side input, #content-side select').change(function (e) {
        inputChangeFlag = true;
    });
}
function scollToTop() {
    $("html, body").animate({ scrollTop: 0 }, 1000);
    return false;
}

function activeStepper(item) {
    // content
    $('.step-content').removeClass('current');
    $('#step-content-' + item).addClass('current');

    // icon
    $('.stepper-icon').removeClass('stepper-icon-active');
    $('#stepper-icon-' + item).addClass('stepper-icon-active');

    // title
    $('.stepper-title').removeClass('stepper-title-active');
    $('#stepper-title-' + item).addClass('stepper-title-active');
}




function checkAge(day, month, year) {
    let date = new Date();
    let currentYear = date.getFullYear();
    let currentMonth = date.getMonth() + 1;
    let currentDay = date.getDate();
    let age = currentYear - parseInt(year);
    console.log(currentDay, currentMonth, currentYear, parseInt(age));
    if (parseInt(age) > 18) return true;
    else if (parseInt(age) < 18) return false;
    else {
        if (currentMonth < parseInt(month) || currentMonth == parseInt(month) && parseInt(day) > currentDay) return false;
        return true;
    }
}


function isChange() {
    setFormChange()
    if (inputChangeFlag) {
        if (confirm("Form data has changed. Do you want to discard this changes") == false) {
            return false;
        }
    }
}

function getPayeeStep(sideBarClick = false) {
    if (sideBarClick) {
        hideSideBar();
    }
    if (inputChangeFlag) {
        if (confirm("Form data has changed. Do you want to discard this changes") == false) {
            return false;
        }
    }

    setFormChange();
    inputChangeFlag = false;
    window.location = "/JobApplication/TFNDeclare";
}
function getBankAccountStep(sideBarClick = false) {
    if (sideBarClick) {
        hideSideBar();
    }
    if (inputChangeFlag) {
        if (confirm("Form data has changed. Do you want to discard this changes") == false) {
            return false;
        }
    }

   
    inputChangeFlag = false;
    window.location = "/JobApplication/BankAccountDetail";
}
function getSuperannuationStep(sideBarClick = false) {
    if (sideBarClick) {
        hideSideBar();
    }
    if (inputChangeFlag) {
        if (confirm("Form data has changed. Do you want to discard this changes") == false) {
            return false;
        }
    }

  
    inputChangeFlag = false;
    window.location = "/JobApplication/SuperAnnuation";
}

function getParentConsentStep(sideBarClick = false) {
    if (sideBarClick) {
        hideSideBar();
    }
    if (inputChangeFlag) {
        if (confirm("Form data has changed. Do you want to discard this changes") == false) {
            return false;
        }
    }

    
    inputChangeFlag = false;
    window.location = "/JobApplication/ParentConsent";
}

function getIdentityProoImageStep(sideBarClick = false) {
    if (sideBarClick) {
        hideSideBar();
    }
    if (inputChangeFlag) {
        if (confirm("Form data has changed. Do you want to discard this changes") == false) {
            return false;
        }
    }

   
    inputChangeFlag = false;
    window.location = "/JobApplication/IdentityProoImage";
}

function getBackStep() {
    if (checkAgeForm) {
        window.location = "/JobApplication/SuperAnnuation";
    }
    else {
        window.location = "/JobApplication/ParentConsent";
    }
}