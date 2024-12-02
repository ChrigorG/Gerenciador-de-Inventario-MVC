$(() => {
    const $form = $("#form-permission-group-modal #permissionGroupForm");

    $("#form-permission-group-modal #submitPermissionGroupForm").on("click", function () {
        $form.submit();
    });

    $form.on("submit", async (event) => {
        // Impede o envio padrão do formulário
        event.preventDefault();

        $.ajax({
            type: "POST",
            url: "/PermissionGroup/Form",
            data: $form.serializeArray(),
            success: function (response) {
                if (!response.statusErro) {
                    $("#idDivTablePermissionGroup").html(response.view);
                    $("#form-permission-group-modal #closePermissionGroupForm").click();

                    Swal.fire({
                        title: "Sucesso",
                        text: response.message,
                        icon: "success"
                    });
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
    });
})

