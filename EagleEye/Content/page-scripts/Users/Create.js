var createUser = function () {
    var me = this;
    this.createUserUrl = null;
    var isDuplicateEmail = false;
    var $croppableImage = null;

    var createUserForm = function () {

        $("#CreateUserForm").validate({
            errorPlacement: function (error, element) {
                if (element[0].name == 'Email') {
                    error.appendTo(element.parent().parent());
                }
                else {
                    error.appendTo(element.parent().parent());
                }
            },
            rules: {
                UserName: { required: true, letterswithspace: true },
                Email: { required: true, emailvalidatecustom: true },
            },
           
            submitHandler: function (form) {
                var f = $(form);
                var data = f.serializeArray();              
                if (isDuplicateEmail && $("#UserId").val() == "0")
                    return false;

                $.ajax({
                    type: f[0].method,
                    url: f[0].action,
                    data: data,
                    dataType: 'json',
                    success: function (data, strStatus) {
                        showMessage(data.Message, data.Data, data.Type);
                        //setTimeout(function () {
                        //    if (data.Success)
                        //        window.location.href = "/Users/Create/" + data.OptionalValue;
                        //}, 2000)
                    },
                    error: handleAjaxError()
                });
            }
        });
    }
    
    

    $("#Email").blur(function () {
        var email = $(this);
        validateDuplicateEmail(email);
    });

    var validateDuplicateEmail = function (email) {
        if ($("#UserId").val() == "0") {
            $.ajax({
                type: 'GET',
                data: { email: email.val() },
                url: "/Users/CheckEmailAvailability",
                dataType: 'json',
                success: function (data) {
                    if (data.Data == "1") {
                        $("#isExistEmailError").text(data.Message);
                        isDuplicateEmail = true;
                    }
                    else {
                        $("#isExistEmailError").text("");
                        isDuplicateEmail = false;
                    }
                },
                error: handleAjaxError()
            });
        }
    }

    var updatePassword = function (userId, password) {
        $.ajax({
            type: 'POST',
            url: '/Users/UpdatePassword',
            data: { userId: userId, password: password },
            dataType: 'json',
            success: function (data, strStatus) {
                if (data.Success) {
                    $("#Password").val(password);
                }
                showMessage(data.Message, data.Data, data.Type);
            }
        });

    }


    this.init = function () {
        createUserForm();

      
        $("#btnUpdatePassword").click(function () {
            var oldPassword = $("#Password").val();
            var currentPassword = $("#CurrentPassword").val();
            var newPassword = $("#NewPassword").val();
            var confirmPassword = $("#ConfirmPassword").val();

            if (currentPassword == "") {
                showMessage("Warning!", "Enter current password.", "notice");
                return false;
            }

            if (newPassword == "") {
                showMessage("Warning!", "Enter new password.", "notice");
                return false;
            }


            if (confirmPassword == "") {
                showMessage("Warning!", "Enter confirm password.", "notice");
                return false;
            }

            if (oldPassword != currentPassword) {
                showMessage("Warning!", "Current password is wrong.", "notice");
                return false;
            }
            if (newPassword != confirmPassword) {
                showMessage("Warning!", "New password and confirm password not matched.", "notice");
                return false;
            }

            if (oldPassword == newPassword) {
                showMessage("Warning!", "Password should not be same as old password.", "notice");
                return false;
            }

            updatePassword($("#UserId").val(), newPassword);

        });
        $("#createFormContainer").on("click", "button", function () {
            var action = $(this).data("action");
            var id = $(this).data('id');
            switch (action) {
                case "cancel":
                    window.location.href = "/Users/index";
                    break;
            }
        });


    }
}