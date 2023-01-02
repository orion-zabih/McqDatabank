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

function PopulateLevelDetails(caller)
{
    var token = $('input[name="__RequestVerificationToken"]').val();
    //console.log(token);
    var value = $(caller).val();
    //console.log(value);
    $.ajax({
        url: "/User/PopulateLocationTypes",
        dataType: "html",
        type: "POST",
        data: { __RequestVerificationToken: token, arg: value },
        async: false,
        success: function (result) {

            var items = $.parseJSON($.parseJSON(result));
            //console.log(items);
            var options = "<option style=' text-transform:capitalize;' value=''>-- Select " + value + "--</option>";

            $("#LocationCodes_" + value ).children().remove();
            $(items.list).each(function () {
                options = options + "<option value=" + $(this)[0].code + ">" + $(this)[0].desc + "</option>";

            });
            $("#LocationCodes_" + value ).append(options);

            //$("#RolesPanel").children().remove();
            $("#RolesPanel").children().animo({ animation: ['fadeOutRight'], duration: .5 }, function () {
                $("#RolesPanel").children().remove();
                $("#RolesPanel").append($(items)[0].rolesPanel);//.animo({ animation: ['fadeInLeftBig'], duration: 10 });
            });
            
            //$("#RolesPanel").children().remove();
            //$("#RolesPanel").hide();
            
            //"$('#demo2').animo({animation: ['tada', 'bounce'], duration: 2});"
            

          //  console.log(items);
            
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
        async: false,
        data: {
            __RequestVerificationToken: token,
            LocationType: type,
            ParentLocationCode: code
        },
        success: function (result) {

            var items = $.parseJSON($.parseJSON(result));
            //console.log(items);
            var options = "<option style='text-transform:capitalize;' value=''>-- Select " + type + "--</option>";

            $("#LocationCodes_" + type).children().remove();
            $(items.list).each(function () {
                options = options + "<option value='" + $(this)[0].Value + "'>" + $(this)[0].Text + "</option>";

            });
            //console.log(options);
            //console.log("#LocationCodes_" + type  + $("#LocationCodes_" + type ).length);
            $("#LocationCodes_" + type).prepend($(options));


        }

    });
}

//function ElectionEventSelectionChanged(objButton) {
//    var val = $(objButton).val();
//    var constt = $("#selectedConstType").val();
//    if (val != "") {
//        var token = $('input[name="__RequestVerificationToken"]').val();
//        //console.log(token);

//        //console.log(value);
//        $.ajax({
//            url: "/Lookup/PopulateConstuenciesByElection",
//            dataType: "html",
//            type: "POST",
//            data: { __RequestVerificationToken: token, ElectionEventId: val, ConstType: constt },
//            async: false,
//            success: function (result) {

//                var items = $.parseJSON($.parseJSON(result));
//                //console.log(items);
//                if (constt == "na") {
//                    var options = "<option style=' text-transform:capitalize;' value=''>-- Select NA Constituency --</option>";

//                    $("#SelectedNaConstituency").children().remove();
//                    $(items.list).each(function () {
//                        options = options + "<option value=" + $(this)[0].const_id + ">" + $(this)[0].const_no + "</option>";

//                    });
//                    $("#SelectedNaConstituency").append(options);
//                }
//                else if (constt == "pa") {
//                    var options = "<option style=' text-transform:capitalize;' value=''>-- Select PA Constituency --</option>";

//                    $("#SelectedPaConstituency").children().remove();
//                    $(items.list).each(function () {
//                        options = options + "<option value=" + $(this)[0].const_id + ">" + $(this)[0].const_no + "</option>";

//                    });
//                    $("#SelectedPaConstituency").append(options);
//                }




//            }

//        });

//    }

//}
function ElectionEventSelectionChanged(objButton) {
    var val = $(objButton).val();
    if (val != "") {
        var token = $('input[name="__RequestVerificationToken"]').val();
        //console.log(token);

        //console.log(value);
        $.ajax({
            url: "/Lookup/PopulateProvincesByElection",
            dataType: "html",
            type: "POST",
            data: { __RequestVerificationToken: token, ElectionEventId: val },
            async: false,
            success: function (result) {

                var items = $.parseJSON($.parseJSON(result));
                //console.log(items);
                var options = "<option style=' text-transform:capitalize;' value=''>-- Select Province --</option>";

                $("#SelectedProvince").children().remove();
                $(items.list).each(function () {
                    options = options + "<option value=" + $(this)[0].prov_code + ">" + $(this)[0].prov_name + "</option>";

                });
                $("#SelectedProvince").append(options);




            }

        });

    }

}

//function ProvinceSelectionChanged(objButton) {
//    var val = $(objButton).val();
//    if (val != "") {
//        var token = $('input[name="__RequestVerificationToken"]').val();
//        //console.log(token);

//        //console.log(value);
//        $.ajax({
//            url: "/Lookup/PopulateDistrictByProvince",
//            dataType: "html",
//            type: "POST",
//            data: { __RequestVerificationToken: token, ProvCode: val },
//            async: false,
//            success: function (result) {

//                var items = $.parseJSON($.parseJSON(result));
//                //console.log(items);
//                var options = "<option style=' text-transform:capitalize;' value=''>-- Select District --</option>";

//                $("#SelectedDistrict").children().remove();
//                $(items.list).each(function () {
//                    options = options + "<option value=" + $(this)[0].distt_code + ">" + $(this)[0].distt_name + "</option>";

//                });
//                $("#SelectedDistrict").append(options);



//            }

//        });

//    }

//}
function ProvinceSelectionChanged(objButton) {
    var val = $(objButton).val();
    var elec = $("#SelectedElection").val();
    //if (val != "")
    {
        var token = $('input[name="__RequestVerificationToken"]').val();
        //console.log(token);

        //console.log(value);
        
            $.ajax({
                url: "/Lookup/PopulateConstuenciesByProv",
                dataType: "html",
                type: "POST",
                data: { __RequestVerificationToken: token, ProvCode: val, ConstType:"na", ElectionEventId: elec },
                async: false,
                success: function (result) {

                    var items = $.parseJSON($.parseJSON(result));
                    //console.log(items);
                    var options = "<option style=' text-transform:capitalize;' value=''>-- Select NA Constituency --</option>";

                    $("#SelectedNaConstituency").children().remove();
                    $(items.list).each(function () {
                        options = options + "<option value=" + $(this)[0].const_id + ">" + $(this)[0].const_no + "</option>";

                    });
                    $("#SelectedNaConstituency").append(options);




                }

            });

        
            $.ajax({
                url: "/Lookup/PopulateConstuenciesByProv",
                dataType: "html",
                type: "POST",
                data: { __RequestVerificationToken: token, ProvCode: val, ConstType: "pa", ElectionEventId: elec },
                async: false,
                success: function (result) {

                    var items = $.parseJSON($.parseJSON(result));
                    //console.log(items);
                    var options = "<option style=' text-transform:capitalize;' value=''>-- Select PA Constituency --</option>";

                    $("#SelectedPaConstituency").children().remove();
                    $(items.list).each(function () {
                        options = options + "<option value=" + $(this)[0].const_id + ">" + $(this)[0].const_no + "</option>";

                    });
                    $("#SelectedPaConstituency").append(options);




                }

            });

        
        
    }

}
//function DistrictSelectionChanged(objButton) {
//    var val = $(objButton).val();
//    var elec = $("#SelectedElection").val();
//    var constt = $("#selectedConstType").val();
//    alert(constt);
//    if (val != "") {
//        var token = $('input[name="__RequestVerificationToken"]').val();
//        //console.log(token);

//        //console.log(value);
//        $.ajax({
//            url: "/Lookup/PopulateConstuenciesByDistrict",
//            dataType: "html",
//            type: "POST",
//            data: { __RequestVerificationToken: token, DisttCode: val, ConstType: constt, ElectionEventId:elec },
//            async: false,
//            success: function (result) {

//                var items = $.parseJSON($.parseJSON(result));
//                //console.log(items);
//                if (constt == "na") {
//                    var options = "<option style=' text-transform:capitalize;' value=''>-- Select NA Constituency --</option>";

//                    $("#SelectedNaConstituency").children().remove();
//                    $(items.list).each(function () {
//                        options = options + "<option value=" + $(this)[0].const_id + ">" + $(this)[0].const_no + "</option>";

//                    });
//                    $("#SelectedNaConstituency").append(options);
//                }
//                else if (constt == "pa") {
//                    var options = "<option style=' text-transform:capitalize;' value=''>-- Select PA Constituency --</option>";

//                    $("#SelectedPaConstituency").children().remove();
//                    $(items.list).each(function () {
//                        options = options + "<option value=" + $(this)[0].const_id + ">" + $(this)[0].const_no + "</option>";

//                    });
//                    $("#SelectedPaConstituency").append(options);
//                }




//            }

//        });

//    }

//}

function ConstituencySelectionChanged(objButton) {
    var UserType = $("#User_USER_TYPE").val();
    if (UserType != "ro")
    {
        var val = $(objButton).val();
        var constt = $("#selectedConstType").val();
        if (val != "") {
            var token = $('input[name="__RequestVerificationToken"]').val();
            //console.log(token);

            //console.log(value);
            $.ajax({
                url: "/Lookup/PopulatePsByConstituency",
                dataType: "html",
                type: "POST",
                data: { __RequestVerificationToken: token, ConstId: val, ConstType: constt },
                async: false,
                success: function (result) {

                    var items = $.parseJSON($.parseJSON(result));
                    //console.log(items);
                    var options = "<option style=' text-transform:capitalize;' value=''>-- Select Polling Station --</option>";

                    $("#SelectedPollingStation").children().remove();
                    $(items.list).each(function () {
                        options = options + "<option value=" + $(this)[0].ps_id + ">" + $(this)[0].ps_name + "</option>";

                    });
                    $("#SelectedPollingStation").append(options);



                }

            });

        }
    }

    

}
function UserTypeSelectionChanged(objButton, UserType) {
    var value = $(objButton).val();
    var consttype = $("#selectedConstType").val();
    if (value != null && value.length!=0)
    {
        var token = $('input[name="__RequestVerificationToken"]').val();

        $.ajax({
            url: "/OpsUser/PopulateRoles",
            dataType: "html",
            type: "POST",
            data: { __RequestVerificationToken: token, arg: value },
            async: false,
            success: function (result) {

                var items = $.parseJSON($.parseJSON(result));
                //console.log(items);

                //$("#RolesPanel").children().remove();
                $("#RolesPanel").children().animo({ animation: ['fadeOutRight'], duration: .5 }, function () {
                    $("#RolesPanel").children().remove();
                    $("#RolesPanel").append($(items)[0].rolesPanel);//.animo({ animation: ['fadeInLeftBig'], duration: 10 });
                });

                //$("#RolesPanel").children().remove();
                //$("#RolesPanel").hide();

                //"$('#demo2').animo({animation: ['tada', 'bounce'], duration: 2});"


                //  console.log(items);

            }

        });

        if (value == "ro") {
            $("#SelectedPollingStation").removeProp("disabled");
            $("#SelectedProvince").removeProp("disabled");
            $("#NaConstituency").removeProp("disabled");
            $("#PaConstituency").removeProp("disabled");
            if (consttype == "na")
                $("#SelectedNaConstituency").removeProp("disabled");
            if (consttype == "pa")
                $("#SelectedPaConstituency").removeProp("disabled");
                
        }
        else if (value == "cso") {

             $("#SelectedPollingStation").attr('disabled', 'true');
             $("#SelectedProvince").attr('disabled', 'true');
             $("#NaConstituency").attr('disabled', 'true');
             $("#SelectedNaConstituency").attr('disabled', 'true');
             $("#PaConstituency").attr('disabled', 'true');
             $("#SelectedPaConstituency").attr('disabled', 'true');
         }
    }
        
    

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
        document.getElementById('prStatus').value = objButton.value;
        $("#userForm").submit();
    });
}

