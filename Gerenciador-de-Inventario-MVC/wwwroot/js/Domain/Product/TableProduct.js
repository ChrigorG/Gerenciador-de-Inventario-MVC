$(document).ready(function () {
    // Inicializar DataTables
    var table = new DataTableHelper('#productsTable', {
        enableExport: false,
        order: [[1, 'asc']],
    }).init();
});

async function OpenDetailProduct(id) {
    $.ajax({
        url: "/Product/Detail",
        type: "POST",
        data: { id: id },
        success: function (response) {
            if (!response.statusErro) {
                new Modal({
                    id: "detail-product-modal",
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