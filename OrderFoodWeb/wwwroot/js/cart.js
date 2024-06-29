let cartCount = 0;

function openPopup(product) {
    var popup = document.getElementById("popup");

    // Set the product details in the popup
    document.getElementById("popupProductImage").src = product.imageUrl;
    document.getElementById("popupProductName").textContent = product.name;
    document.getElementById("popupProductDescription").textContent = product.description;

    // Set the product ID in the hidden field
    document.getElementById("productId").value = product.id;

    popup.style.display = "block";
    document.addEventListener("mousedown", closeIfClickOutside);
}

function closePopup() {
    var popup = document.getElementById("popup");
    popup.style.display = "none";
    document.removeEventListener("mousedown", closeIfClickOutside);
}

function closeIfClickOutside(event) {
    var popup = document.getElementById("popup");
    var popupContent = document.querySelector(".popup-content");

    if (!popupContent.contains(event.target)) {
        closePopup();
    }
}

// Bắt sự kiện khi click vào nút "Add to Cart"
document.addEventListener("DOMContentLoaded", function () {
    var addToCartButtons = document.querySelectorAll(".add-to-cart-button");
    addToCartButtons.forEach(function (button) {
        button.addEventListener("click", function (event) {
            event.preventDefault();

            var menuItem = button.closest(".menu-item");
            var product = {
                id: menuItem.dataset.productId,
                imageUrl: menuItem.querySelector(".menu-item-image").src,
                name: menuItem.querySelector(".menu-item-name").textContent,
                description: menuItem.querySelector(".menu-item-description").textContent
            };

            openPopup(product);
        });
    });

    // Xử lý form khi submit
    var cartForm = document.getElementById("cartForm");
    cartForm.addEventListener("submit", function (event) {
        event.preventDefault();

        // Lấy dữ liệu từ form (size và extras)
        var formData = new FormData(cartForm);
        var productId = formData.get("productId");
        var size = formData.get("size");
        var extras = formData.getAll("extras");

        // Tạo đối tượng sản phẩm từ dữ liệu thu thập được
        var productData = {
            productId: productId,
            imageUrl: document.getElementById("popupProductImage").src,
            name: document.getElementById("popupProductName").textContent,
            description: document.getElementById("popupProductDescription").textContent,
            size: size,
            extras: extras
        };

        console.log("Product Data:", productData);

        // Gửi dữ liệu sản phẩm lên server (ví dụ qua fetch)
        fetch('/Cart/AddToCart', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(productData)
        })
            .then(response => {
                if (response.ok) {
                    return response.json();
                }
                throw new Error('Failed to add product to cart.');
            })
            .then(data => {
                // Xử lý kết quả từ server (nếu cần)
                console.log(data.message); // Ví dụ hiển thị thông báo
                cartCount++; // Tăng số lượng sản phẩm trong giỏ hàng
                document.getElementById("cartCount").textContent = cartCount; // Cập nhật giao diện số lượng giỏ hàng
                closePopup(); // Đóng popup sau khi xử lý
            })
            .catch(error => {
                console.error('Error:', error);
                // Xử lý lỗi (hiển thị thông báo lỗi cho người dùng nếu cần)
            });
    });
});