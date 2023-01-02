

$('#add-office').on('click', function () {
    //$.get('/PartyRegistration/AddNewOfficeDetail').done(function (html) {
    //    $('#office-list').append(html);
    //});
    var Request = new Political_party_offices();
    var jString = "{PartyOffice:" + JSON.stringify(Request) + "}";
    //var jString = "PartyOffice:" +JSON.stringify(Request) ;

    console.log(jString);
    $.ajax({
        url: "/PartyRegistration/AddNewOfficeDetail",
        dataType: "html",
        contentType: "application/json",
        type: "POST",
        data: jString,
        success: function (result) {
            $('#accordionOfficeDetails').append($.parseJSON(result).message);
            sweetAlert("Yayy..", "Office Added Successfully!", "success");
            RefreshValidation();
        }

    });
  
});






function Political_party_offices()
{
    var self = this;
    self.code = $("#PartyOffice_Code").val();
    self.Party_code = $("#Party_Code").val();
    self.Office_type_code = $("#PartyOffice_Office_type_code").val();
    self.Person_in_charge = $("#PartyOffice_Person_in_charge").val();
    self.Address_line_one = $("#PartyOffice_Address_line_one").val();
    self.Address_line_two = $("#PartyOffice_Address_line_two").val();
    self.Postal_address_line_one = $("#PartyOffice_Postal_address_line_one").val();
    self.Postal_address_line_two = $("#PartyOffice_Postal_address_line_two").val();
    self.Party_office_contacts = new Array();
    $('#contact-list tbody tr').each(function () {
        var RowID = $(this).attr("id");
        var Contact = new Party_office_contacts();
        Contact.Contact_type_code = $("#partyOfficeContactsCollection_" + RowID + "__Contact_type_code").val();
        Contact.Contact_data = $("#partyOfficeContactsCollection_" + RowID + "__Contact_data").val();
        self.Party_office_contacts.push(Contact);
       // console.log(self.Party_office_contacts);
    });
    //var list = $()
    //self.Party_office_contacts = $("")
}

function Party_office_contacts()
{
    var self = this;
    self.Contact_type_code = "";
    self.Contact_data = "";
}

$('#add-contact').on('click', function () {
   
    $.ajax({
        url: "/PartyRegistration/AddNewContactDetail",
        dataType: "html",
        contentType: "application/json",
        type: "POST",
        data: "",
        success: function (result) {
            $('#contact-list tbody').append(result);
            //console.log(result);
            RefreshValidation();
        }

    });

});




