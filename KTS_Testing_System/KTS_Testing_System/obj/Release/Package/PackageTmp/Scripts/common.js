

////////////// GLOBAL EVENT HANDLER FOR AJAX ERRORS FROM MASTER ERRORS FILE ///////////////////
/*       
$(document).ajaxError(function (e, jqxhr, settings, exception) {
          e.stopPropagation();
          if (jqxhr != null)
          {
              var x = $.parseJSON(jqxhr.responseText);
              //console.log(x);
              swal({
                  title: "Error",
                  text: x.error,
                  type: x.type,
                  closeOnConfirm: true,
              });
          }
              
      });
*/


$("form").each(function () {
    //<-- Should return all input elements in that specific form.
});


$.validator.setDefaults({
    showErrors: function (errorMap, errorList) {
        $.each(this.validElements(), function (i, v) {
            /*if ($(v).hasClass("text-box") && $(v).val() != '') {
                $(v).removeClass("invalid");
                $(v).addClass("Validated");
                $(v).tooltip('destroy');
            }*/
            $(v).tooltip('destroy');
            $(v.element).removeClass("validationFail"); //RESET BACKGROUND OF INPUT CONTROL TO NORMAL
            $(v).removeClass("validationFail"); //RESET BACKGROUND OF INPUT CONTROL TO NORMAL
        });

      
        
            

        //////////var container = $("div .row.panel-heading.bg-system");
        //////////var list = "<div class='msg-block alert alert-danger alert-dismissible' role='alert' style='margin:10px;'>"+
        //////////           "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button>"+
        //////////           "<div class='validation-summary-errors'><ul>";

        /*if (list && list.length && validator.errorList.length) {
            list.empty();
            container.addClass("validation-summary-errors")
              .removeClass("validation-summary-valid");

            //$.each(validator.errorList, function () {
            //    $("<li />").html(this.message).appendTo(list);
            //});
        }*/

        $.each(errorList, function (i, v) {
            //console.log(errorList);
            //$(v.element).addClass("invalid");

            //$("<li />").html(v.message).appendTo(list);
            //$(list).append("<li>" + v.message + "</li>");
            

            //$(v.element).focus();
            $(v.element).addClass("validationFail"); //SET BACKGROUND OF INPUT CONTROL TO RED
            $(v.element).tooltip({ title: v.message, placement: 'bottom' });
            //////////list += "<li>" + v.message + "</li>";
        });
        //////////list += "</ul></div></div>";

        //container.append(list);
        //////////$(list).insertAfter($(container));

        //this.defaultShowErrors();
    }
});



function RefreshValidation() {
    $("form").removeData("validator");
    $("form").removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse("form");
}


$('.text-box').change(function (e) {
    RefreshValidation();
});

function AddEventToDynamicButton(id) {
    var FileRowSelect = "#" + id;
    id = id.replace("DocumentImage", "Filename")
    var FileNameRowSelect = "#" + id;
    var input = $(FileRowSelect),
    label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
    $(FileNameRowSelect).val(label);
}
function DeleteLocationNew(callingObj, level) {
    var x = "";
    if (level == "2")
        $(callingObj).parent().parent().remove();
    if (level == "3")
        $(callingObj).parent().parent().parent().remove();
    if (level == "4")
        $(callingObj).parent().parent().parent().parent().remove();
}

