$(function() {
    $('#paginationForm').submit(function(event) {
        event.preventDefault();

        var formData = $(this).serialize();
        var url = '@Url.Action("GetAllProducts", "Products")';

        $.get(url, formData, function(data) {
            $('#productsContainer').html(data);
        });
    });
});