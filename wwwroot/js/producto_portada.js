document.addEventListener('DOMContentLoaded', function () {
    const selectArchivo = document.getElementById('ArchivoId');
    const imgPortada = document.getElementById('imgPortada') 
                       || document.querySelector('.portada');

    if (!selectArchivo || !imgPortada) return;

    const baseUrl = imgPortada.dataset.url;
    const placeholder = 'https://via.placeholder.com/300x450';

    function actualizarPreview() {
        const archivoId = selectArchivo.value;

        if (!archivoId) {
            imgPortada.src = placeholder;
        } else {
            imgPortada.src = `${baseUrl}/api/archivos/${archivoId}`;
        }
    }

    selectArchivo.addEventListener('change', actualizarPreview);

    // por si ya viene algo seleccionado (en Editar)
    actualizarPreview();
});