function handleFileSelect(evt) {


   

    if (!window.File || !window.FileReader || !window.FileList || !window.Blob) {
        alert('The File APIs are not fully supported in this browser.');
        return;
    }

    
    var dropDownSelector = $(evt).attr('id').replace("DocumentImage", "DocumentTypeId");
    //console.log(dropDownSelector);
    var selectorValue = $("#" + dropDownSelector + " option:selected").text();

    var selectorID = $("#" + dropDownSelector + " option:selected").val();
    //console.log(selectorValue);

   



    input = evt; //document.getElementById('fileinput');
    if (!input) {
        alert("Um, couldn't find the fileinput element.");
    }
    else if (!input.files) {
        alert("This browser doesn't seem to support the `files` property of file inputs.");
    }
    else if (!input.files[0]) {
        alert("Please select a file before clicking 'Load'");
    }

    //var files = evt.target.files; // FileList object
    var files = evt.files; // FileList object

    // Loop through the FileList and render image files as thumbnails.
    for (var i = 0, f; f = files[i]; i++) {

        // Only process image files.
        if (!f.type.match('image.*')) {
            continue;
        }

        var reader = new FileReader();

        // Closure to capture the file information.
        reader.onload = (function (theFile) {
            return function (e) {
                // Render thumbnail.

                //METHOD 1
                //var span = document.createElement('span');
                //span.innerHTML = ['<img class="thumb" src="', e.target.result,'" title="', escape(theFile.name), '"/>'].join('');
                //document.getElementById('list').insertBefore(span, null);

                //METHOD 2
                //$(input).siblings("#image").src = e.target.result;

                //METHOD 2
                $(input).siblings("#image").attr("src", e.target.result);



                //if (selectorValue == "Application Form") {
                if (selectorID == "16") {
                   
                    var newImage = $("#target").attr("src", e.target.result);
                    var uid = "#" + dropDownSelector.replace("__DocumentTypeId", "").replace("candidateImagesCollection_DIV_", "");
                    $(uid).siblings().each(function()
                    {
                        var SiblingUID = $(this).attr("id");
                        var Selector = "#candidateImagesCollection_DIV_"+SiblingUID+"__DocumentTypeId";
                        //if ($(Selector + " option:selected").html() == "Application Form" || $("#"+ SiblingUID + " #pSignature").length > 0 || $("#"+SiblingUID + " #pPhotograph").length > 0)
                        if ($(Selector + " option:selected").val() == "16" || $("#" + SiblingUID + " #pSignature").length > 0 || $("#" + SiblingUID + " #pPhotograph").length > 0)
                        {
                            $("#"+SiblingUID).remove();
                                                     
                        }

                       
                    })
                    //$(uid).siblings().remove();
                    //var src = $("#target");//.attr("src");
                    //console.log($(evt).attr('id').replace("DocumentImage", "DocumentTypeId"));

                    initJcrop();
                 
                  
                  
                    $("#SourceID").val($(input).attr("id"));
                    //console.log('image added');

                }
                //$(input).parent().nextAll("div").children("#image").attr("src", e.target.result);
            };
        })(f);

        // Read in the image file as a data URL.
        reader.readAsDataURL(f);
    }
}


function LoadUserInfo(_userID)
{

    //var tempSrc = $("#ProfilePicture").attr("src");
    $.ajax({
        url: "/Lookup/getUserPhotograph",
        dataType: "html",
        type: "POST",
        data: {  id :_userID},
        success: function (result) {
            if (result != "")
            {
                $("#ProfilePicture").attr("src", "data:image/jpg;base64," + result);
            }
            else 
            {
                $("#ProfilePicture").attr("src", "../Content/images/defaultUserImage.png");
            }
           
       
       
        }

    });
}
$(document).ready(function () {
    var url = window.location.href;
    if (url == window.location.protocol + "//" + window.location.host + "/")
    {
        url = url + "Home/Index";
    }
    var substr = url.split('/');
    var urlController = substr[substr.length - 2];
    var urlaspx = substr[substr.length - 1];
    var curr_href = "/" + urlController + "/" + urlaspx;
    curr_href = decodeURI(curr_href);
    var els = document.querySelectorAll("a[href='" + curr_href + "']");
    $('#mainNav').find('.active').removeClass('active');
    for (var i = 0, l = els.length; i < l; i++) {
        var el = els[i];
       
        $(el).parent().addClass('active');
        var prnts = $(el).parent().parents();
        for (var j = 0; j < prnts.length; j++) {
            var elparent = prnts[j];
            if(elparent.tagName=="LI")
            {
               
                var hasProp = $(elparent).hasOwnProperty("aria-expanded");
                if (hasProp == true)
                {
                    $(elparent).first().prop("aria-expanded", "true");
                }
                
            }
            else if(elparent.tagName=="UL")
            {
                
                var hasProp = $(elparent).hasOwnProperty("aria-expanded");
                if (hasProp == true)
                {
                    $(elparent).prop("aria-expanded", "true");
                    
                }
                var hasClass = $(elparent).hasClass("nav sidebar-subnav collapse");
                if(hasClass==true)
                {
                    $(elparent).prop("class", "nav sidebar-subnav collapse in");
                }
                
            }

        }
        
    }
    
    

});

//function VerifyData(callingObj) {

//    var cnic_no = "";

//    if (callingObj.indexOf('#') == 0)
//    {
//        cnic_no = $(callingObj).val();
//    }
//    else
//    {
//        cnic_no = $(callingObj).data('cnic_no');
//    }
     

