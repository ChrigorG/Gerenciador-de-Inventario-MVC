async function OpenFormStockMovements() {
    $.ajax({
        url: "/StockMovements/Form",
        type: "POST",
        success: function (response) {
            if (!response.statusErro) {
                new Modal({
                    id: "form-stock-movements-modal",
                    html: response.view
                }).Create();
            } else {
                Swal.fire({
                    title: "Atenção",
                    text: response.message,
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