////////////////////////////////Populate Row from Grid to Form to edit it /////////////////////////////////////////////////
function UpdateRecord(parentUID)
{
    $('#contact-list tbody').children().remove();

    var token = $('input[name="__RequestVerificationToken"]').val();
    var officeCode = parentUID;
    var officeTypeCode = $("#officeType_" + parentUID);
    var Person_in_charge = $("#Person_in_charge_" + parentUID);
    var Address_line_one = $("#Address_line_one_" + parentUID);
    var Address_line_two = $("#Address_line_two_" + parentUID);
    var Postal_address_line_one = $("#Postal_address_line_one_" + parentUID);
    var Postal_address_line_two = $("#Postal_address_line_two_" + parentUID);
    



    $("#PartyOffice_Code").val(officeCode);
    $("#PartyOffice_Office_type_code").val($(officeTypeCode).attr("data-val"));
    $("#PartyOffice_Person_in_charge").val(Person_in_charge.html());
    $("#PartyOffice_Address_line_one").val(Address_line_one.html());
    $("#PartyOffice_Address_line_two").val(Address_line_two.html());
    $("#PartyOffice_Postal_address_line_one").val(Postal_address_line_one.html());
    $("#PartyOffice_Postal_address_line_two").val(Postal_address_line_two.html());
    var contactTypes = "<option value=''>-- Select Contact Type --</option>";
    $.ajax({
        url: "/PartyRegistration/GetContactTypes",
        dataType: "html",
        type: "POST",
        data: { __RequestVerificationToken: token},
        success: function (result) {
            var arr = $.parseJSON(result);
            for (var i = 0; i < arr.length; i++)
            {
                //console.log(arr[i].Text + arr[i].Value);
                contactTypes += "<option value='" + arr[i].Value + "'>" + arr[i].Text + "</option>";
            }
          //  console.log(contactTypes);
            var ArrayOfSelectors = new Array();
            
            $("#partyContacts_" + officeCode + " .row").each(function () {
                

                var id = $(this).attr('id');
                var Type = ($(this).find("[data-val]").attr('data-val'));
                var Value = ($(this).find("[data-val]").html());
               // console.log(id+" "+Type + " " + Value);
                
                var paramID = "partyOfficeContactsCollection_"+id;
                var paramName = "partyOfficeContactsCollection[" + id + "]";

                var tr = "<tr id='" + id + "'>";
                var hidden = "<input name='partyOfficeContactsCollection.Index' value=" + id + " type='hidden'>";
                var SelectHtml = "<td><select class= 'ddlProjectvalue form-control' id='"+paramID+"__Contact_type_code' name='"+paramName+".Contact_type_code'>"+contactTypes+"</select></td>";
              //  console.log(SelectHtml);

                var inputHtml = "<td><input class='form-control text-box single-line' id='" + paramID + "__Contact_data' name='" + paramName + ".Contact_data' type='text' /></td>";
               // console.log(inputHtml);
                var btnDelete = "<td><button type='button' class='btn btn-default' onclick=DeleteLocation('" + id + "')>Delete</button></td>"

                var trEnd = "</tr>";

                var Row = tr + hidden + SelectHtml + inputHtml + btnDelete + trEnd;

              //  console.log(Row);
                $('#contact-list tbody').append(Row);

                $('#' + paramID + "__Contact_type_code").val(Type);
                $('#' + paramID + "__Contact_data").val(Value);

            })
          
        }

    });
    //$('#contact-list tbody').children().remove();
    
  
    //Get DropDown Menu
    //$('#' + parentUID + ' td table tbody tr').each(function () {

    //    //console.log($(this).attr("id"));
    //    var CurrentUID = $(this).attr("id");
    //    var Contact = new Party_office_contacts();
    //    var selector = "#partyOfficesCollection_ " + parentUID + "__partyOfficesCollection_" + parentUID + "__Party_office_contacts_" + CurrentUID + "__Contact_type_code";
    //    console.log(selector);
    //    Contact.Contact_type_code = $("#partyOfficesCollection_" + parentUID + "__partyOfficesCollection_" + parentUID + "__Party_office_contacts_" + CurrentUID + "__Contact_type_code").val();
    //    Contact.Contact_data = $("#partyOfficesCollection_" + parentUID + "__partyOfficesCollection_" + parentUID + "__Party_office_contacts_" + CurrentUID + "__Contact_data").val();
    //    //GetPartyContacts(Contact);
    //});
    $("#update-office").show();
    $("#add-office").hide();


    $('#update-office').on('click', function () {
      
        var Partyoffice = new Political_party_offices();
        var token = $('input[name="__RequestVerificationToken"]').val();
       //var token = "";
        //console.log(token);
        var jString = "{PartyOffice:" + JSON.stringify(Partyoffice) + "}";
        console.log(Partyoffice);
      
        var event = $(this);
        $.ajax({
            url: "/PartyRegistration/UpdateOffice",
                    dataType: "html",
                    //contentType: "",
                    type: "POST",
                    data: { __RequestVerificationToken: token, Partyoffice },
                    success: function (result) {
                        $('#update-office').hide();
                        $('#add-office').show();
                        ResetContactForm();

                       
                        ////Update Grid/////////
                        //officeCode.val(officeCode);
                        //officeTypeCode.val(Partyoffice.Office_type_code);
                        //Person_in_charge.val(Partyoffice.Person_in_charge);
                        //Address_line_one.val(Partyoffice.Address_line_one);
                        //Address_line_two.val(Partyoffice.Address_line_two);
                        //Postal_address_line_one.val(Partyoffice.Postal_address_line_one);
                        //Postal_address_line_two.val(Partyoffice.Postal_address_line_two);
                        console.log();
                        event.unbind();
                        sweetAlert("Yayy..", "Office Updated Successfully!", "success");
                        $("#panel_Office_" + officeCode).replaceWith($.parseJSON(result).message);
                       
                       
                    },
                    beforeSend: function(){
                       // loader.show();
                    },
                    complete:function() {
                        //loader.hide();
                    },
                    error: function () {
                        sweetAlert("Oops..", "Error has occurred!", "error");
                    }
        });



    });


}


function ResetContactForm()
{
    $("#PartyOffice_Code").val("");
    $("#PartyOffice_Office_type_code").val("");
    $("#PartyOffice_Person_in_charge").val("");
    $("#PartyOffice_Address_line_one").val("");
    $("#PartyOffice_Address_line_two").val("");
    $("#PartyOffice_Postal_address_line_one").val("");
    $("#PartyOffice_Postal_address_line_two").val("");

    $('#contact-list tbody').children().remove();
    
}
//function GetPartyOfficesFromDatabase()
//{
//    $.ajax({
//        url: "/PartyRegistration/GetAllPartyOffices",
//        dataType: "html",
//        contentType: "application/json",
//        type: "POST",
//        data: jString,
//        success: function (result) {
//            $('#contact-list tbody').append(result);
//            // console.log(result);
//            RefreshValidation();
//        }

