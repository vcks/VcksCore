$(function () {
    var modal = $('#ImageViewer');
    $('.MaximizeImage').click(function () {
        modal.css('display', 'block');
        $('#ImageViewerBig').attr('src', $(this).data('max'));
        $('#ImageViewerCaption').html(this.alt);
    });
    $('.ImageViewerClose').click(function () {
        modal.css('display', 'none');
    });
});
