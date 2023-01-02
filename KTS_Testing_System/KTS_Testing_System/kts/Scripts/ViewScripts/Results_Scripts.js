$(document).ready(
    $('.date').datetimepicker({
        allowInputToggle: true,
        format: 'DD-MMM-YYYY'
    })
);

$('#add-document').on('click', function () {

    var conType = $('#conType').val();
    $.get('/OpsResults/AddDocuments?conType=' + conType).done(function (html) {
        $('#image-list').append(html);
    });
});

function ShowImage(caller) {
    // Get the modal
    var modal = document.getElementById('myModal');
    var modalImg = document.getElementById("img01");

    modalImg.src = $(caller).attr("src");
    modal.style.display = "block";
};

function ShowROImage(caller) {
    // Get the modal
    var modal = document.getElementById('ImgPreviewModal');
    var modalImg = document.getElementById("img01");    

    modalImg.src = $(caller).attr("src");
    modal.style.display = "block";
};

function DeleteDocument(callingObj, location) {
    var selector = "#" + location + "";
    $(selector).remove();

    RefreshValidation();
}

function DeleteDocumentNew(callingObj, level) {
    var x = "";
    if (level == "2")
        $(callingObj).parent().parent().remove();
    if (level == "3")
        $(callingObj).parent().parent().parent().remove();
}

function PrintReport(resultControlDataId, type) {
    var url = "";
    url = "/Results/PSResultImageReport?resultControlId=" + resultControlDataId + "&conType=" + type;
    location.href = url;
    //window.open(url,'_blank');
}