//    $.ajax({
//        url: "/Lookup/GetCNICData",
//        type: 'POST',
//        data: { cnic: cnic_no },
//        success: function (result) {

//            var data = result;

//            if (result != null) {
//                if (result != null && result != "") {

//                    //var dob = result.dob;
//                    //var Full_name = result.Full_name;
//                    //var first_names = result.first_names;
//                    //var last_name = result.last_name;
//                    //var Gender = result.Gender;

//                    swal({
//                        title: "<div class='panel-headingModified'>CNIC Verification Data</div>",
//                        customClass: 'swal-wide',                        
//                        text: result,
//                        html: true
//                    },
//                        function () {

//                        //start: for user management only
//                        //    if (callingObj == "#User_CITIZEN_NUMBER") {
//                        //        $("#cnicStatus").val($("#Citizen_Status").val());
                          
                            

//                        //}
//                       // end: for user management only
//                    }

//                    );

                    
//                }
//            }
//            else
//                sweetAlert("Error", "Invalid CNIC Number", "error");
//        },
//        error: function (xhr) {
//            sweetAlert("Error", "Unable to get data against provided CNIC number", "error");
//        },
//        beforeSend: function () { }
//    });
//}

//COMMON METHOD
function Confirm(argFormName, callingObj) {

    //COMMON VARIABLE
    var container = $("div .row.panel-heading.bg-system");
    //    document.getElementById('prStatus').value = $(callingObj).val();
    $('#prStatus').attr('value',$(callingObj).val());

    RefreshValidation();

    //RECHECK VALIDATION
    var form = $(argFormName);
    var validator = form.data('validator');
    /*validator.settings.ignore = "";

    $.each(validator.errorList, function () {
            $("<li />").html(this.message).appendTo(list);
        });*/

    //IF ALL OKAY THEN PROCEED WITH POPUP
    if ($(form).valid()) {

        //UPLOADED FILE VALIDATION
        var isFileSizeCorrect = true;
        var isFileExtensionCorrect = true;
        var FileNames = new Array();
        $("input[type='file']").each(function () {

            //5242880 for 5 mb
            if (this.files[0] != null) {

                //CHK FOR IMAGE SIZE
                if (this.files[0].size > '1048576') {
                    isFileSizeCorrect = false;
                    FileNames.push($(this).val());
                }

                              
                var SelectorID = "#" + $(this).attr("id").replace("DocumentImage", "DocumentTypeId");
                var filetypeDesc = $(SelectorID + " :selected").text();
                //CHECK FOR IMAGE EXTENSION
                var validExtensions = ['jpg', 'jpeg', 'png','pdf']; //array of valid extensions
                var fileName = this.files[0].name;
                var fileNameExt = fileName.substr(fileName.lastIndexOf('.') + 1);
                
                if ($.inArray(fileNameExt, validExtensions) == -1) {
                        isFileExtensionCorrect = false;
                        FileNames.push($(this).val());                       
                }
                if (filetypeDesc == "Application Form" && fileNameExt == "pdf") {
                    isFileExtensionCorrect = false;
                    FileNames.push($(this).val());
                }
               
            }
        });

        //IF FILE VALIDATION PASSES THEN SHOW POPUP AND SUBMIT ELSE SHOW ERROR LIST
        if (isFileSizeCorrect == true && isFileExtensionCorrect == true) {

            //IF NO ERRORS FOUND THEN HIDE ERROR SUMMARY (GENERATED PREVIOUSLY)
            $(container).siblings('div .msg-block').remove();

            //SHOW POPUP
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
       $(argFormName).submit();
   });
        }
        else {

            var AllFileNames = "<ul>";
            for (var o in FileNames) {
                if (AllFileNames == null) {
                    AllFileNames = "<li>" + FileNames[o] + "</li>";
                }
                else {
                    AllFileNames = AllFileNames + "<li>" + FileNames[o] + "</li>";
                }
            }
            AllFileNames = AllFileNames + "</ul>"
            swal({
                type: "error",
                title: null,
                text: "Following images either have size bigger than 1 MB or image format is not jpeg or png <br/><br/>" + AllFileNames + " .",
                html: true
            });
            //sweetAlert("Error", "Document(s) " + AllFileNames + " have size bigger than 2 MB. Please select a document of smaller size.", "error");
        }

    }
    else {

        //CLIENT SIDE VALIDATION SUMMARY
        
        var list = "<div class='msg-block alert alert-danger alert-dismissible' role='alert' style='margin:10px;'>"+
                       "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button>"+
                       "<div class='validation-summary-errors'><ul>";
        
        /*$.each(errorList, function (i, v) {
            //console.log(errorList);
            //$(v.element).addClass("invalid");

            //$("<li />").html(v.message).appendTo(list);
            //$(list).append("<li>" + v.message + "</li>");


            //$(v.element).focus();
            $(v.element).addClass("validationFail"); //SET BACKGROUND OF INPUT CONTROL TO RED
            $(v.element).tooltip({ title: v.message, placement: 'bottom' });
            //////////list += "<li>" + v.message + "</li>";
        });*/
        
        //ITERATE THROUGH ERROR LIST AND CONCATINATE IN VALIDATION SUMMARY
        $.each(validator.errorList, function () {
            list += "<li>" + this.message + "</li>";
        });

        list += "</ul></div></div>";

        if($(container).siblings('div .msg-block').length >0) {
            $(container).siblings('div .msg-block').replaceWith(list);
        }
        else {
            $(list).insertAfter($(container));
        }
        
    }


    
}


