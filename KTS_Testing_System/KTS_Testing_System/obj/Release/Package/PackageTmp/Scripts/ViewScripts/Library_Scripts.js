
function submitDepartmentOnClick(objButton) {

    var form = $("#departmentForm");
    var validator = form.data('validator');
    validator.settings.ignore = "";
    var container = $("div .row.panel-heading.bg-system");
    if ($("#departmentForm").valid()) {
        $(container).siblings('div .msg-block').remove();
        submitPromptDepartment(objButton);        

    }
    else {

        //CLIENT SIDE VALIDATION SUMMARY
        var list = "<div class='msg-block alert alert-danger alert-dismissible' role='alert' style='margin:10px;'>" +
            "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button>" +
            "<div class='validation-summary-errors'><ul>";

        //ITERATE THROUGH ERROR LIST AND CONCATINATE IN VALIDATION SUMMARY
        $.each(validator.errorList, function () {
            list += "<li>" + this.message + "</li>";
        });

        list += "</ul></div></div>";

        if ($(container).siblings('div .msg-block').length > 0) {
            $(container).siblings('div .msg-block').replaceWith(list);
        }
        else {
            $(list).insertAfter($(container));
        }
    }

}
function submitPromptDepartment(objButton) {
    swal({
        title: "Are you sure?",
        text: "Please confirm data entered is correct!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#23b7e5",
        confirmButtonText: "Submit",
        closeOnConfirm: true
    },
        function () {
            //document.getElementById('prStatus').value = objButton.value;
            $("#departmentForm").submit();
        });
}


function submitLevelOnClick(objButton) {

    var form = $("#levelForm");
    var validator = form.data('validator');
    validator.settings.ignore = "";
    var container = $("div .row.panel-heading.bg-system");
    if ($("#levelForm").valid()) {
        $(container).siblings('div .msg-block').remove();
        submitPromptLevel(objButton);

    }
    else {

        //CLIENT SIDE VALIDATION SUMMARY
        var list = "<div class='msg-block alert alert-danger alert-dismissible' role='alert' style='margin:10px;'>" +
            "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button>" +
            "<div class='validation-summary-errors'><ul>";

        //ITERATE THROUGH ERROR LIST AND CONCATINATE IN VALIDATION SUMMARY
        $.each(validator.errorList, function () {
            list += "<li>" + this.message + "</li>";
        });

        list += "</ul></div></div>";

        if ($(container).siblings('div .msg-block').length > 0) {
            $(container).siblings('div .msg-block').replaceWith(list);
        }
        else {
            $(list).insertAfter($(container));
        }
    }

}
function submitPromptLevel(objButton) {
    swal({
        title: "Are you sure?",
        text: "Please confirm data entered is correct!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#23b7e5",
        confirmButtonText: "Submit",
        closeOnConfirm: true
    },
        function () {
            //document.getElementById('prStatus').value = objButton.value;
            $("#levelForm").submit();
        });
}

function submitSubjectOnClick(objButton) {

    var form = $("#subjectForm");
    var validator = form.data('validator');
    validator.settings.ignore = "";
    var container = $("div .row.panel-heading.bg-system");
    if ($("#subjectForm").valid()) {
        $(container).siblings('div .msg-block').remove();
        submitPromptSubject(objButton);

    }
    else {

        //CLIENT SIDE VALIDATION SUMMARY
        var list = "<div class='msg-block alert alert-danger alert-dismissible' role='alert' style='margin:10px;'>" +
            "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button>" +
            "<div class='validation-summary-errors'><ul>";

        //ITERATE THROUGH ERROR LIST AND CONCATINATE IN VALIDATION SUMMARY
        $.each(validator.errorList, function () {
            list += "<li>" + this.message + "</li>";
        });

        list += "</ul></div></div>";

        if ($(container).siblings('div .msg-block').length > 0) {
            $(container).siblings('div .msg-block').replaceWith(list);
        }
        else {
            $(list).insertAfter($(container));
        }
    }

}
function submitPromptSubject(objButton) {
    swal({
        title: "Are you sure?",
        text: "Please confirm data entered is correct!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#23b7e5",
        confirmButtonText: "Submit",
        closeOnConfirm: true
    },
        function () {
            //document.getElementById('prStatus').value = objButton.value;
            $("#subjectForm").submit();
        });
}
