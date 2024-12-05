$(document).ready(function () {
    // Inicializar DataTables
    var table = new DataTableHelper('#permissionGroupTable', {
        enableExport: false,
        order: [[1, 'asc']],
    }).init();
});

async function OpenDetailPermissionGroup(id) {
    $.ajax({
        url: "/PermissionGroup/Detail",
        type: "POST",
        data: { id: id },
        success: function (response) {
            if (!response.statusErro) {
                new Modal({
                    id: "detail-permission-group-modal",
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