function validateForm() {
    var form = document.getElementById('checkoutForm');
    var inputs = form.querySelectorAll('input[required]');
    var isValid = true;

    var phoneRegex = /^\d{10,11}$/;
    var emailRegex = /^\S+@\S+\.\S+$/;

    inputs.forEach(function (input) {
        var errorId = input.id + '-error';
        var errorMessage = '';

        if (!input.value.trim()) {
            errorMessage = 'Vui lòng nhập thông tin.';
            isValid = false;
        } else if (input.id === 'phone' && !phoneRegex.test(input.value)) {
            errorMessage = 'Số điện thoại không hợp lệ.';
            isValid = false;
        } else if (input.id === 'email' && !emailRegex.test(input.value)) {
            errorMessage = 'Email không hợp lệ.';
            isValid = false;
        }

        document.getElementById(errorId).textContent = errorMessage;
        input.classList.toggle('invalid-field', errorMessage !== '');
    });

    return isValid;
}