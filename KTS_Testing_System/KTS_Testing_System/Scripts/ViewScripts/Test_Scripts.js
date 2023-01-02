$(document).ready(function () {
    GetQuestions();
});
function GetQuestions() {
    var columns;
    var levelCode = $('#Tests_question_level_id').val();
    var subjectCode = new Array();
    //subjectsCollection_SUBDIV[4a679e99-731a-43d1-b205-d83c66728863].subject_id
    $("select[name^='subjectsCollection_SUBDIV['][name$='subject_id']").each(function () {
        subjectCode.push(this.value);
        //console.log(this.value);
    });
    console.log(subjectCode);
    //var difficultyCode = $('#difficulty_code').val();
    //var importanceCode = $('#importance_code').val();

    // var provCode = $("#SelectedProvince option:selected").text();


    var constType;
    var radios = document.getElementsByName('RadConstituency');

    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            // do whatever you want with the checked radio
            constType = radios[i].value;

            // only one radio can be logically checked, don't check the rest
            break;
        }
    }
    columns = [
        { "title": "S.No.", "data": "serial_no", "searchable": true },
        { "title": "Level", "data": "level", "searchable": true },
        { "title": "Subject", "data": "subject", "searchable": true },
        { "title": "Difficulty", "data": "difficulty", "searchable": true },
        { "title": "Importance", "data": "importance", "searchable": true },
        { "title": "Question", "data": "question", "searchable": true },
    ];
    columns.push({
        "title": "Action", "render": function (data, type, full, meta) {

            if ($('#selected_' + full.question_id).length === 0) {
                return '<div class="checkbox c-checkbox"><label><input type="checkbox" id="chkSelectedQuestions_' + full.question_id + '" onclick="SelectUnSelectQuestion(this,' + full.question_id + ',\'' + full.level + '\',\'' + full.subject + '\',\'' + full.difficulty + '\',\'' + full.importance + '\',\'' + full.question + '\',' + full.marks + ')" name="SelectedQuestions" value="' + full.question_id + '"><span class="fa fa-check"></span></label></div>';
            }
            else {
                return '<div class="checkbox c-checkbox"><label><input type="checkbox" checked="checked" id="chkSelectedQuestions_' + full.question_id + '" onclick="SelectUnSelectQuestion(this,' + full.question_id + ',\'' + full.level + '\',\'' + full.subject + '\',\'' + full.difficulty + '\',\'' + full.importance + '\',\'' + full.question + '\',' + full.marks + ')" name="SelectedQuestions" value="' + full.question_id + '"><span class="fa fa-check"></span></label></div>';
            }
            //return '<div style="overflow: hidden;white-space: nowrap;"><a class="btn btn-bg-system" onclick="SelectQuestion(' + full.question + ')" type="button" >Select</a> </div>';

        }
    });
    if ($.fn.dataTable.isDataTable('#tblQuestionsPool')) {
        var table = $('#tblQuestionsPool').DataTable();
        table.destroy();
    }
    var table = $('#tblQuestionsPool').DataTable({
        //initComplete: function () {
        //    var input = $('.dataTables_filter input').unbind(),
        //        self = this.api(),
        //        $searchButton = $('<button class="btn btn-oval btn-bg-system">')
        //            .text('Search')
        //            .click(function () {
        //                self.search(input.val()).draw();
        //            }),
        //        $clearButton = $('<button class="btn btn-oval btn-bg-system">')
        //            .text('Clear')
        //            .click(function () {
        //                input.val('');
        //                $searchButton.click();
        //            })
        //    $('.dataTables_filter').append($searchButton, $clearButton);
        //},
        "processing": true,
        "paging": false,
        "serverSide": true,
        "scrollY": 500,
        "deferRender": true,
        "scroller": true,
        "order": [[1, "asc"]],
        "rowId": 'question_id',
        "columnDefs": [{
            "targets": 1, "searchable": true, "orderable": true, "visible": true
        }
        ],
        "ajax": {
            "url": "/Tests/GetQuestionsPool",
            "type": "POST",
            "dataType": "json",
            async: true,
            "data": function (d) {
                return $.extend({}, d, {
                    "LevelCode": levelCode,
                    "SubjectCode": subjectCode
                    //,
                    //"DifficultyCode": difficultyCode,
                    //"ImportanceCode": importanceCode
                });
            }

        },
        "columns": columns
    });


}
function SelectUnSelectQuestion(cb, question_id, level, subject, difficulty, importance, question,marks) {
    //console.log(cb.checked);
    if (cb.checked == true) {
        if ($('#selected_' + question_id).length === 0) {
            var leng = $('#tblSelectedQuestions tr').length;
            var actionBtn = '<div style="overflow: hidden;white-space: nowrap;"><a class="btn btn-bg-system" onclick="RemoveQuestion(' + question_id + ',\'button\',' + marks + ')" type="button" >Remove</a> </div>';
            var hiddenInput = '<td id="tblSelectedQuestions_' + question_id + '" style="display:none;"><input type="hidden" value="' + question_id + '" name="selected_questions" /></td>'
            var rn = '<tr id="selected_' + question_id + '" role="row">' + hiddenInput + '<td>' + leng + '</td><td>' + level + '</td><td>' + subject + '</td><td>' + difficulty + '</td><td>' + importance + '</td><td>' + question + '</td><td>' + actionBtn + '</td></tr>'
            $('#tblSelectedQuestions > tbody:last-child').append(rn);
            CalcMarks(marks, "plus");
            ResetSelectedQuestionsSerial()
        }

    }
    else if (cb.checked == false) {
        RemoveQuestion(question_id, "self",marks);
    }
}
function CalcMarks(marks, operator) {
    var sumMarks = 0;
    sumMarks=Number($("input[name='Tests.total_marks']").val());
    if (operator == 'minus')
    {
        sumMarks -= marks;
    }
    else {
        sumMarks += marks;
    }
    $("input[name='Tests.total_marks']").val(sumMarks);
    var rowCount = $('table#tblSelectedQuestions tr').length;
    $("input[name='Tests.total_questions']").val(rowCount - 1);
}
function ResetSelectedQuestionsSerial() {
    var table = document.getElementById('tblSelectedQuestions');
    for (var i = 1; i < table.rows.length; i++) {
        var firstCol = table.rows[i].cells[1]; //first column
        firstCol.innerHTML = i;
    }
}
function RemoveQuestion(rowid, src,marks) {
    if (src === "button") {
        $('table#tblSelectedQuestions tr#selected_' + rowid).remove();
        $('#chkSelectedQuestions_' + rowid).prop("checked", false);
    }
    else {
        $('table#tblSelectedQuestions tr#selected_' + rowid).remove();
    }
    CalcMarks(marks, "minus");
    ResetSelectedQuestionsSerial();
}
function SetProcessingStatusAndSubmit(arg) {
    // $("#loadingOverlay").show();

    if (arg === "QuestionPoolDetailsTab") //&& $("#FilterDetailsTab").hasClass('active')
    {
        GetQuestions();
    }
    //else if (arg === "SelectedQuestionsTab") {
    //    var searchIDs = $("#SelectedQuestions:checked").map(function () {
    //        return $(this).val();
    //    }).get(); // <----
    //    console.log(searchIDs);
    //}
    $("[name='processingStatus']").removeClass('active');
    $("[name='detailsPanel']").removeClass('active');
    $("[name='processingStatus']").addClass('inactive');
    $("[name='detailsPanel']").addClass('disabled');

    $("#btn" + arg).removeClass('inactive');
    $("#" + arg).removeClass('disabled');
    $("#" + arg).addClass('active');
    $("#btn" + arg).addClass('active');
}



