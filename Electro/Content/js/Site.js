function functAddCart(productID, quantity) {
    $.ajax({
        type: "POST",
        url: "/Home/AddCart",
        data: { "productID": productID, "quantity": quantity },
        success: function () { waitingDialog.show('Sepete Ekleniyor', { dialogSize: 'sm', progressType: 'warning' }); setTimeout(function () { waitingDialog.hide(); }, 1000) },
        error: function (e) { alert(e.responseText) },
    });
    $(".productCount").text(parseInt($(".productCount").text()) + 1)
}

function deleteCart(productID) {
    $.ajax({
        type: "POST",
        url: "/Home/DeleteCart",
        data: { "productID": productID },
        success: function () { location.href = '/Cart'; },
        error: function (e) { alert(e.responseText) },
    });
}
function updateCart(productID, quantity, sender, pm) {
    if (parseInt($(sender).parent().find(".cartDetailValue").val()) + parseInt(pm) != 0) {

        $.ajax({
            type: "POST",
            url: "/Home/UpdateCart",
            data: { "productID": productID, "quantity": parseInt($(sender).parent().find(".cartDetailValue").val()) + parseInt(pm) },
            success: function () { location.href = '/Cart'; },
            error: function (e) { alert(e.responseText) },
        });
    }


}

function functcheckoutAddress(sender) {
    $(".Address").val("");
    $(".City").val("");
    $(".District").val("");
    $(".Name").val("");
    if ($(sender).val() != "") {

        $.ajax({
            type: "GET",
            url: "/Check/GetAddress",
            data: { "addressID": $(sender).val() },
            success: function (data) {
                $(".Address").val(data.MemberAddress);
                $(".Address").attr("readonly", "readonly");
                $(".City").val(data.City);
                $(".City").attr("readonly", "readonly");
                $(".District").val(data.District);
                $(".District").attr("readonly", "readonly");
            },
            error: function (e) { alert(e.responseText) },
        });
        $(".Name").fadeOut();
    }
    else {
        $(".Address").removeAttr("readonly");
        $(".City").removeAttr("readonly");
        $(".District").removeAttr("readonly");
        $(".Name").fadeIn();
    }
}




var proQty = $('.pro-qty');
proQty.on('click', '.qtybtn', function () {
    var $button = $(this);
    var oldValue = $button.parent().find('input').val();
    if ($button.hasClass('inc')) {
        var newVal = parseFloat(oldValue) + 1;
    } else {
        // Don't allow decrementing below zero
        if (oldValue > 1) {
            var newVal = parseFloat(oldValue) - 1;
        } else {
            newVal = 1;
        }
    }
    $button.parent().find('input').val(newVal);
});

var waitingDialog = waitingDialog || (function ($) {
    'use strict';


    var $dialog = $(
        '<div class="modal fade" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-hidden="true" style="padding-top:15%; overflow-y:visible;">' +
        '<div class="modal-dialog modal-m">' +
        '<div class="modal-content">' +
        '<div class="modal-header"><h3 style="margin:0;"></h3></div>' +
        '<div class="modal-body">' +
        '<div class="progress progress-striped active" style="margin-bottom:0;"><div class="progress-bar" style="width: 100%"></div></div>' +
        '</div>' +
        '</div></div></div>');

    return {

        show: function (message, options) {

            if (typeof options === 'undefined') {
                options = {};
            }
            if (typeof message === 'undefined') {
                message = 'Loading';
            }
            var settings = $.extend({
                dialogSize: 'm',
                progressType: '',
                onHide: null
            }, options);


            $dialog.find('.modal-dialog').attr('class', 'modal-dialog').addClass('modal-' + settings.dialogSize);
            $dialog.find('.progress-bar').attr('class', 'progress-bar');
            if (settings.progressType) {
                $dialog.find('.progress-bar').addClass('progress-bar-' + settings.progressType);
            }
            $dialog.find('h3').text(message);

            if (typeof settings.onHide === 'function') {
                $dialog.off('hidden.bs.modal').on('hidden.bs.modal', function (e) {
                    settings.onHide.call($dialog);
                });
            }

            $dialog.modal();
        },

        hide: function () {
            $dialog.modal('hide');
        }
    };

})(jQuery);