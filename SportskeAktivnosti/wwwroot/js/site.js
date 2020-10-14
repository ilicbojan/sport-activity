function confirmDelete(uniqueId, isDeleteClicked) {
    var deleteSpan = 'deleteSpan_' + uniqueId;
    var confirmDeleteSpan = 'confirmDeleteSpan_' + uniqueId;

    if (isDeleteClicked) {
        $('#' + deleteSpan).hide();
        $('#' + confirmDeleteSpan).show();
    }
    else {
        $('#' + deleteSpan).show();
        $('#' + confirmDeleteSpan).hide();
    }
}

$(document).ready(function () {
    /* Scroll on buttons */
    $('.js--scroll-to-objects').click(function () {
        $('html, body').animate({ scrollTop: $('.js--section-objects').offset().top }, 1000);
    });

    /* Scroll on buttons */
    $('.js--scroll-to-contact').click(function () {
        $('html, body').animate({ scrollTop: $('.js--section-contact').offset().top }, 1000);
    });
});