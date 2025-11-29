document.addEventListener('DOMContentLoaded', () => {
    const modalEl = document.getElementById('addToCartModal');
    if (!modalEl) return;

    const bsModal = new bootstrap.Modal(modalEl);

    const btnGoToCart = document.getElementById('btnGoToCart');
    if (btnGoToCart) {
        btnGoToCart.addEventListener('click', () => {
            window.location.href = '/Comprar/Carrito';
        });
    }

    document.querySelectorAll('.js-add-to-cart-form').forEach(form => {
        form.addEventListener('submit', async (e) => {
            e.preventDefault();

            const formData = new FormData(form);

            try {
                const response = await fetch(form.action, {
                    method: 'POST',
                    body: formData,
                    headers: { 'X-Requested-With': 'XMLHttpRequest' }
                });

                if (!response.ok) {
                    throw new Error('Error al agregar al carrito');
                }

                const title = form.dataset.productTitle || '';
                const image = form.dataset.productImage || '';

                modalEl.querySelector('.js-product-title').textContent = title;
                if (image) {
                    document.getElementById('addToCartImage').src = image;
                }

                bsModal.show();
            } catch (err) {
                console.error(err);
                alert('No se pudo agregar al carrito, intenta de nuevo.');
            }
        });
    });
});