$('#add-subject').on('click', function () {

    var token = $('input[name="__RequestVerificationToken"]').val();
    $.ajax({
        url: "/Tests/AddNewSubjectDetail",
        //dataType: "html",
        //contentType: "application/json",
        data: { __RequestVerificationToken: token },
        type: "POST",
        success: function (result) {
            $("#subject-list").append(result);
            var panelID = $($(result).find("div:first div")[2]).attr("id");
            $('#' + panelID).collapse('show');
            RefreshValidation();
        },
        beforeSend: function () {
            $("#SubjectLoader").fadeIn();
        },
        complete: function () {
            $("#SubjectLoader").fadeOut();
        }

    });

});


function AddDifficulty(code) {
    var contacts = "#subject_difficulty_" + code;
    var containerPrefix = "subjectsCollection_SUBDIV[" + code + "]";
    var token = $('input[name="__RequestVerificationToken"]').val();

    $.ajax({
        url: "/Tests/AddNewDifficultyDetail",
        data: { __RequestVerificationToken: token, parentprefix: containerPrefix },
        type: "POST",
        success: function (result) {
            $(contacts).append(result);
            RefreshValidation();
        },
        beforeSend: function () {
            $("#DifficultyLoader_" + code).fadeIn();
        },
        complete: function () {
            $("#DifficultyLoader_" + code).fadeOut();
        }

    });
}
function AddImportance(parent_code,code) {
    var contacts = "#subject_importance_" + code;
    var containerPrefixParent = "subjectsCollection_SUBDIV[" + parent_code + "]";
    var containerPrefix = containerPrefixParent+".listDifficultyFilter[" + code + "]";
    var token = $('input[name="__RequestVerificationToken"]').val();

    $.ajax({
        url: "/Tests/AddNewImportanceDetail",
        data: { __RequestVerificationToken: token, parentprefix: containerPrefix },
        type: "POST",
        success: function (result) {
            $(contacts).append(result);
            RefreshValidation();
        },
        beforeSend: function () {
            $("#ImportanceLoader_" + code).fadeIn();
        },
        complete: function () {
            $("#ImportanceLoader_" + code).fadeOut();
        }

    });
}
function importanceChange(imp)
{
    var stringVariable = imp.name;
    //get names of ImportanceNoOFMcqs in according panel
    var imps = stringVariable.substring(0, stringVariable.lastIndexOf('['));   
    var impSum = 0;
    $("input[name^='" + imps + "'][name$='ImportanceNoOFMcqs']").each(function () {
        impSum += Number(this.value);
    });
    var dif = imps.substring(0, imps.lastIndexOf(']'));
    $("input[name^='" + dif + "'][name$='DifficultyNoOFMcqs']").val(impSum);

    var difs = dif.substring(0, dif.lastIndexOf('['));
    var difSum = 0;
    $("input[name^='" + difs + "'][name$='DifficultyNoOFMcqs']").each(function () {
        difSum += Number(this.value);
        //console.log(this.value);
    });

    var subj = difs.substring(0, difs.lastIndexOf(']'));
    $("input[name^='" + subj + "'][name$='SubjectNoOFMcqs']").val(difSum);

    var subs = subj.substring(0, subj.lastIndexOf('['));
    var subjSum = 0;
    $("input[name^='" + subs + "'][name$='SubjectNoOFMcqs']").each(function () {
        subjSum += Number(this.value);
        //console.log(this.value);
    });
    $("input[name='TotalTestQuestions']").val(subjSum);
    

}

function submitTestOnClick(objButton) {

    var form = $("#testForm");
    var validator = form.data('validator');
    validator.settings.ignore = "";
    var container = $("div .row.panel-heading.bg-system");
    if ($("#testForm").valid()) {
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
    var msg = "Please confirm data entered is correct!";
    if (objButton.value == "ready")
    {
        msg = "You will not be able to change the Test once you save and close it. "+msg;
    }
    swal({
        title: "Are you sure?",
        text: msg,
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#23b7e5",
        confirmButtonText: "Submit",
        closeOnConfirm: true
    },
        function () {
            //document.getElementById('Tests_status').value = objButton.value;
            $("input[name='Tests.status']").val(objButton.value);
            $("#testForm").submit();
        });
}
