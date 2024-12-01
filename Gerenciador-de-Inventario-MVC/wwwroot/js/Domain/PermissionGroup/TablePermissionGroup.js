$(document).ready(function () {
    // Inicializar DataTables
    var table = new DataTableHelper('#permissionGroupTable', {
        enableExport: false,
        order: [[1, 'asc']],
    }).init();
});

async function OpenFormPermissionGroup(idPermissionGroup) {
    $.ajax({
        url: "/PermissionGroup/Form",
        type: "POST",
        data: { idPermissionGroup: idPermissionGroup },
        success: function (response) {
            if (response.status) {
                new Modal({
                    id: "form-permission-group-modal",
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

async function OpenDetailPermissionGroup(idPermissionGroup) {
    $.ajax({
        url: "/PermissionGroup/Detail",
        type: "POST",
        data: { idPermissionGroup: idPermissionGroup },
        success: function (response) {
            if (response.status) {
                new Modal({
                    id: "detail-permission-group-modal",
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