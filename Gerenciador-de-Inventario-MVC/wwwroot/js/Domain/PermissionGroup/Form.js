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
            url: "/PermissionGroup/Save",
            data: $form.serializeArray(),
            success: function (response) {
                if (!response.statusErro) {
                    // Tudo certo, atualizando a tabela e fechando a modal
                    $("#idDivTablePermissionGroup").html(response.view);
                    $("#form-permission-group-modal #closePermissionGroupForm").click();

                    Swal.fire({
                        title: "Sucesso",
                        text: response.message,
                        icon: "success"
                    });
                } else {
                    if (response.view)
                        $("#form-employee-modal #idModalBodyFormPermissionGroup").html(response.view); // Atualizo a <div> com as mensagens os dados que foram invalidado no servidor

                    // Mensagem de Erro
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

