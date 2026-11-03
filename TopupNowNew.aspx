<!-- Your existing branding, fonts, colors, layout remain unchanged -->

<table>
    <tr>
        <td style="vertical-align:top">
            <span style="color: #3C7FAF;font-family:Arial;font-weight:bold;font-size: 1.2em;">
                One-time purchase of simplyFYI UNITS
            </span>
            <span class="instruction"><br />Refill your balance with a one-time payment.</span>
        </td>
    </tr>

    <tr>
        <td style="padding-top:10px">
            <select id="unitSelector">
                <option value="1000" data-price="10.00">1000 units $10.00 USD</option>
                <option value="2000" data-price="20.00">2000 units $20.00 USD</option>
                <option value="3000" data-price="30.00">3000 units $30.00 USD</option>
                <option value="5000" data-price="50.00">5000 units $50.00 USD</option>
                <option value="7500" data-price="75.00">7500 units $75.00 USD</option>
                <option value="10000" data-price="100.00">10000 units $100.00 USD</option>
            </select>
        </td>
    </tr>
</table>

<!-- Replace legacy Buy Now button with PPCP JS SDK button -->
<div id="paypal-button-container" style="margin-top:15px;"></div>

<script src="https://www.paypal.com/sdk/js?client-id=YOUR_CLIENT_ID&currency=USD&intent=capture&data-partner-attribution-id=SIMPLYFYI_SP_PPCP"></script>

<script>
    paypal.Buttons({
        createOrder: function() {
            const selector = document.getElementById("unitSelector");
            const units = selector.value;
            const price = selector.options[selector.selectedIndex].dataset.price;

            return fetch("/api/paypal/create-order", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    units: units,
                    price: price,
                    custom: "<%=CustomValue %>"
                })
            })
            .then(res => res.json())
            .then(data => data.orderID);
        },

        onApprove: function(data) {
            return fetch("/api/paypal/capture-order", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ orderID: data.orderID })
            })
            .then(res => res.json())
            .then(result => {
                window.location.href = "<%=PaymentCompletedReturnUrl %>";
            });
        },

        onCancel: function() {
            window.location.href = "<%=PaymentCanceledReturnUrl %>";
        },

        onError: function(err) {
            console.error(err);
            alert("An error occurred while processing your payment.");
        }
    }).render('#paypal-button-container');
</script>
