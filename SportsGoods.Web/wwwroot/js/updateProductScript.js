document.addEventListener('DOMContentLoaded', function () {
    const updateButtons = document.querySelectorAll('.update-btn');

    updateButtons.forEach(button => {
        button.addEventListener('click', async function () {
            const productId = button.dataset.productId;
            const product = await getProductDetails(productId);

            populateModal(product);

            showModal();
        });
    });

    async function getProductDetails(productId) {
        try {
            const response = await fetch(`/products/${productId}`);
            if (!response.ok) { 
                throw new Error('Failed to fetch product details');
            }
            const product = await response.json();
            return product;
        } catch (error) {
            console.error('Error fetching product details:', error);
        }
    }

    function populateModal(product) {
        document.getElementById('titleField').value = product.title;
        document.getElementById('descriptionField').value = product.description;
        document.getElementById('brandField').value = product.description;
        document.getElementById('priceField').value = product.price;
        document.getElementById('quantityField').value = product.quantity;
        document.getElementById('categoryField').value = product.productCategory;
    }

    function showModal() {
        const modal = document.getElementById('updateModal');
        modal.classList.add('show'); 
    }
});
