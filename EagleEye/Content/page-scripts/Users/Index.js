var listUser = function () {
    var me = this;
    this.getUserUrl = null;
    this.deleteUserUrl = null;
    this.editUserUrl = null;
    this.userId = null;   

    var loadUserList = function () {
        $("#tableUser").DataTable({
            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            //"scrollX": "auto",
            "scrollCollapse": true,
            "lengthMenu": [10, 25, 50, 100],
            "pageLength": 50,
            "autoWidth": true,
            "order": [[0, "asc"]],
            "ajax": {
                "url": me.getUserUrl,
                "type": "POST",
                "datatype": "json",
                "data": { "filter":"0" }
            },
            "columns": [
                { "data": "UserId", "name": "UserId", "autoWidth": true, "visible": true },
                {
                    "data": null,
                    "name": "UserName",
                    "autoWidth": true,
                    "className": "dt-body-left",
                    render: function (data, type, row) {                       
                            return data.UserName;
                    }
                },
                { "data": "Email", "name": "Email", "autoWidth": true, "className": "dt-body-left" },
                { "data": "Phone", "name": "Phone", "autoWidth": true, "className": "dt-body-left" },
                {
                    "data": "UserTypeId", "name": "UserTypeId", "autoWidth": true, "className": "dt-body-left",
                    render: function (index, type, data) {                        
                        if (data.UserTypeId == 1)
                            return "Admin";
                        else
                            return "User";
                    }
                },
                {
                    "data": "DOB", "name": "DOB", "autoWidth": true, "className": "dt-body-left",
                    render: function (index, type, data) {
                        var DOB = '';                        
                        if (data.DOB != null)
                            DOB = formatJsonDate(data.DOB);
                        return DOB
                    }
                },
                { "data": "Gender", "name": "Gender", "autoWidth": true, "className": "dt-body-left" },
                {
                    "data": null,
                    "name": "",
                    "orderable": false,
                    "className": "center",
                    "autoWidth": true,
                    "width": "10%",
                    render: function (data, type, row) {
                        var EditButtons = ' <a href="javascript:void(0)" data-action="edit" data-id="' + data.UserId + '" ><i class="fa fa-pencil-square-o font20"></i></a> &nbsp;'
                        var DeleteButton = ' <a href="javascript:void(0)" data-action="delete" data-id="' + data.UserId + '" class="delete-row color-danger"><i class="fa fa-trash-o font20"></i></a>'
                        if (data.UserTypeId==1) {
                            return EditButtons + '<a href="javascript:void(0)" data-action="access-notify" class="color-danger"> <i class="fa fa-trash-o font20" ></i></a>';
                        }
                        else {
                            return EditButtons + DeleteButton;
                        }
                    }
                }
            ],
            "initComplete": function (settings, json) {
                $(this.api().table().header()).find('th').css({ 'padding': '6px 18px' });              
            }
        });
    }

    var accessNotify = function () {
        PNotify.error({ title: 'Failed', text: 'You can not delete your account.' });
    }


    this.init = function () {
        
        $("#tableUser").on("click", "a", function () {
            var action = $(this).data("action");
            var id = $(this).data('id');
            switch (action) {
                case "edit":
                    window.location.href = "/Users/Create/" + id;
                    break;
                case "delete":
                    deleteRecord(me.deleteUserUrl, { id: id }, "tableUser");
                    break;
                case "access-notify":
                    accessNotify();
                    break;
            }
        });      

        loadUserList();
    }
}