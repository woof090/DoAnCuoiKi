document.addEventListener("DOMContentLoaded", function () {
    const settingBtn = document.getElementById("settingBtn");
    const settingMenu = document.getElementById("settingMenu");

    if (!settingBtn || !settingMenu) return;

    settingBtn.addEventListener("click", function (e) {
        e.stopPropagation();

        const isOpen = settingMenu.classList.toggle("active");

        settingBtn.setAttribute("aria-expanded", isOpen);
    });

    settingMenu.addEventListener("click", function (e) {
        e.stopPropagation();
    });

    document.addEventListener("click", function () {
        settingMenu.classList.remove("active");
        settingBtn.setAttribute("aria-expanded", "false");
    });

    document.addEventListener("keydown", function (e) {
        if (e.key === "Escape") {
            settingMenu.classList.remove("active");
            settingBtn.setAttribute("aria-expanded", "false");
        }
    });
});

//CHUYỂN ĐỔI ĐĂNG NHẬP / ĐĂNG KÝ 
function showRegister() {
    document.getElementById("loginForm").style.display = "none";
    document.getElementById("registerForm").style.display = "block";
}
function showLogin() {
    document.getElementById("registerForm").style.display = "none";
    document.getElementById("loginForm").style.display = "block";
}
//KHỞI TẠO 
document.addEventListener("DOMContentLoaded", function () {
    const passwordInput = document.getElementById("regPassword");
    const confirmInput = document.getElementById("confirmPassword");
    //KIỂM TRA MẬT KHẨU
    function checkRule(id, condition) {
        const element = document.getElementById(id);
        if (!element) return;
        const originalText = {
            length: "Tối thiểu 8 ký tự",
            uppercase: "Ít nhất 1 chữ in hoa (A-Z)",
            lowercase: "Ít nhất 1 chữ thường (a-z)",
            number: "Ít nhất 1 chữ số (0-9)",
            special: "Ít nhất 1 ký tự đặc biệt (!@#$...)"
        };
        element.classList.toggle("valid", condition);
        element.textContent =
            (condition ? "✓ " : "✗ ") + originalText[id];
    }
    function checkConfirmPassword() {
        const message = document.getElementById("passwordMessage");

        if (!confirmInput || !passwordInput || !message) return false;

        if (confirmInput.value === "") {
            message.textContent = "";
            return false;
        }
        if (confirmInput.value === passwordInput.value) {
            message.style.color = "#16a34a";
            message.textContent = "✓ Mật khẩu trùng khớp";
            return true;
        }
        message.style.color = "#dc2626";
        message.textContent = "✗ Mật khẩu không trùng khớp";
        return false;
    }
    function validatePassword() {
        if (!passwordInput) return;
        const password = passwordInput.value;
        const rulesBox = document.getElementById("passwordRules");
        const isLength = password.length >= 8;
        const isUppercase = /[A-Z]/.test(password);
        const isLowercase = /[a-z]/.test(password);
        const isNumber = /[0-9]/.test(password);
        const isSpecial = /[^A-Za-z0-9]/.test(password);
        checkRule("length", isLength);
        checkRule("uppercase", isUppercase);
        checkRule("lowercase", isLowercase);
        checkRule("number", isNumber);
        checkRule("special", isSpecial);
        const isValid =
            isLength &&
            isUppercase &&
            isLowercase &&
            isNumber &&
            isSpecial;
        if (rulesBox) {
            rulesBox.style.display =
                password.length > 0 && !isValid ? "block" : "none";
        }
        checkConfirmPassword();
    }
    if (passwordInput) {
        passwordInput.addEventListener("input", validatePassword);
    }
    if (confirmInput) {
        confirmInput.addEventListener("input", checkConfirmPassword);
    }
    const formRegister = document.getElementById("formRegister");

    if (formRegister) {
        formRegister.addEventListener("submit", function (e) {
            e.preventDefault();
            const email = document.getElementById("email").value.trim();
            const password = passwordInput.value;
            const confirmPassword = confirmInput.value;
            const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            if (!emailRegex.test(email)) {
                alert("Email không đúng định dạng!");
                return;
            }
            const validPassword =
                password.length >= 8 &&
                /[A-Z]/.test(password) &&
                /[a-z]/.test(password) &&
                /[0-9]/.test(password) &&
                /[^A-Za-z0-9]/.test(password);
            if (!validPassword) {
                alert("Mật khẩu chưa đáp ứng đủ điều kiện!");
                return;
            }
            if (password !== confirmPassword) {
                alert("Mật khẩu nhập lại không trùng khớp!");
                return;
            }
            alert("Thông tin hợp lệ! Có thể tiến hành đăng ký.");
        });
    }
    const formLogin = document.getElementById("formLogin");
    if (formLogin) {
        formLogin.addEventListener("submit", function (e) {
            e.preventDefault();

            alert("Bạn cần kết nối chức năng đăng nhập với Controller!");
        });
    }
});
// Form LienHe
document.getElementById("contactForm")
    .addEventListener("submit", function (e) {
        e.preventDefault();

        alert("Gửi tin nhắn thành công! Cảm ơn bạn đã liên hệ.");

        this.reset();
    });

