
$("#CategoryId").change(function () {
    var currentId = $(this).val();
    console.log(currentId)
    $.ajax({
        url: "/admin/products/Index/LoadChildCategories?parentId=" + currentId,
        type: "get"
    }).done(function (data) {
        $("#SubCategoryId").html(data);
    });
});
$("#SubCategoryId").change(function () {
    var currentId = $(this).val();
    console.log(currentId)
    $.ajax({
        url: "/admin/products/Index/LoadChildCategories?parentId=" + currentId,
        type: "get"
    }).done(function (data) {
        $("#SecondarySubCategoryId").html(data);
    });
});
function AddRow() {
    var count = $("#rowCount").val();

    for (var i = 0; i < count; i++) {
        $("#table-body").append(
            "<tr>" +
            "<td><input type='text' autocomplete='off' name='Keys' class='form-control'/></td>" +
            "<td><input type='text' autocomplete='off' name='Values' class='form-control'/></td></tr>"
        );
    }
}


function activeAddress(addressId) {
    console.log("start custom.js");
    console.log("start sweat alert");


    Swal.fire({
        title: "آیا از انجام عملیات اطمینان دارید ؟",
        icon: "info",
        confirmButtonText: "بله ، مطمعا هستم",
        cancelButtonText: "خیر",
        showCancelButton: true,
    }).then((result) => {
        console.log(result);

        if (result.isConfirmed) {
            $.ajax({
                url: "/profile/addresses/SetActiveAddress?addressId=" + addressId,
                beforeSend: function () {
                    $(".loading").show();
                },
                complete: function () {
                    $(".loading").hide();
                },
            }).done(function (data) {
                console.log(data);
                var res = JSON.parse(data);
                console.log(data);

                if (res.Status === 1) {
                    console.log(res.Status);

                    Success("", res.Message, true);
                } else {
                    ErrorAlert("", res.Message, res.isReloadPage);
                }
            })
        }
    });
}

function addToCart(inventoryId, count) {
    var token = $("#ajax-token input[name='__RequestVerificationToken']").val();

    $.ajax({
        url: `/shopcart/addItem?inventoryId=${inventoryId}&count=${count}`,
        type: "post",
        data: {
            __RequestVerificationToken: token
        },
        beforeSend: function (xhr) {
            $(".loading").show();
        },
        complete: function () {
            $(".loading").hide();
        },
    }).done(function (data) {
        var res = JSON.parse(data);
        CallBackHandler(res);
    });
}


//function changePageId(pageId) {
//    var url = new URL(window.location.href);
//    var search_params = url.searchParams;
//    search_params.set('FilterParams.PageId', pageId);
//    url.search = search_params.toString();
//    var new_url = url.toString();
//    window.location.replace(new_url);
//}

function changePageId(pageId) {
    var url = new URL(window.location.href);
    var search_params = url.searchParams;
    search_params.set('filterParams.pageId', pageId);
    url.search = search_params.toString();
    var new_url = url.toString();
    window.location.replace(new_url);
}