async function OpenFormProduct(idProduct) {
    $.ajax({
        url: "/Product/Form",
        type: "POST",
        data: { idProduct: idProduct },
        success: function (response) {
            if (response.status) {
                new Modal({
                    id: "form-product-modal",
                    html: response.view
                }).Create();
            } else {
                Swal.fire({
                    title: "Atenção",
                    text: response.Message(),
                    icon: "warning"
                });
            }
        },
        error: function (xhr, status, error) {
            Swal.fire({
                title: "Atenção",
                text: `Não foi possível completar a ação: ${error}`,
                icon: "error"
            });
        }
    });
}