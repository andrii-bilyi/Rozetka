document.addEventListener('DOMContentLoaded', () => {
    //вхід
    const authButton = document.getElementById("auth-button"); // кнопка входа на модальном окне
    if (authButton) authButton.addEventListener('click', authButtonClick);
    //вихід
    const signoutButton = document.getElementById("auth-signout-button-nav"); // кнопка выхода nav-панели
    if (signoutButton) signoutButton.addEventListener('click', signOutButtonClick);

    //const signoutButton2 = document.getElementById("auth-signout-button-offcanvas");
    //if (signoutButton2) signoutButton2.addEventListener('click', signOutButtonClick); // кнопка выхода offcanvas-панели

    //const signoutButton3 = document.getElementById("auth-signout-button-personal");
    //if (signoutButton3) signoutButton3.addEventListener('click', signOutButtonClick); // кнопка выхода offcanvas-панели
    //////зміни
    ////const saveProfileButton = document.getElementById("profile-save-button");
    ////if (saveProfileButton) saveProfileButton.addEventListener('click', saveProfileButtonClick);

    ////видалення
    //const deleteProfileButton = document.getElementById("profile-delete-button");
    //if (deleteProfileButton) deleteProfileButton.addEventListener('click', deleteProfileButtonClick);
    ////зміна паролю
    //const changePasswordButton = document.getElementById("changePassword-button");
    //if (changePasswordButton) changePasswordButton.addEventListener('click', changePasswordButtonClick); //feedback-button

    //////м'яке видалення
    ////const softRemovalProfileButton = document.getElementById("profile-softRemoval-button");
    ////if (softRemovalProfileButton) softRemovalProfileButton.addEventListener('click', softRemovalProfileButtonClick);


    ////инициализирование всплывающих подсказок
    //const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
    //const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))

});

function authButtonClick() {//вход в аккаунт
    const emailInput = document.getElementById("auth-email");
    if (!emailInput) throw "Element #auth-email not found";
    const email = emailInput.value.trim();
    if (email.length == 0) {
        showAuthMessage("Email не може бути порожнім");
        return;
    }
    const passwordInput = document.getElementById("auth-password");
    if (!passwordInput) throw "Element #auth-password not found";
    const password = passwordInput.value.trim();
    if (password.length == 0) {
        showAuthMessage("Пароль не може бути порожнім");
        return;
    }

    fetch(`/api/auth/login?email=${email}&password=${password}`)
        .then(r => {
            if (r.status == 200) { // ok                            

                alert("Вхід виконано!");
                //showAuthMessage("Вхід виконано!");
                window.location.reload();
            }
            else if (r.status == 410) {
                alert("Аккаунт призупинено!");
            }
            else { // 401
                alert("Вхід відхилено!");
            }
        });

}