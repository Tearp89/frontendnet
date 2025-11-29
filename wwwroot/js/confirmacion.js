document.addEventListener("DOMContentLoaded", () => {
    const modal = new bootstrap.Modal(document.getElementById("confirmModal"));
    const title = document.getElementById("confirmModalTitle");
    const msg = document.getElementById("confirmModalMessage");
    const btnOk = document.getElementById("confirmModalAcceptBtn");

    // Delegación: escucha cualquier botón que tenga data-confirm
    document.body.addEventListener("click", (e) => {
        const target = e.target.closest("[data-confirm]");

        if (!target) return;

        e.preventDefault(); // Evita el submit o navegación

        const message = target.getAttribute("data-confirm") || "¿Estás seguro?";
        const formId = target.getAttribute("data-form"); // ID del form a ejecutar
        const url = target.getAttribute("data-url"); // o URL de redirección

        // Personaliza mensaje
        msg.textContent = message;

        // Limpia listeners previos
        btnOk.replaceWith(btnOk.cloneNode(true));
        const newBtnOk = document.getElementById("confirmModalAcceptBtn");

        newBtnOk.addEventListener("click", () => {
            modal.hide();

            if (formId) {
                document.getElementById(formId)?.submit();
            }
            else if (url) {
                window.location.href = url;
            }
        });

        modal.show();
    });
});
