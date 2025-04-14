$(document).on("click", ".openModal", function () {
    const type = $(this).data("type");
    const id = $(this).data("id");

    let title = "", url = "";
    if (type === "delete") {
        title = "Xác nhận xóa";
        url = `/Admin/Delete/${id}`;
    } else if (type === "details") {
        title = "Chi tiết người dùng";
        url = `/Admin/Details/${id}`;
    } else {
        alert("❌ Loại thao tác không hợp lệ!");
        return;
    }

    $("#adminModal .modal-title").text(title);
    $("#adminModalBody").html("<p class='text-muted'>Đang tải nội dung...</p>");
    $("#adminModal").modal("show");

    // Load nội dung vào modal
    $.get(url, function (html) {
        $("#adminModalBody").html(html);

        // ✅ Delay để DOM thực sự gắn xong trước khi tìm form
        setTimeout(() => {
            const $form = $("#deleteForm");
            if ($form.length === 0) {
                console.error("❌ Không tìm thấy form #deleteForm trong DOM.");
                return;
            }

            console.log("✅ Tìm thấy #deleteForm, gắn submit...");

            $form.off("submit").on("submit", function (e) {
                e.preventDefault();

                $.ajax({
                    type: "POST",
                    url: $form.attr("action"),
                    data: $form.serialize(),
                    success: function (res) {
                        console.log("📥 Server response:", res);
                        if (res.success) {
                            showToast(res.message, "success");
                            $("#adminModal").modal("hide");
                            setTimeout(() => location.reload(), 800);
                        } else {
                            showToast(res.message || "❌ Xóa không thành công", "danger");
                        }
                    },
                    error: function (xhr) {
                        console.error("❌ AJAX error:", xhr.responseText);
                        showToast("❌ Kết nối thất bại.", "danger");
                    }
                });
            });
        }, 50); // 👈 Độ trễ 50ms là đủ để DOM update
    });
});
