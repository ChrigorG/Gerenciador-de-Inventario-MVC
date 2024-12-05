async function OpenFormPermissionGroup(id) {
    $.ajax({
        url: "/PermissionGroup/Form",
        type: "POST",
        data: { id: id },
        success: function (response) {
            if (!response.statusErro) {
                new Modal({
                    id: "form-permission-group-modal",
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