(function () {
    const stripe = Stripe('pk_test_51Gs62EFNresQ3SgULVbAEuS99Zh8ZXT9HnAszQsXLCfK07xvZ4c6bfORnUJlUKWMuyTcDBHZnDtDXIzxLMsG1H7F00UMkNBkmn');
    const elements = stripe.elements();
    const cardSpinner = document.getElementById("card-spinner");
    const errorElement = document.getElementById('card-errors');
    const card = elements.create('cardNumber', {
        classes: {
            base: 'form-control',
            invalid: 'error'
        }
    });
    const resultInput = document.getElementById("result-input");

    const cardCvc = elements.create('cardCvc', {
        classes: {
            base: 'form-control',
            invalid: 'error'
        }
    });

    const cardExp = elements.create('cardExpiry', {
        classes: {
            base: 'form-control',
            invalid: 'error'
        }
    });

    card.mount('#card-element');
    cardCvc.mount('#card-cvc');
    cardExp.mount('#card-exp');
    cardSpinner.classList.add("d-none");

    var form = document.getElementById('payment-form');
    form.addEventListener('submit', function (event) {
        event.preventDefault();

        stripe.createToken(card).then(function (result) {
            if (result.error) {
                errorElement.textContent = result.error.message;
            } else {
                stripeTokenHandler(result.token);
            }
        });
    });

    async function stripeTokenHandler(token) {
        const firstName = form.getAttribute("data-first-name");
        const lastName = form.getAttribute("data-last-name");
        const address = form.getAttribute("data-address");
        const shipmentMethod = form.getAttribute("data-shipment-method");
        const url = "/api/charges"
        const model = {
            token: token.id,
            order: {
                shipmentMethod: Number.parseInt(shipmentMethod),
                customer: {
                    firstName: firstName,
                    lastName: lastName,
                    address: address
                }
            }
        };
        cardSpinner.classList.toggle("d-none");

        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            redirect: 'follow',
            body: JSON.stringify(model)
        });

        const charge = await response.json();
        cardSpinner.classList.toggle("d-none");

        if (charge.isSuccessful === true) {
            const resultInput = document.getElementById("result-input");
            resultInput.value = true;
            form.submit();
        }
        else {
            errorElement.textContent = charge.error;
        }
    }
}())