function JustConfirmOnA(e) {

    e.preventDefault();

    swal({
        title: 'Are you sure?',
        text: 'Please confirm data entered is correct!',
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#23b7e5',
        confirmButtonText: 'Submit',
        closeOnConfirm: true
    }, function () {
        $(this).trigger(e);
    })
}


// ################# SERVER SENT EVENTS #################
$(document).ready(function () {
    //window.addEventListener("DOMContentLoaded", callSSE, false);
});
function callSSE() {
    var source = new EventSource('/Lookup/ServerSentEvents');
    //var ul = document.getElementById("messages");
    source.onmessage = function (e) {
        //var li = document.createElement("li");
        //var returnedItem = JSON.parse(e.data)
        //li.textContent = returnedItem.message + ' ' + returnedItem.item;
        console.log(JSON.parse(e.data).message);
    }
}




//COMMON METHOD
function GetEVRDataDuplicateCheck(callingObj) {

   
    var evr = "";

    if (callingObj.indexOf('#') == 0) {
        evr = $(callingObj).val();
    }
    else {
        evr = $(callingObj).data('evr');
    }


    $.ajax({
        url: "/Lookup/GetEVRDataDuplicateCheck",
        type: 'POST',
        data: { EVR: evr},
        success: function (result) {           

            var data = result;
            
            if (result != null) {
                if (result != null && result != "") {

                    var dob = result.dob;
                    var Full_name = result.Full_name;
                    var first_names = result.first_names;
                    var last_name = result.last_name;
                    var Gender = result.Gender;

                    swal({
                        title: "<div class='panel-headingModified'>EVR Verification Data</div>",
                        customClass: 'swal-wide',
                        //text: "<b>Name: </b><span style='color:#003A54'>" + Full_name + "</span> </br> <b>Date Of Birth: </b><span style='color:#003A54'>" + dob + "</span>",
                        text: result,
                        html: true
                    },
                    function () {
                        //start: for postal voter only
                        if (callingObj == "#objPostalVoter_Evr_number") {
                            $("#objPostalVoter_Last_name").val($("#last_name").val());
                            $("#objPostalVoter_First_names").val($("#first_names").val());
                            if ($("#Gender").val() == "Male") {
                                $("#objPostalVoter_Salutation").val("mr.");
                            }


                        }
                        //end: for postal voter only
                    }

                    );
                }
            }
            else
                sweetAlert("Error", "Invalid EVR Number", "error");
        },
        error: function (xhr) {
                      sweetAlert("Error", xhr.responseJSON, "error");
        },
        beforeSend: function () { }
    });
}

var slideIndex = 1;
showSlides(slideIndex);

// Next/previous controls
function plusSlides(n) {
    showSlides(slideIndex += n);
}

// Thumbnail image controls
function currentSlide(n) {
    showSlides(slideIndex = n);
}

function showSlides(n) {
    var i;
    var slides = document.getElementsByClassName("mySlides");
    var dots = document.getElementsByClassName("demo");
    //var captionText = document.getElementById("caption");
    if (n > slides.length) { slideIndex = 1 }
    if (n < 1) { slideIndex = slides.length }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }
    slides[slideIndex - 1].style.display = "block";
    //dots[slideIndex - 1].className += " active";
    //captionText.innerHTML = dots[slideIndex - 1].alt;
}