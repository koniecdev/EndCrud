$(document).ready(function () {
    var access_token = "";
    var basePath = "https://localhost:7137/api/pictures/Image/"
    var cookies = document.cookie.split(";");
    for (var i = 0; i < cookies.length; i++) {
        var cookie = cookies[i].trim();
        if (cookie.indexOf("access_token") === 0) {
            access_token = cookie.substring("access_token=".length, cookie.length);
        }
    }
    $.ajax({
        type: 'GET',
        url: 'https://localhost:7137/api/Pictures',
        headers: {
            'Accept': "application/json",
            'Authorization': 'Bearer ' + access_token
        },
        success: function (response) {
            var pictures = response.pictures;
            $.each(pictures, function (index, picture) {
                var img = $('<img>').attr('src', basePath + picture.relativePath.replace(/\\/g, "%2F")).addClass("media-library-img").attr("data-id", picture.id);
                img.appendTo('.thumbnailLibrary');
                img.clone().appendTo('.galleryLibrary');
            });
        },
        error: function (error) {
            console.log(error);
        }
    });


    var lastClicked;
    var shiftPressed = false;
    var ctrlPressed = false;

    $(document).on('click', '.thumbnailLibrary .media-library-img', function (e) {
        $('.thumbnailLibrary .media-library-img').removeClass('selectedThumbnail');
        $(this).addClass('selectedThumbnail');
    });

    $(document).on('click', '.galleryLibrary .media-library-img', function (e) {
        if (shiftPressed) {
            // Shift + click logic
            var start = $('.galleryLibrary .media-library-img').index(lastClicked);
            var end = $('.galleryLibrary .media-library-img').index(this);
            $('.galleryLibrary .media-library-img').slice(Math.min(start, end), Math.max(start, end) + 1).addClass('selected');
        } else if (ctrlPressed) {
            // Ctrl + click logic
            $(this).toggleClass('selected');
        } else {
            // Normal click logic
            $('.galleryLibrary .media-library-img').removeClass('selected');
            $(this).addClass('selected');
        }

        lastClicked = this;
    });

    $(document).on('keydown', function (e) {
        if (e.which === 16) {
            shiftPressed = true;
        }
        if (e.which === 17) {
            ctrlPressed = true;
        }
    });

    $(document).on('keyup', function (e) {
        if (e.which === 16) {
            shiftPressed = false;
        }
        if (e.which === 17) {
            ctrlPressed = false;
        }
    });

    $('form').on('submit', function (e) {
        e.preventDefault(); // prevent the form from submitting

        var selectedIds = $('.media-library-img.selected').map(function () {
            return $(this).data('id');
        }).get();
        var selectedIdsString = selectedIds.join(',');

        var x1 = $(".selectedThumbnail").first().attr("data-id");
        console.log(x1);
        $("#Article_ThumbnailId").val(x1);
        $("#galleryPics").val(selectedIdsString);

        this.submit();
    });


});