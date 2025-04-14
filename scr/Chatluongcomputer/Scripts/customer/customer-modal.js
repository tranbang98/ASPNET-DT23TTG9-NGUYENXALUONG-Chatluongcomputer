$(document).ready(function () {
    // Mở modal khi bấm nút có class 'open-modal'
    $(document).on("click", ".open-modal", function () {
        const url = $(this).data("url");
        $("#adminModalBody").html("Đang tải...");
        $("#adminModal").modal("show");

        $.get(url, function (data) {
            $("#adminModalBody").html(data);
        });
    });

    // Gửi form tạo khách hàng (form trong modal)
    $(document).on("submit", "#createForm", function (e) {
        e.preventDefault();
        const form = $(this);

        $.ajax({
            type: "POST",
            url: form.attr("action"),
            data: form.serialize(),
            success: function () {
                location.reload(); // reload lại danh sách
            },
            error: function () {
                alert("❌ Có lỗi xảy ra khi thêm khách hàng.");
            }
        });
    });

    // Có thể mở rộng cho các form khác như #editForm, #deleteForm ở đây
});
