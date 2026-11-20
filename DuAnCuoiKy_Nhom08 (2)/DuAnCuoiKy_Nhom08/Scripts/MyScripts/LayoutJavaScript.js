document.addEventListener("DOMContentLoaded", function () {
    // 1. SETTING MỞ / ĐÓNG
    const settingBtn = document.getElementById("settingBtn");
    const settingMenu = document.getElementById("settingMenu");
    const settingClose = document.getElementById("settingClose");
    if (settingBtn && settingMenu) {
        settingBtn.addEventListener("click", function (e) {
            e.preventDefault();
            e.stopPropagation();
            settingMenu.classList.toggle("active");
            settingBtn.setAttribute(
                "aria-expanded",
                settingMenu.classList.contains("active")
            );
        });
        if (settingClose) {
            settingClose.addEventListener("click", function (e) {
                e.stopPropagation();
                settingMenu.classList.remove("active");
                settingBtn.setAttribute("aria-expanded", "false");
            });
        }
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
    }
    // 2. GIAO DIỆN SÁNG / TỐI
    const themeButtons = document.querySelectorAll(".theme-btn");
    function setTheme(theme) {
        document.body.classList.toggle(
            "dark-theme",
            theme === "dark"
        );
        themeButtons.forEach(function (btn) {
            btn.classList.toggle(
                "active",
                btn.dataset.theme === theme
            );
        });
        localStorage.setItem("websiteTheme", theme);
    }
    const savedTheme = localStorage.getItem("websiteTheme") || "light";
    setTheme(savedTheme);
    themeButtons.forEach(function (btn) {
        btn.addEventListener("click", function () {
            setTheme(this.dataset.theme);
        });
    });
    // 3. THÔNG BÁO
    const notificationToggle =
        document.getElementById("notificationToggle");
    if (notificationToggle) {
        const savedNotification =
            localStorage.getItem("websiteNotification");
        if (savedNotification !== null) {
            notificationToggle.checked =
                savedNotification === "true";
        }
        notificationToggle.addEventListener("change", function () {
            localStorage.setItem(
                "websiteNotification",
                this.checked
            );
        });
    }
    // 4. NGÔN NGỮ
    const languageSelect =
        document.getElementById("languageSelect");
    if (languageSelect) {
        const savedLanguage =
            localStorage.getItem("websiteLanguage");
        if (savedLanguage) {
            languageSelect.value = savedLanguage;
        }
        languageSelect.addEventListener("change", function () {
            localStorage.setItem(
                "websiteLanguage",
                this.value
            );
            alert(
                this.value === "en"
                    ? "English selected."
                    : "Đã chọn Tiếng Việt."
            );
        });
    }
});
//CHUYỂN ĐỔI ĐĂNG NHẬP / ĐĂNG KÝ 

//KHỞI TẠO 
document.addEventListener("DOMContentLoaded", function () {
    const passwordInput = document.getElementById("passwordhash");
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
    //const formRegister = document.getElementById("formRegister");
    //if (formRegister) {
    //    formRegister.addEventListener("submit", function (e) {
    //        e.preventDefault();
    //        const email = document.getElementById("email").value.trim();
    //        const password = passwordInput.value;
    //        const confirmPassword = confirmInput.value;
    //        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    //        if (!emailRegex.test(email)) {
    //            alert("Email không đúng định dạng!");
    //            return;
    //        }
    //        const validPassword =
    //            password.length >= 8 &&
    //            /[A-Z]/.test(password) &&
    //            /[a-z]/.test(password) &&
    //            /[0-9]/.test(password) &&
    //            /[^A-Za-z0-9]/.test(password);
    //        if (!validPassword) {
    //            alert("Mật khẩu chưa đáp ứng đủ điều kiện!");
    //            return;
    //        }
    //        if (password !== confirmPassword) {
    //            alert("Mật khẩu nhập lại không trùng khớp!");
    //            return;
    //        }
    //        alert("Thông tin hợp lệ! Có thể tiến hành đăng ký.");
    //    });
    //}
    //const formLogin = document.getElementById("formLogin");
    //if (formLogin) {
    //    formLogin.addEventListener("submit", function (e) {
    //        e.preventDefault();
    //        alert("Bạn cần kết nối chức năng đăng nhập với Controller!");
    //    });
    //}
});
// Form LienHe
const contactForm = document.getElementById("contactForm");
if (contactForm) {
    contactForm.addEventListener("submit", function (e) {
        e.preventDefault();
        alert("Gửi tin nhắn thành công! Cảm ơn bạn đã liên hệ.");
        this.reset();
    });
}
