function confirmDelete(producttId) {
    if (confirm("Are you sure you want to delete this product?")) {
        window.location.href = "/products/" + productId + "/Delete"
    }
}