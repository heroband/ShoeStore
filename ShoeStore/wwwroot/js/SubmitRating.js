function submitRating(value) {
    const stars = document.querySelectorAll('.star');
    stars.forEach((star, index) => {
        if (index < value) {
            star.classList.remove('text-muted');
            star.classList.add('text-warning');
        } else {
            star.classList.add('text-muted');
            star.classList.remove('text-warning');
        }
    });

    document.getElementById('rating').value = value;
    document.getElementById('ratingForm').submit();
}