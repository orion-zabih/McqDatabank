$('#btnAddUserContact').on('click', function () {

    $.ajax({
        url: "/User/AddNewContactDetail",
        //dataType: "html",
        //contentType: "application/json",
        type: "POST",
        //data: "",
        success: function (result) {
            $('#user-contact-list').append(result);           
            RefreshValidation();
        },
        beforeSend: function () {
            $("#UserContactLoader").fadeIn();
        },
        complete: function () {
            $("#UserContactLoader").fadeOut();
        }

    });

});


function DeleteLocation(callingObj) {
    var location = $(callingObj).parent().parent().attr("id");
    var selector = "#" + location + "";
    $(selector).remove();

    RefreshValidation();
}

function ContactTypeSelectionChanged(objButton) {
    var val = $(objButton).val();
    if (val != "") {
        $(objButton).parent().parent().siblings('[id=contactDiv]').children('[id=contactInput]').find('input:first').removeAttr('disabled');
        $(objButton).parent().parent().siblings('[id=contactDiv]').children('[id=contactInput]').find('input:first').removeAttr('data-val-regex-pattern');
        if (val == "email") {
            $(objButton).parent().parent().siblings('[id=contactDiv]').children('[id=contactInput]').find('input:first').attr('data-val-regex-pattern', "^[a-z0-9_](\.?[a-z0-9_]){1,}@feo.org.fj$")
        }
        else if (val == "mobile" || val == "landline" || val == "fax") {
            $(objButton).parent().parent().siblings('[id=contactDiv]').children('[id=contactInput]').find('input:first').attr('data-val-regex-pattern', "^([0|\+[0-9]{1,5})?([1-9][0-9]{9})$")
        }
        else if (val == "skype") {
            $(objButton).parent().parent().siblings('[id=contactDiv]').children('[id=contactInput]').find('input:first').attr('data-val-regex-pattern', "[a-zA-Z][a-zA-Z0-9\.,\-_]{5,31}")
        }
        $("form").removeData("validator");
        $("form").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse("form");
    }
    else {
        $(objButton).parent().parent().siblings('[id=contactDiv]').children('[id=contactInput]').find('input:first').attr('disabled', 'disabled');
    }
}

function PopulateConstituencies(election_list)
{
    var token = $('input[name="__RequestVerificationToken"]').val();
    //console.log(token);
    //var value = $(caller).val();
    //console.log(value);
    $.ajax({
        url: "/User/PopulateConstituencies",
        dataType: "html",
        type: "POST",
        data: { __RequestVerificationToken: token, arg: election_list },
        async: false,
        success: function (result) {

            var items = $.parseJSON($.parseJSON(result));
            //console.log(items);
           
            //$("#ConstituencyPanel").children().remove();
            if ($("#ConstituencyPanel").children().length != 0) {
                $("#ConstituencyPanel").children().animo({ animation: ['fadeOutRight'], duration: .5 }, function () {
                    $("#ConstituencyPanel").children().remove();
                    $("#ConstituencyPanel").append($(items)[0].constsPanel);//.animo({ animation: ['fadeInLeftBig'], duration: 10 });
                });
            }
            else {
                $("#ConstituencyPanel").append($(items)[0].constsPanel);
            }
            
            
            
        }

    });
}
//var arr = $.map($("input[name='SelectedElections']:checked"), function (e, i) {
//    return +e.value;
//});
$("#ElectionsPanel").on('change', "input[name='SelectedElections']", function () {
    //console.log("firedevt");
    $("#ConstituencyPanel").children().animo({ animation: ['fadeOutRight'], duration: .5 }, function () {
        $("#ConstituencyPanel").children().remove();
    });
    
   var arr = $.map($("input[name='SelectedElections']:checked"), function (e, i) {
        return +e.value;
    });
   if (arr.length != 0) {
       PopulateConstituencies(arr);
   }
    
});


function PopulateLevelDetails(caller) {
    $("#ConstituencyPanel").children().animo({ animation: ['fadeOutRight'], duration: .5 }, function () {
        $("#ConstituencyPanel").children().remove();
    });
    var token = $('input[name="__RequestVerificationToken"]').val();
    //console.log(token);
    var value = $(caller).val();
    //console.log(value);
    $.ajax({
        url: "/User/PopulateElectionEvents",
        dataType: "html",
        type: "POST",
        data: { __RequestVerificationToken: token, arg: value },
        async: false,
        success: function (result) {

            var items = $.parseJSON($.parseJSON(result));
            //console.log(items);

            

            //$("#ConstituencyPanel").children().remove();
            $("#ElectionsPanel").children().animo({ animation: ['fadeOutRight'], duration: .5 }, function () {
                $("#ElectionsPanel").children().remove();
                $("#ElectionsPanel").append($(items)[0].constsPanel);//.animo({ animation: ['fadeInLeftBig'], duration: 10 });
            });




        }

    });
}




function GetLevelCodes(type, code) {
    var token = $('input[name="__RequestVerificationToken"]').val();
    //console.log(token);

    //console.log(value);
    $.ajax({
        url: "/User/GetLocationCodes",
        dataType: "html",
        type: "POST",
        async:false,
        data: {
            __RequestVerificationToken: token,
            LocationType: type,
            ParentLocationCode: code
        },
        success: function (result) {

            var items = $.parseJSON($.parseJSON(result));
            //console.log(items);
            var options = "<option style='text-transform:capitalize;' value=''>-- Select " + type + "--</option>";
           
            $("#LocationCodes_" + type ).children().remove();
            $(items.list).each(function () {
                options = options + "<option value='" + $(this)[0].Value + "'>" + $(this)[0].Text + "</option>";

            });
            //console.log(options);
            //console.log("#LocationCodes_" + type  + $("#LocationCodes_" + type ).length);
            $("#LocationCodes_" + type ).prepend($(options));
      

        }

    });
}

function submitUserOnClick(objButton) {

    var form = $("#userForm");
    var validator = form.data('validator');
    validator.settings.ignore = "";
    var container = $("div .row.panel-heading.bg-system");
    if ($("#userForm").valid()) {
        $(container).siblings('div .msg-block').remove();
        submitPrompt(objButton);
            //$.ajax({
            //    url: "/Lookup/GetEvrDataNameDoB",
            //    type: 'POST',
            //    data: { evrNumber: $('#User_CITIZEN_NUMBER').val() },
            //    success: function (result) {
            //        var data = JSON.parse(result.Data);

            //        if (data != null) {
            //            if (data != "") {
            //                //alert(data.Status);
            //                if (data.Citizen_Status != "verified") {
            //                    sweetAlert("Error", "CNIC number not verified, please enter valid CNIC number", "error");

            //                }
            //                else {
            //                    submitPrompt(objButton);
            //                }

            //            }
            //        }
            


            //    },
            //    error: function (xhr) {

            //        sweetAlert("Error", "CNIC number not verified, please enter valid CNIC number", "error");

            //    },
            //    beforeSend: function () {
            //        //$('#attrName').val("");
            //        //$('#attrDob').val("");

            //    }
            //});


        
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
        $("#userForm").submit();
    });
}

$(document).ready(function () {
    

});

