$(function () {
    $(".datefield").datepicker()({

        dateFormat: "mm/dd/yy",
        changeMonth: true,
        changeYear: true,
        defaultDate: new Date()
    });
});