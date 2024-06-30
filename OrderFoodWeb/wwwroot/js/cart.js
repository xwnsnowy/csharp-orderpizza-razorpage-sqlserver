let cartCount = 0;

function openPopup(product) {
    var popup = document.getElementById("popup");

    // Set the product details in the popup
    document.getElementById("popupProductImage").src = product.imageUrl;
    document.getElementById("popupProductName").textContent = product.name;
    document.getElementById("popupProductDescription").textContent = product.description;

    // Set the product ID in the hidden field
    document.getElementById("productId").value = product.id;
    document.getElementById("basePrice").value = product.basePrice;

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

document.addEventListener("DOMContentLoaded", function () {
    var addToCartButtons = document.querySelectorAll(".add-to-cart-button");
    addToCartButtons.forEach(function (button) {
        button.addEventListener("click", function (event) {
            event.preventDefault();

            var menuItem = button.closest(".menu-item");
            var product = {
                id: parseInt(menuItem.dataset.productId), 
                imageUrl: menuItem.querySelector(".menu-item-image").src,
                name: menuItem.querySelector(".menu-item-name").textContent,
                description: menuItem.querySelector(".menu-item-description").textContent,
                basePrice: parseFloat(menuItem.dataset.productPrice)
            };

            openPopup(product);
        });
    });

    var cartForm = document.getElementById("cartForm");
    cartForm.addEventListener("submit", function (event) {
        event.preventDefault();
        var productId = parseInt(document.getElementById("productId").value);
        var basePrice = parseFloat(document.getElementById('basePrice').value);

        var size = document.querySelector('input[name="size"]:checked').value;
        var extras = [];
        document.querySelectorAll('input[name="extras"]:checked').forEach(function (checkbox) {
            extras.push(checkbox.value);
        });

        const extraPrices = {
            "Extra cheese": 1,
            "Extra pepperoni": 2
        };

        var productData = {
            Id: productId,
            ImageUrl: document.getElementById("popupProductImage").src,
            Name: document.getElementById("popupProductName").textContent,
            Description: document.getElementById("popupProductDescription").textContent,
            Size: size,
            BasePrice: basePrice,
            Extras: extras.map(extra => {
                return {
                    Name: extra,
                    Price: extraPrices[extra]
                };
            })
        };



        console.log("Product Data:", productData);

        const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

        fetch('/Cart/Index?handler=AddToCart', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'XSRF-TOKEN': token
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
                console.log(data.message);
                cartCount++;
                document.getElementById("cartCount").textContent = cartCount;
                closePopup();
            })
            .catch(error => {
                console.error('Error:', error);
            });
    });
});