//    });
//}


function GetPartyContacts(Contact)
{
   
    var jString = "{PartyContact:" + JSON.stringify(Contact) + "}";
    console.log(jString);
    $.ajax({
        url: "/PartyRegistration/AddNewContactDetail",
        dataType: "html",
        contentType: "application/json",
        type: "POST",
        data: jString,
        success: function (result) {
            $('#contact-list tbody').append(result);
            // console.log(result);
            RefreshValidation();
        }

    });
}




$('#add-document').on('click', function () {
    
    

    $.ajax({
        url: "/PartyRegistration/AddPartyImagesDetail",
        dataType: "html",
        contentType: "application/json",
        type: "POST",
        success: function (result) {
            $('#document-list tbody').append(result);
            RefreshValidation();
        }

    });

});

$('#btnSave').on('click', function () {
    
    var partyOfficesCollection = new PartyOffices();
    console.log(JSON.stringify(partyOfficesCollection));
    var jString = "{partyOfficesCollection:" + JSON.stringify(partyOfficesCollection) + "}";
    console.log(jString);
    $.ajax({
        url: "/PartyRegistration/SaveOfficeDetails",
        dataType: "html",
        contentType: "application/json",
        data: jString,
        type: "POST",
        success: function (result) {
      
        }

    });

});

function DeleteOffice(ID) {

    

    var token = $('input[name="__RequestVerificationToken"]').val();
    $.ajax({
        url: "/PartyRegistration/RemoveOffice",
        dataType: "html",
        type: "POST",
        data: { __RequestVerificationToken: token, PartyOfficeCode: ID },
        success: function (result) {
            console.log($.parseJSON(result));
            $("#panel_Office_" + ID).remove();
            sweetAlert("Yayy..", "Office Deleted Successfully!", "success");
        }

    });
  
}


function GetOffice(OfficeID)
{
    var token = $('input[name="__RequestVerificationToken"]').val();
    $.ajax({
        url: "/PartyRegistration/GetOffice",
        dataType: "html",
        type: "POST",
        data: { __RequestVerificationToken: token, PartyOfficeCode: OfficeID },
        success: function (result) {
            console.log($.parseJSON(result));
        }

    });
}






















//function PartyOffices()
//{
//    var self = new Array();

//    $('#officelistBody tr').each(function () {

//        var Office = new Political_party_offices();
//        var parentUID = $(this).attr('id');
//        Office.code = $("#partyOfficesCollection_" + parentUID + "__Code").val();
//        Office.Office_type_code = $("#partyOfficesCollection_" + parentUID + "__Office_type_code").val();
//        Office.Person_in_charge = $("#partyOfficesCollection_" + parentUID + "__Person_in_charge").val();
//        Office.Address_line_one = $("#partyOfficesCollection_" + parentUID + "__Address_line_one").val();
//        Office.Address_line_two = $("#partyOfficesCollection_" + parentUID + "__Address_line_two").val();
//        Office.Postal_address_line_one = $("#partyOfficesCollection_" + parentUID + "__Postal_address_line_one").val();
//        Office.Postal_address_line_two = $("#partyOfficesCollection_" + parentUID + "__Postal_address_line_two").val();

//        self.push(Office);
//    });
//    return self;
//   // console.log(JSON.stringify(self));
//}



//function Users()
//{
//    var self = this;
//    self.Username = $("#PROAccount_Username").val();
//    self.Password = $("#PROAccount_Password").val();
//    self.Evr_number = $("#PROAccount_Evr_number").val();

//}

//function Documents_Upload() {
//    var self = this;
//    self.DocumentImage = "";
//}

//function PRO_Account_RegistrationVM() {
//    var self = this;
//    self.PROAccount = new Users();
//    self.partyImagesCollection = new Array();
//    $("#document-list tbody tr").each(function () {
//        var partyImage = new Documents_Upload();
//        partyImage.DocumentImage = $("#partyImagesCollection_" + $(this).attr('id') + "__DocumentImage").val();
//        self.partyImagesCollection.push(partyImage);
//    });

//}

//$("#btnCreateAccount").on('click', function () {
    
//    var form = $("form");

//    if (form.valid())
//    {
//        var Request = new PRO_Account_RegistrationVM();
//        var jString = "{PRS_AcVM:" + JSON.stringify(Request) + "}";
//        console.log(jString);
//        console.log(jString);
//        $.ajax({
//            url: "/PartyRegistration/RegisterAccount",
//            dataType: "html",
//            contentType: "application/json",
//            type: "POST",
//            data: jString,
//            success: function (result) {
//                alert('done');
//            }

//        });
//    }

//});