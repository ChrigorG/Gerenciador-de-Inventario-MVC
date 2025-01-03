﻿$(document).ready(function () {
    // Inicializar DataTables
    var table = new DataTableHelper('#stockMovementsTable', {
        enableExport: true,
        order: [[1, 'asc']],
    }).init();
});

async function OpenDetailStockMovements(id) {
    $.ajax({
        url: "/StockMovements/Detail",
        type: "POST",
        data: { id: id },
        success: function (response) {
            if (!response.statusErro) {
                new Modal({
                    id: "detail-stock-movements-modal",
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