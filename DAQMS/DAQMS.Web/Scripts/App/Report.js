var Report = function () {

    var _validateForm = function () {
        // Setup form validation on the #ContactCreateOrEdit element
        if ($().validate) {
            var form = $("#frmReportEditor");
            var error = $('.alert-danger', form);
            var success = $('.alert-success', form);

            form.validate({
                doNotHideMessage: true, //this option enables to show the error/success messages on tab switch.
                errorElement: 'span', //default input error message container
                errorClass: 'help-block help-block-error', //help-block help-block-error default input error message class
                focusInvalid: false, // do not focus the last invalid input
                // Specify the validation rules
                rules: {
                    emp_name: {
                        required: true
                    },
                    emp_emailaddress: {
                        required: true
                    }
                },
                errorPlacement: function (error, element) { // render error placement for each input type
                    //if (element.attr("name") == "gender") { // for uniform radio buttons, insert the after the given container
                    //    error.insertAfter("#form_gender_error");
                    //} else if (element.attr("name") == "payment[]") { // for uniform checkboxes, insert the after the given container
                    //    error.insertAfter("#form_payment_error");
                    //} else {
                    //    error.insertAfter(element); // for other inputs, just perform default behavior
                    //}
                    var errorContainer = element.parents('div.form-group');
                    errorContainer.append(error);
                    //error.insertAfter(element);
                },
                messages: {
                    // Specify the custom validation error messages
                    emp_name: {
                        required: "Name is required."
                    },
                    emp_emailaddress: {
                        required: "Email address is required."
                    }
                },
                invalidHandler: function (event, validator) { //display error alert on form submit              
                    success.hide();
                    error.show();
                },
                highlight: function (element) { // hightlight error inputs
                    $(element)
                        .closest('.form-group').addClass('has-error'); // set error class to the control group
                },
                unhighlight: function (element) { // revert the change done by hightlight
                    $(element)
                        .closest('.form-group').removeClass('has-error'); // set error class to the control group
                },
                success: function (label) {
                    label.closest('.form-group').removeClass('has-error'); // set success class to the control group
                },
                submitHandler: function (form) {
                    if ($('#btnSaveReport').length > 0) {
                        var url = $(form).attr('action');
                        $.post(url, $(form).serializeArray(),
                            function (res) {
                                if (res.success) {

                                    App.modalHide();
                                    App.toastrNotifier(res.data, true);

                                } else {
                                    App.toastrNotifier(res.data, false);
                                }
                            });
                    } else {
                        form.submit(function (e) { });
                    }

                }
            });
        }
    };

    var _googleChartsHandler = function () {
        function drawChartByClick(companyId, projectId, deviceId, dateRangeFrom, dateRangeTo, valueType, sensor) {

            var options = {
                title: 'Daily Average Temperature'
            };

            var url = '/Report/GetTempLineLineChartAjax?companyId=' + companyId + '&projectId=' + projectId + '&deviceId=' + deviceId + '&dateRangeFrom=' + dateRangeFrom + '&dateRangeTo=' + dateRangeTo + '&valueType=' + valueType + '&sensor=' + sensor;

            $.get(url, {}, function (result) {
                var data = google.visualization.arrayToDataTable(result);

                var chart = new google.visualization.LineChart(document.getElementById('temp_linechart'));
                chart.draw(data, options);
            });
        }

        function initialize() {
            $('#btnSearch').unbind('click').on('click', function () {
                var companyId = $('#CompanyId').val();
                var projectId = $('#ProjectId').val();
                var deviceId = $('#DeviceId').val();
                var dateRangeFrom = $('#DateRangeFrom').val();
                var dateRangeTo = $('#DateRangeTo').val();
                var valueType = $('#ValueType').val();
                var sensor = $('#CompanyId').val();
                drawChartByClick(companyId, projectId, deviceId, dateRangeFrom, dateRangeTo, valueType, sensor);
            });
        }
        google.charts.setOnLoadCallback(initialize);
        google.charts.load("visualization", "1", { packages: ["corechart"] });
    };

    var _actionHandler = function () {

    };
    var linechartObjData;

    var _initializeForm = function () {

        $('#CompanyId').unbind('change').on('change', function () {
            App.loadDropdown('ProjectId', '/Report/LoadProjectListByCompanyIdAjax', { companyId: $('#CompanyId').val() });
        });

        $('#ProjectId').unbind('change').on('change', function () {
            App.loadDropdown('DeviceId', '/Report/LoadDeviceListByCompanyIdAjax', { projectId: $('#ProjectId').val() });
        });

        linechartObjData = $('#table_report').dataTable({
            "bJQueryUI": true,
            "bAutoWidth": false,
            "sPaginationType": "full_numbers",
            "bSort": false,
            "oLanguage": {
                "sLengthMenu": "Display _MENU_ records per page",
                "sZeroRecords": "Nothing found - Sorry",
                "sInfo": "Showing _START_ to _END_ of _TOTAL_ records",
                "sInfoEmpty": "Showing 0 to 0 of 0 records",
                "sInfoFiltered": "(filtered from _MAX_ total records)"
            },
            "bProcessing": true,
            "bServerSide": true,
            "sAjaxSource": "/Optimize/GetCategories",
            "aoColumns": [{
                "sName": "RecordDate",
                "bSearchable": false,
                "bSortable": false,
            },
            { "sName": "T1" },
            { "sName": "T2" },
            { "sName": "T3" },
            { "sName": "T4" },
            { "sName": "T5" },
            { "sName": "T6" },
            { "sName": "T7" },
            { "sName": "T8" }
            ]
        });

    };

    var initializeReport = function () {
        _validateForm();
        _initializeForm();
        _actionHandler();
        _googleChartsHandler();
    };

    return {
        init: initializeReport
    };
}();