$(document).ready(function () {

    $('#User_CITIZEN_NUMBER').mask('99999-9999999-9');
    $("#User_First_names,#User_Last_name").keypress(function (event) {
            var inputValue = event.charCode;
            if (!(inputValue >= 65 && inputValue <= 122) && (inputValue != 32 && inputValue != 0)) {
                event.preventDefault();
            }
        });
        //$("#objPostalVoter_Postal_address_postcode").keypress(function (event) {
        //    var inputValue = (event.which) ? event.which : event.keyCode;
        //    if (inputValue != 46 && inputValue > 31
        //        && (inputValue < 48 || inputValue > 57))
        //        return false;

        //    return true;
        //});
       
  
    $("input[type=radio][id=User_Level_name]").on('change', function () {
       
        
      
        var div = $(this).parent().parent().parent();
        $("#LocationCodes_" + $(this).val()).attr("disabled", false);
        if ($(div).prevAll().length != 0)
        {
            
            var SelectedLocation = $(this).val();//$("input[type=radio][id=User_Level_name]:checked").val();
            
            if ($(this).val() == "constituency")
            {
                var ParentType = $("#LocationCodes_Province").prev().children().find("[id=User_Level_name]").val();
                var ParentVal = $("#LocationCodes_Province").val();
                if (SelectedLocation != ParentType) {
                    
                    if (ParentVal != "") {
                        GetLevelCodes("constituency", ParentVal, PopulateLevelDetails($(this)));
                    }
                    else {
                        PopulateLevelDetails($(this));
                    }
                    
                    
                }
                
            }
            //else if ($(this).val() == "field") {
            //    var ParentType = $("#LocationCodes_area_code").prev().children().find("[id=User_Level_name]").val();
            //    var ParentVal = $("#LocationCodes_area_code").val();
            //    if (SelectedLocation != ParentType) {
            //        if (ParentVal !="")
            //        {
            //            GetLevelCodes("field", ParentVal, PopulateLevelDetails($(this)));
            //        } else {
            //            PopulateLevelDetails($(this));
            //        }
                    
            //    }
            //}
            else if ($(this).val() == "pollingstation") {
                var ParentType = $("#LocationCodes_Constituency").prev().children().find("[id=User_Level_name]").val();
                var ParentVal = $("#LocationCodes_Constituency").val();
                if (SelectedLocation != ParentType) {
                    if (ParentVal != "") {
                        GetLevelCodes("polling station", ParentVal, PopulateLevelDetails($(this)));
                    } else {
                        PopulateLevelDetails($(this));
                    }
                    
                }
            }
            else {
                PopulateLevelDetails($(this));
            }

            $(div).nextAll().each(function () {
               
                //var nam = $(this).attr('name');
                //$(this).children("select").children().remove();
                //$("#" + nam + " option:first").attr('selected', 'selected');
                //$("#" + nam).val('');
                $(this).children("select").attr("disabled", true);
                $(this).children("select").val('');
            });

            $(div).prevAll().each(function () {
                $(this).children("select").attr("disabled", false);
            });
        }
        else if ($(div).nextAll().length != 0)
        {
            PopulateLevelDetails($(this));
            $(div).nextAll().each(function () {
                $(this).children("select").attr("disabled", true);
                $(this).children("select").val('');
            });
        }
        
               

       
    });
    $("input[type=radio][name=RadConstituency]").on('change', function () {
        
        if (this.id == "NaConstituency")
        {
            $('#selectedConstType').val('na');
            $('#SelectedPaConstituency').prop("disabled", true);
            $('#SelectedNaConstituency').removeProp("disabled");
            $("#SelectedPaConstituency").val("");
        }
        else if (this.id == "PaConstituency") {
            $('#selectedConstType').val('pa');
            $('#SelectedNaConstituency').prop("disabled", true);
            $('#SelectedPaConstituency').removeProp("disabled");
            $("#SelectedNaConstituency").val("");
        }
    });
    
    $("#LocationCodes_Province").on('change', function (evt) {
        var SelectedLocation = $("input[type=radio][id=User_Level_name]:checked").val();
        var ParentType = $(this).prev().children().find("[id=User_Level_name]").val();
        var ParentVal = $(this).val();
        //$(this).val(ParentVal);
        if (SelectedLocation != ParentType && ParentVal!="") {
            GetLevelCodes("constituency", ParentVal);
        }
        else {
            var div = $(this).parent();
            $(div).nextAll().each(function () {
                //$(this).children("select").attr("disabled", true);
                $(this).children("select").val('');
            });
        }
       

    });
    

    $("#LocationCodes_Constituency").on('change', function (evt) {
        var SelectedLocation = $("input[type=radio][id=User_Level_name]:checked").val();
        var ParentType = $(this).prev().children().find("[id=User_Level_name]").val();
        var ParentVal = $(this).val();
        if (SelectedLocation != ParentType && ParentVal != "") {
            GetLevelCodes("polling station", ParentVal);
        }
        else {
            var div = $(this).parent();
            $(div).nextAll().each(function () {
                //$(this).children("select").attr("disabled", true);
                $(this).children("select").val('');
            });
        }

    });


    //$("#LocationCodes_field_code").on('change', function (evt) {
    //    var SelectedLocation = $("input[type=radio][id=User_Level_name]:checked").val();
    //    var ParentType = $(this).prev().children().find("[id=User_Level_name]").val();
    //    var ParentVal = $(this).val();
    //    if (SelectedLocation != ParentType && ParentVal != "") {
    //        GetLevelCodes("hub", ParentVal);
    //    }
    //    else {
    //        var div = $(this).parent();
    //        $(div).nextAll().each(function () {
    //            //$(this).children("select").attr("disabled", true);
    //            $(this).children("select").val('');
    //        });
    //    }

    //});

});

