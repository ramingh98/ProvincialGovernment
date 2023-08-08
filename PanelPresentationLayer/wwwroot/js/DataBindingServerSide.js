if (document.getElementsByTagName('body').length == 0)
    window.location.reload();
var table = null;
//***************************************************************
var AddBoxModalOnShow = null;
var AddBoxModalOnHide = null;
var EditBoxModelOnShow = null;
var EditBoxModelOnHide = null;
var OnDeletedCallBack = null;
var OnEditedCallBack = null;
var OnCreatedCallBack = null;
var TableOnDraw = null;
//***************************************************************
var isDefaultCreateBtn = true;
//****************************************************************
function enableSubmitBtn() {
    $('#resultLoader').hide();

}
function disableSubmitBtn() {
    $('#resultLoader').show();
}
//***********************************************************
function resetTable() {
    $(function () {
        if (table != null) {
            $('#GridFilterBar').hide();
            $('#modelList tbody').empty();

        }
    });
}
//******************************************************
var MainViewModel = {
    c: ko.observable({}),  //Create
    e: ko.observable({}), //Edit
    d: ko.observable({ Details: {} }), //Detail
    l: ko.observableArray([]), //List , 
    fe: ko.observable({}),

};
//*************************************************

var extendViewModelColumns = [];
var IsAutomaticInxCalculator = false;
var InxCalculatorUrl = '';
function DataBind(addUrl, readUrl, editUrl, updateUrl, deleteUrl, detailUrl, bindDataOnPageLoad) {
    if (bindDataOnPageLoad == null) { bindDataOnPageLoad = true; }
    $(function () {
 

        $.validator.unobtrusive.parse(document);
        $.validator.setDefaults({
            ignore: [],
        });
        ko.applyBindings(MainViewModel, document.getElementById('mainArea'));
        var isSecondRun = false;

        //=================================================================================================

        $('#modelList').on('click', '.edit',
            function () {
                var data = table.row($(this).parents('tr')).data();
                DataTransfer(editUrl, '{"Id": "' + data.Id + '"}', '.spinner',
                    function (f) {
                        toastr.remove();
                        if (!f.IsSuccess) {

                            toastr.error(f.Message);
                        } else {
                            MainViewModel.e(f.Value);

                            $('#EditBox').modal({ backdrop: 'static', keyboard: false });
                            $('#EditBox').modal('show');
                        }
                    },
                    function (e) {
                        toastr.remove();
                        toastr.error("خطا در ارتباط با سرور");
                    }
                );
            });
        //=================================================================================================
        $('#modelList').on('change', 'tbody> tr > td   input[type=checkbox]',
            function () {
                if (isCheckedAll()) {
                    $('thead> tr> th input[type=checkbox]').prop('checked', 'checked');
                } else {
                    $('thead> tr> th input[type=checkbox]').removeAttr('checked');
                }
            });
        //=================================================================================================
        if (bindDataOnPageLoad) {
            dataRead(readUrl);
        }
        //=================================================================================================
        function getDataGridColumns() {
            var list = [];
            if ($('#modelList > thead').attr('data-checkboxColumn') == 'true' || $('#modelList > thead').attr('data-checkboxColumn') == null) {
                list.push({
                    "targets": 0,
                    "defaultContent": '<div class="checkbox"><label style="font-size: 1.2em"><input type="checkbox" value=""><span class="cr"><i class="cr-icon fa fa-check"></i></span></label></div>',
                    "orderable": false,
                    "data": null,
                });
            }
            for (i = 0; i < $('#modelList > thead > tr > th').length; i++) {
                var columnName = $('#modelList > thead > tr > th').eq(i).attr('data-viewModel');
                var orderable = $('#modelList > thead > tr > th').eq(i).attr('data-orderable');
                var thousandSeparator = $('#modelList > thead > tr > th').eq(i).attr('data-thousandseparator');
                console.log(thousandSeparator);
                if (columnName != null && columnName != 'Id') {
                    var columnOption = { "data": columnName, "orderable": orderable == null ? true : false };
                    if (thousandSeparator == 'true') {
                        columnOption.render = $.fn.dataTable.render.number(',', '.', 0);
                    }
                    list.push(columnOption);
                }
            }
            if ($('#modelList > thead').attr('data-actionColumn') == 'true' || $('#modelList > thead').attr('data-actionColumn') == null) {
                list.push({
                    "targets": -1,
                    "data": null,
                    "orderable": false,
                    "defaultContent": "<button class='btn btn-info btn-xs edit'  type='button' style='margin-right:5px; margin-top:2px;  width:60px;'><i class='fa fa-pencil' style='margin-left: 3px;'></i>ویرایش</button>"
                });
            }
            if ($('#modelList > thead').attr('data-actionColumn') == 'custom') {
                list.push({
                    "targets": -1,
                    "data": null,
                    "orderable": false,
                    "defaultContent": $('#operands').html()
                });
            }
            return list;
        }

        function getRequestColumns() {
            var list = [];
            for (i = 0; i < $('#modelList > thead > tr > th').length; i++) {
                var a = $('#modelList > thead > tr > th').eq(i).attr('data-viewModel');
                if (a != null) {
                    list.push(a);
                }
            }
            if (extendViewModelColumns.length != 0) {
                for (i = 0; i < extendViewModelColumns.length; i++) {
                    list.push(extendViewModelColumns[i]);
                }
            }
            return list;
        }
        //=================================================================================================
        function dataTableConfig(url) {
            var colCounts = $('#modelList > thead> tr> th').length;

            var listDisplayCols = [];
            for (i = 1; i < colCounts - 1; i++)
                listDisplayCols.push(i);


            var options = {
                stateSave: true,
                'processing': true,
                'serverSide': true,
                ajax: {
                    "url": url,
                    "type": "post",
                    'beforeSend': function (request) {
                        $('.spinner').show();
                    },
                    data: function (d) {
                        d.searchWith = $('#SearchWith').val();
                        d.RequestColumns = getRequestColumns();
                        d.Filters = ko.toJSON( MainViewModel.fe());
                    },
                    'complete': function (xhr, textStatus) {
                        if (xhr.status == 401) {
                            window.location.href = '/Account/Index/';
                        }
                        $('.spinner').hide();
                        $('#modelList > thead > tr > th:first').removeClass('sorting_asc');
                        $(window).resize();
                    },
                    error: function (e) {
                        toastr.remove();
                        toastr.error('خطا در ارتباط با سرور');
                    },
                    "dataSrc": function (json) {
                        if (!json.IsSuccess) {
                            $('.spinner').hide();
                            toastr.error(json.Message);
                        }
                        json.draw = json.Value.draw;
                        json.recordsTotal = json.Value.recordsTotal;
                        json.recordsFiltered = json.Value.recordsFiltered;
                        return json.Value.data;

                    },

                },
                "columns": getDataGridColumns(),
                dom: '<"html5buttons"B>lTfgitp',

                buttons: [
                    {
                        extend: 'excel', text: 'اکسل', title: $('title').html(),
                        exportOptions: {
                            columns: listDisplayCols
                        }
                    },
                    {
                        extend: 'print', text: 'چاپ', title: $('title').html(),
                        exportOptions: {
                            columns: listDisplayCols
                        },
                        customize: function (win) {
                            $(win.document.body).addClass('white-bg');
                            $(win.document.body).css('font-size', '10px');
                            $(win.document.body).css('direction', 'rtl');
                            $(win.document.body).find('table')
                                .addClass('compact')
                                .css('font-size', 'inherit')
                            $(win.document.body).find('body').children().css('margin', '10px;');
                        }
                    }
                ],
                order: [],
                columnDefs: [{ "type": 'pstring', targets: '_all' }],
                language: {
                    "sEmptyTable": "هیچ داده ای در جدول وجود ندارد",
                    "sInfo": "نمایش _START_ تا _END_ از _TOTAL_ رکورد",
                    "sInfoEmpty": "نمایش 0 تا 0 از 0 رکورد",
                    "sInfoFiltered": "(فیلتر شده از _MAX_ رکورد)",
                    "sInfoPostFix": "",
                    "sInfoThousands": ",",
                    "sLengthMenu": "نمایش _MENU_ رکورد",
                    "sLoadingRecords": "در حال بارگزاری...",
                    "sProcessing": "در حال پردازش...",
                    "sSearch": '<div class="input-group" style="position: relative;margin-left:15px;"><span class="glyphicon glyphicon-remove serach-clear" style=""></span> <span class="input-group-addon"><span class="glyphicon glyphicon-search"></span></span> ',
                    "searchPlaceholder": 'جستجو...',
                    "sZeroRecords": "رکوردی با این مشخصات پیدا نشد",
                    "oPaginate": {
                        "sFirst": "ابتدا",
                        "sLast": "انتها",
                        "sNext": "بعدی",
                        "sPrevious": "قبلی"
                    },
                    "oAria": {
                        "sSortAscending": ": فعال سازی نمایش به صورت صعودی",
                        "sSortDescending": ": فعال سازی نمایش به صورت نزولی"
                    }
                },

            }
            //if ($('#modelList > thead').attr('data-actionColumn') == 'true' || $('#modelList > thead').attr('data-actionColumn') == null) {
            //    options.columnDefs.push({
            //        "targets": -1,
            //        //"data": null,
            //        //"orderable": false,
            //        "defaultContent": "<button class='btn btn-info btn-xs edit'  type='button' style='margin-right:5px; margin-top:2px;  width:60px;'><i class='fa fa-pencil' style='margin-left: 3px;'></i>ویرایش</button>"
            //        //"<button class='btn btn-danger btn-xs detail' style='margin-right:5px; margin-top:2px; width:60px;' type='button'><i class='fa fa-remove' style='margin-left: 3px;'></i>حذف</button>"
            //    });
            //}
            //if ($('#modelList > thead').attr('data-checkboxColumn') == 'true' || $('#modelList > thead').attr('data-checkboxColumn') == null) {
            //    options.columnDefs.push({
            //        "targets": 0,
            //       // "data": null,
            //      //  "orderable": false,
            //        "defaultContent": '<div class="checkbox"><label style="font-size: 1.2em"><input type="checkbox" value=""><span class="cr"><i class="cr-icon fa fa-check"></i></span></label></div>'
            //    });
            //}

            //if ($('#modelList > thead').attr('data-actionColumn') == 'custom') {
            //    options.columnDefs.push({
            //        "targets": -1,
            //        "data": null,
            //        "orderable": false,
            //        "defaultContent": $('#operands').html()
            //    });
            //}
            table = $('#modelList').DataTable(options);

        }
        $('body').on('click', '.serach-clear', function () {
            $('input[type=search]').val('').change().keyup();
        });
        //=================================================================================================
        $('#btnExcel').click(function () {

            $('.buttons-excel').click();
        });
        $('#btnPrint').click(function () {
            $('.buttons-print').click();
        });
        $('#btnCopy').click(function () {
            $('.buttons-copy').click();
        });
        //=================================================================================================
        function createSearchWith() {
            var list_option = '';
            var j = 0;
            if ($('#modelList > thead').attr('data-checkboxColumn') == 'true' || $('#modelList > thead').attr('data-checkboxColumn') == null) {
                j = 1;
            }
            for (i = j; i < $('#modelList > thead > tr > th').length; i++) {

                var option = '<option value="!">#</option>';
                var a = $('#modelList > thead > tr > th').eq(i).attr('data-viewModel');
                if (a != null) {
                    option = option.replace('!', a);
                    option = option.replace('#', $('#modelList > thead > tr > th').eq(i).html());
                    list_option += option;
                }

            }
            $('#SearchWith').html(list_option);
            if (window.localStorage.getItem('modelList_searchWith_' + window.location.pathname) != null) {
                $('#SearchWith').val(window.localStorage.getItem('modelList_searchWith_' + window.location.pathname).toString());
            }
        }
        //======================================================================================
        $('body').on('change', '#SearchWith', function () {
            window.localStorage.setItem('modelList_searchWith_' + window.location.pathname, $('#SearchWith').val());
        });
        //======================================================================================
        $('#searchValue').change(function () {
            if ($(this).val() == '' && isSearchHasText == true) {
                isSearchHasText = false;
                $('#searchBtn').click();
            }
            if ($(this).val() != '') {
                isSearchHasText = true;
            }
        });
        //======================================================================================
        $('#searchValue').keypress(function (e) {
            if (e.keyCode == 13) {

                $('#searchBtn').click();
            }
            if ($(this).val() != '') {
                isSearchHasText = true;
            } else {
                isSearchHasText = false;
            }
        });
        //======================================================================================
        $('#searchValue').focusin(function () {
            if ($(this).val() != '') {
                isSearchHasText = true;
            } else {
                isSearchHasText = false;
            }
        });
        //======================================================================================
        var isSearchHasText = true;
        $('#searchValue').keyup(function (e) {
            if ($(this).val() == '' && isSearchHasText == true) {
                isSearchHasText = false;
                $('#searchBtn').click();
            }
            if ($(this).val() != '') {
                isSearchHasText = true;
            }
        });
        //======================================================================================
        $('#searchBtn').click(function () {
            $('#searchValue').val($('#searchValue').val().trim());
            table.search($('#searchValue').val()).draw();
        });
        //======================================================================================
        $('#length_change').change(function () {
            table.page.len($(this).val()).draw();
        });
        //=================================================================================================
        function dataRead(url) {

            if (table == null) {
                createSearchWith();
                dataTableConfig(url);
                $('#length_change').val($('select[name=modelList_length]').val());
                $('#searchValue').val($('#modelList_filter input').val());
                table.on('draw', function () {
                    $('thead> tr> th> input[type=checkbox]').prop('checked', false)
                    $('#table_info').html($('.dataTables_info').html());
                    $('#GridFilterBar').show();
                    if (TableOnDraw != null) {
                        if (!($('#modelList > tbody > tr > td:first').hasClass('dataTables_empty'))) {
                            TableOnDraw();
                        }
                    }
                });



            } else {
                table.ajax.url(url).load();
            }
        }
        //=================================================================================================
        function isCheckedAll() {
            for (i = 0; i < $('tbody> tr').length; i++) {
                if ($('tbody> tr').eq(i).children('td').eq(0).find('input[type=checkbox]').prop("checked") == false) {
                    return false;
                }
            }
            return true;
        }
        //=================================================================================================
        $('thead> tr> th  input[type=checkbox]').change(function () {
            if ($(this).prop("checked") == false) {
                $('tbody> tr').each(function () {
                    $(this).children('td').eq(0).find('input[type=checkbox]').removeAttr('checked');
                });
            } else {
                $('tbody> tr').each(function () {
                    $(this).children('td').eq(0).find('input[type=checkbox]').prop('checked', 'checked');
                });
            }
        });
        //=================================================================================================
        $('#AddForm').submit(function () {
            if ($(this).valid()) {
                disableSubmitBtn();
                DataTransfer(addUrl, ko.toJSON(MainViewModel.c()), '',
                    function (e) {
                        var f = e;
                        toastr.remove();
                        if (!f.IsSuccess) {
                            toastr.error(f.Message);
                            enableSubmitBtn();
                        } else {
                            toastr.success(f.Message);
                            $('#AddBox').modal('hide');
                            MainViewModel.c({});
                            enableSubmitBtn();
                            if (OnCreatedCallBack != null) {
                                OnCreatedCallBack();
                            }
                            dataRead(readUrl);
                        }
                    },
                    function (e) {
                        enableSubmitBtn();
                        toastr.remove();
                        toastr.error("خطا در ارتباط با سرور");
                    }
                );
            }
            return false;
        });
        //=================================================================================================

        $('#delete').click(function () {
            if ($('tbody> tr > td   input:checked').length > 0) {
                bootbox.confirm({
                    message: "آیا برای حذف موارد انتخاب شده مطمئن می باشید؟",
                    buttons: {
                        confirm: {
                            label: 'تایید',
                            className: 'btn-primary'
                        },
                        cancel: {
                            label: 'انصراف',
                            className: 'btn-default'
                        }
                    },
                    callback: function (result) {
                        if (result) {
                            disableSubmitBtn();
                            var list = [];
                            for (i = 0; i < $('#modelList tbody input:checked').length; i++) {
                                var data = $('#modelList').DataTable().row($('#modelList tbody input:checked').eq(i).parents('tr')).data();

                                list.push({ Id: data.Id });
                            }
                            var listForDelete = JSON.stringify(list);
                            DataTransfer(deleteUrl,
                                listForDelete,
                                '',
                                function (e) {
                                    var f = e;

                                    toastr.remove();
                                    if (!f.IsSuccess) {
                                        toastr.error(f.Message);
                                        enableSubmitBtn();
                                    } else {
                                        toastr.success(f.Message);
                                        enableSubmitBtn();
                                        if (OnDeletedCallBack != null) {
                                            OnDeletedCallBack();
                                        }
                                        dataRead(readUrl);
                                    }
                                },
                                function (e) {
                                    enableSubmitBtn();
                                    toastr.remove();
                                    toastr.error("خطا در ارتباط با سرور");
                                }
                            );
                        }
                    }
                });
            } else {
                toastr.remove();
                toastr.error("هیچ موردی را برای حذف انتخاب نکرده اید");
            }
        });
        //=================================================================================================
        $('#EditForm').submit(function () {
            if ($(this).valid()) {
                disableSubmitBtn();
                DataTransfer(updateUrl, ko.toJSON(MainViewModel.e()), '',
                    function (e) {
                        var f = e;
                        toastr.remove();

                        if (!f.IsSuccess) {
                            toastr.error(f.Message);
                            enableSubmitBtn();
                        } else {
                            toastr.success(f.Message);
                            $('#EditBox').modal('hide');
                            enableSubmitBtn();
                            if (OnEditedCallBack != null) {
                                OnEditedCallBack();
                            }
                            dataRead(readUrl);
                        }
                    },
                    function (e) {
                        toastr.remove();
                        enableSubmitBtn();
                        toastr.error("خطا در ارتباط با سرور");
                    }
                );
            }
            return false;
        });
        //=================================================================================================
        $('#EditBox').on('hidden.bs.modal',
            function (e) {
                $("#EditBox").validate().resetForm();
                $('.field-validation-error')
                    .removeClass('field-validation-error')
                    .addClass('field-validation-valid');
                $('span.field-validation-valid > span').remove();
                $('.input-validation-error')
                    .removeClass('input-validation-error')
                    .addClass('valid');
                $('span.input-validation-valid > span').remove();
                $('.IpnComboBoxContainer-ul').html('');
                MainViewModel.e({});
                if (EditBoxModelOnHide != null) {
                    EditBoxModelOnHide();
                }
            });
        //****************************************************************
        $('#EditBox').on('shown.bs.modal',
            function (e) {
                if (EditBoxModelOnShow != null) {
                    EditBoxModelOnShow();
                }
            });
        //=================================================================================================
        $('#AddBox').on('hidden.bs.modal',
            function (e) {
                $("#AddBox").validate().resetForm();
                $('.field-validation-error')
                    .removeClass('field-validation-error')
                    .addClass('field-validation-valid');
                $('span.field-validation-valid > span').remove();
                $('.input-validation-error')
                    .removeClass('input-validation-error')
                    .addClass('valid');
                $('span.input-validation-valid > span').remove();
                MainViewModel.c({});
                if (AddBoxModalOnHide != null) {
                    AddBoxModalOnHide();
                }
            });
        //=================================================================================================
        $('#create').click(function () {
            if (isDefaultCreateBtn) {
                if (IsAutomaticInxCalculator) {
                    InxCalculator(0, function () {
                        $('#AddBox').modal({ backdrop: 'static', keyboard: false });
                        $('#AddBox').modal('show');
                    });
                } else {
                    $('#AddBox').modal({ backdrop: 'static', keyboard: false });
                    $('#AddBox').modal('show');
                }
            }
        });
        $('#AddBox').on("shown.bs.modal", function (event) {
            if (AddBoxModalOnShow != null) {
                AddBoxModalOnShow();
            }
        });
        //=================================================================================================
        $('#applyFilter').click(function () {
            toastr.remove();
            dataRead(readUrl, ko.toJSON(MainViewModel.fe));
        });
        //=================================================================================================
        $('#reloadTableBtn').click(function () {
            if (table != null) {
                $('#modelList').dataTable().fnSort([]);
            }

        });
        //*************************************************************************************************
    });
}
//=======================================================================================================
function InxCalculator(retlateFieldId, callbackDelegate) {
    var data = { Id: retlateFieldId };
    DataTransfer(InxCalculatorUrl, ko.toJSON(data), '#resultLoader',
        function (f) {
            if (!f.IsSuccess) {
                enableSubmitBtn();
            } else {
                $('#Inx').val(f.Value).change();
                enableSubmitBtn();
            }
            if (callbackDelegate != null) {
                callbackDelegate();
            }
        },
        function (e) {
            enableSubmitBtn();
            if (callbackDelegate != null) {
                callbackDelegate();
            }
        }
    );
}
//=======================================================================================================