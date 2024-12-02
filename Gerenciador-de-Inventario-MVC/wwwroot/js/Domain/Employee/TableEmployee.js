$(document).ready(function () {
    // Inicializar DataTables
    var table = new DataTableHelper('#employeeTable', {
        enableExport: false,
        order: [[1, 'asc']],
    }).init();

    // Selecionar todos os checkboxes
    $('#selectAll').on('click', function () {
        var rows = table.rows({ 'search': 'applied' }).nodes();
        $('input[type="checkbox"].selectItem', rows).prop('checked', this.checked);
    });

    // Atualizar o checkbox principal ao alterar qualquer checkbox individual
    $('#employeeTable tbody').on('change', 'input[type="checkbox"].selectItem', function () {
        if (!this.checked) {
            var el = $('#selectAll').get(0);
            if (el && el.checked && ('indeterminate' in el)) {
                el.indeterminate = true;
            }
        }
    });
});

async function OpenDetailEmployee(id) {
    $.ajax({
        url: "/Employee/Detail",
        type: "POST",
        data: { id: id },
        success: function (response) {
            if (response.status) {
                new Modal({
                    id: "detail-employee-modal",
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