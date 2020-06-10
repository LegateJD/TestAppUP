(function () {
    const basketContainer = document.getElementById("basket-container");

    basketContainer.addEventListener("click", deleteOrderDetails);

    async function deleteOrderDetails(event) {
        if (event.target.tagName === "BUTTON" && event.target.hasAttribute("data-product-id")) {
            let url = "api/orderDetails"
            const model = {
                productId: event.target.getAttribute("data-product-id")
            };

            await fetch(url, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(model)
            });

            url = "basket/table";
            const response
                = await fetch(url, {
                    method: 'GET'
                });

            const table = await response.text();
            basketContainer.innerHTML = table;
        }
    }
}())