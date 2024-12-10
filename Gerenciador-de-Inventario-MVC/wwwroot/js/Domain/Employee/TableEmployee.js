$(document).ready(function () {
    // Inicializar DataTables
    var table = new DataTableHelper('#employeeTable', {
        enableExport: false,
        order: [[1, 'asc']],
    }).init();
});

async function OpenDetailEmployee(id) {
    $.ajax({
        url: "/Employee/Detail",
        type: "POST",
        data: { id: id },
        success: function (response) {
            if (!response.statusErro) {
                new Modal({
                    id: "detail-employee-modal",
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