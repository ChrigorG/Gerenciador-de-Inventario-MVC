async function OpenFormEmployee(id) {
    $.ajax({
        url: "/Employee/Form",
        type: "POST",
        data: { id: id },
        success: function (response) {
            if (response.status) {
                new Modal({
                    id: "form-employee-modal",
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