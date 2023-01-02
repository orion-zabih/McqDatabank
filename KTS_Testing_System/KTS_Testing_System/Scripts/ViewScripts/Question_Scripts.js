$('#add-answer').on('click', function () {
    $.get('/Questions/AddAnswersForQuestion').done(function (html) {
        $('#answer-list').append(html);
    });

    RefreshValidation();
});


//$(document).ready(function () {
   
//});

function DeleteLocation(callingObj) {
    var location = $(callingObj).parent().parent().parent().parent().attr("id");
    var selector = "#" + location + "";
    $(selector).remove();

    RefreshValidation();
}


//function RadSelectionChanged(radSelected) {
//    var ConstTypeChecked = $("#" + $(radSelected).attr("id")).attr("value");
//    console.log(ConstTypeChecked);
//}

function RadSelectionChanged(radSelected) {
    $("input[name$='correct_p']").val("false");
    document.getElementById("AnswersForQuestion_DIV_" + radSelected + "__correct_p").value = "true";
    //console.log(radSelected);
}


function submitQuestionOnClick (objButton) {

    var form = $("#questionForm");
    var validator = form.data('validator');
    validator.settings.ignore = "";
    var container = $("div .row.panel-heading.bg-system");
    if ($("#questionForm").valid()) {
        $(container).siblings('div .msg-block').remove();
        submitPrompt(objButton);       
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

function submitPrompt(objButton) {
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
            $("#questionForm").submit();
        });
}

$(document).ready(function () {


});