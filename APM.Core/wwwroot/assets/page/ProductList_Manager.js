var loadData = function () {
    console.log(APP_CONFIG.BaseAddress);
    pTable = $('#MainTable').DataTable({
        pageLength: 10,
        processing: true,
        serverSide: true,
        filter: true,
        sort: false,
        bInfo: false,
        bAutoWidth: true,
        scrollX: true,
        scrollCollapse: true,
        ajax:
        {
            url: APP_CONFIG.GetAll,
            type: "POST",
            headers: { "Authorization": "Bearer " + localStorage.getItem('token') },
            contentType: "application/json; charset=utf-8",
            dataType: "JSON",
            data: function (d) {
                return JSON.stringify(d);
            }
        },
        columns:
            [
                {
                    "title": "#",
                    "width": "2%",
                    "data": "id",
                    "bSearchable": false,
                    "bSortable": false,
                    "sClass": "text-center",
                    "mRender": function (data, type, full, meta) {
                        return (meta.row + 1 + meta.settings._iDisplayStart);
                    }
                },
                { "data": "product_id", "sClass": "left", "width": "150", "height": "150" },
                { "data": "product_name", "sClass": "left", "bSearchable": true, "bSortable": true, "width": "200", "height": "200" },
                {
                    "data": "image_path", "sClass": "left", "bSearchable": false, "bSortable": false,
                    "mRender": function (data, type, full, meta) {
                        return '<img src="http://localhost:6580' + data + '"height="150" width="250"  />';
                    }
                },
                {
                    "data": "price", "sClass": "right", "bSearchable": true, "bSortable": true, "width": "100", "height": "150"
                    //"mRender": function (data, type, full, meta) {
                    //    return data.toLocaleString('vi', { style: 'currency', currency: 'VND' });
                    //}
                },
                {
                    "data": "old_price", "sClass": "right", "bSearchable": true, "bSortable": true, "width": "100", "height": "150"
                    //"mRender": function (data, type, full, meta) {
                    //    return data.toLocaleString('vi', { style: 'currency', currency: 'VND' });
                    //}
                },
                {
                    "data": "date_created", "sClass": "left", "bSearchable": true, "bSortable": true, "width": "100", "height": "150"
                    // "mRender": function (data, type, full, meta) {
                    //    return moment(data).format('DD-MM-YYYY HH:mm:ss');
                    //}
                },
                { "data": "create_by", "sClass": "left", "bSearchable": true, "bSortable": true, "width": "100", "height": "150" },
                {
                    "data": "id",
                    "bSearchable": false,
                    "bSortable": false,
                    "sClass": "text-center",
                    "width": "100", "height": "150",
                    "mRender": function (data, type, full, meta) {
                        if (full.status)
                            return '<button id="btnEdit" title="Sửa" class="btn btn-primary btn-xs" data-toggle="modal" data-id="' + full.product_id + '"><i class="fa fa-pencil" ></i ></button>\
                                <button id="btnDelete" title="Xóa" class="btn btn-danger btn-xs" data-toggle="modal" data-id="' + full.product_id + '"><i class="fa fa-trash-o"></i></button>\
                                <button id="btnResetPass" title="Đặt lại mật khẩu" class="btn btn-warning btn-xs" data-id="' + full.product_id + '"><i class="fa fa-undo"></i></button>'
                        return '<button id="btnEdit" title="Sửa" class="btn btn-primary btn-xs" data-toggle="modal" data-id="' + full.product_id + '"><i class="fa fa-pencil" ></i ></button>\
                                <button id="btnDelete" title="Xóa" class="btn btn-danger btn-xs" data-toggle="modal" data-id="' + full.product_id + '"><i class="fa fa-trash-o"></i></button>\
                                <button id="btnResetPass" title="Đặt lại mật khẩu" class="btn btn-warning btn-xs" data-id="' + full.product_id + '"><i class="fa fa-undo"></i></button>'
                    }
                }

            ],
        buttons: []
    });

    //pTable.one('draw', function () { pTable.columns.adjust(); }).ajax.reload();
};

$(document).ready(function () {
    loadData();
});

$(document).on('click', '#btnAdd', function () {
    $('#serviceModal').modal({ backdrop: 'static', keyboard: false });
    clearData("#serviceModal");
});

$(document).on('click', '#btnEdit', function () {
    var id = $(this).attr('data-id');
    $('#productManagerModal').modal({ backdrop: 'static', keyboard: false });
    var apiFind = APP_CONFIG.FindById + id;
    $.ajax({
        url: apiFind,
        type: "get",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        headers: { "Authorization": "Bearer " + localStorage.getItem('token') },
        success: function (data) {
            var rs = data.result;
            if (data.success) {
                $('#productManagerModal input[name="product_id"]').val(rs.product_id);
                $('#productManagerModal input[name="product_name"]').val(rs.product_name);
                $('#productManagerModal input[name="price"]').val(rs.price);
                $('#productManagerModal input[name="old_price"]').val(rs.old_price);
            }
            else
                iziToast.error({ timeout: 5000, message: 'Lấy Dữ Liệu Thất Bại' });
        }
    });
});

$('#btnSaveModalAdd').click(function () {
    var apiAdd = APP_CONFIG.Add;
    var input = document.getElementById("files");
    var files = input.files;
    var formData = new FormData();

    for (var i = 0; i != files.length; i++) {
        formData.append("thumbnailImage", files[i]);
    }
    formData.append("productName", $('#serviceModal input[name="product_name"]').val());
    formData.append("price", $('#serviceModal input[name="price"]').val());
    formData.append("oldPrice", $('#serviceModal input[name="old_price"]').val());

    $.ajax({
        url: apiAdd,
        type: "post",
        processData: false,
        contentType: false,
        headers: { "Authorization": "Bearer " + localStorage.getItem('token') },
        data: formData,
        success: function (data) {
            if (data.success) {
                iziToast.success({ timeout: 5000, message: 'Thêm Sản Phẩm Thành Công' });
                pTable.ajax.reload();

            }
            else
                iziToast.error({ timeout: 5000, message: 'Thêm Sản Phẩm Thất Bại' });
        }
    });
});

$('#btnSaveModalUpdate').click(function () {
    var id = $('#productManagerModal input[name="product_id"]').val();
    var apiUpdate = APP_CONFIG.Update + id;
    $.ajax({
        url: apiUpdate,
        type: "post",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        headers: { "Authorization": "Bearer " + localStorage.getItem('token') },
        data: JSON.stringify({
            product_id: $('#productManagerModal input[name="product_id"]').val(),
            product_Name: $('#productManagerModal input[name="product_name"]').val(),
            price: $('#productManagerModal input[name="price"]').val(),
            old_Price: $('#productManagerModal input[name="old_price"]').val()
        }),
        success: function (data) {
            if (data.success) {
                iziToast.success({ timeout: 5000, message: 'Update thành công' });
                pTable.ajax.reload();
            }
            else
                iziToast.error({ timeout: 5000, message: 'Update thất bại' });
        }
    });
});

$(document).on('click', '#btnDelete', function () {
    var id = $(this).attr('data-id');
    var apiDelete = APP_CONFIG.Delete + id;
    $.ajax({
        url: apiDelete,
        type: "Post",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        headers: { "Authorization": "Bearer " + localStorage.getItem('token') },
        success: function (data) {
            var rs = data.result;
            if (data.success) {
                iziToast.success({ timeout: 5000, message: 'Xóa Sản phẩm thành công' });
                pTable.ajax.reload();
            }
            else
                iziToast.error({ timeout: 5000, message: 'Xóa Sản phẩm Thất Bại' });
        }
    });
});

function clearData(modalName) {
    $(modalName)
        .find("input,textarea,select")
        .val('')
        .end();
    //$('#serviceModal input[name="is_Active"]').prop('checked', true);
    //$('#serviceModal input[name="is_System"]').prop('checked', true);
    $("#validationForm").removeClass('was-validated');
}

function resetValidation() {
    $("#validationCode").addClass('invalid-feedback');
    $("#validationForm").removeClass('was-validated');
    $("#validationCode").removeClass("error-validation");
    $("#code").removeClass("error-field");
    $('#serviceModal').modal('hide');
    $("#validationCode").html("Vui lòng nhập mã dịch vụ");
}

function printInfo(data) {
    var mywindow = window.open('', 'PRINT');
    mywindow.document.write('<head><link href="css/print_order.css" rel="stylesheet" /></head>');

    var template = createTemplate(data);
    mywindow.document.write(template);

    setTimeout(function () {
        mywindow.print();
        mywindow.close();
    }, 250);
}
function createTemplate(data) {
    var transactionDate = new Date(data.time);
    var month = parseInt(transactionDate.getMonth()) + 1;
    var date = transactionDate.getHours() + ':' + transactionDate.getMinutes() + ', Ngày ' + transactionDate.getDate() + ' tháng ' + month + ' năm ' + transactionDate.getFullYear();

    // var employeeName = data.employee_name;
    var productName = $('#PaymentForm input[name="product_name"]').val();
    var price = $('#PaymentForm input[name="price"]').val();
    var oldPrice = $('#PaymentForm input[name="price"]').val();
    var currentDate = new Date();
    var templateData = { date, productName, price, oldPrice };
    return createRechargeTemplate(templateData);
}

function createRechargeTemplate(data) {
    return `<body>
               <div style="color:black; width:80mm; padding:0px; margin:auto">
        <div>
            <center >
            <div>
                <div class="logo"><img style="height: 50px; width: 70px;float: left;" lt=""src="../../img/hospital_k_logo.png" >
                    <div class="header" style="margin-left: 25%;">
                        <h6 style="text-align: left;"><b>VIETNAM NATIONAL CANCER HOSPITAL</b></h6>
                       <h5 style="margin-top: -15px;text-align: left;"><b>BỆNH VIỆN K</b></h5>
                    </div>
                </div>
            </div>

                <h3 class="mt-1 title" style="margin-top: 10%;"><b>BIÊN NHẬN NẠP TIỀN</b></h3>
                <p class="date"style="font-size:12px">${data.date}</p>
            </center>
        </div>
        <div style="margin-top: 30px;">
            <center style="margin-top:-25px">
                <span style="font-size: 0.5rem;">-----------------------------------------------</span>
            </center>
        </div>
        <div style="margin-top: 30px;">
            <center style="margin-top:-25px">
                <span style="font-size: 1rem;">Số phiếu : ${data.number}</span>
            </center>
        </div>
        <div style="margin-top: 30px;">
            <center style="margin-top:-25px">
                <table>
                    <tr>
                        <div id="info"></div>
                        <input id="barcodeValue" style="display: none;" value="001583687423" />
                    </tr>
                </table>
            </center>
        </div>
        <div class="row ml-1 mt-1">
            <table style="table-layout: auto !important;">
                <tr>
                    <td style="width: 45%;">
                        <span style="font-size: 1rem;">Nhân viên:</span>
                    </td>
                    <td style="width: 100%;">
                        <span style="font-size: 1rem; font-weight: bolder;">${data.productName}</span>
                    </td>
                </tr>
                <tr>
                    <td style="width: auto;">
                        <span style="font-size: 1rem;">Mã số BN:</span>
                    </td>
                    <td style="width: auto;">
                        <span style="font-size: 1rem; font-weight: bolder;">${data.price}</span>
                    </td>
                </tr>
                <tr>
                    <td style="width: auto;">
                        <span style="font-size: 1rem;">Tên BN:</span>
                    </td>
                    <td style="width: auto;">
                        <span style="font-size: 1rem; font-weight: bolder;">${data.oldPrice}</span>
                    </td>
                </tr>
                <tr>
                    <td style="width: auto;">
                        <span style="font-size: 1rem;">Giới tính:</span>
                    </td>
                    <td style="width: auto;">
                        <span style="font-size: 1rem; font-weight: bolder;">${data.gender}</span>
                        <span style="font-size: 1rem; margin-left: 10px;">Năm sinh:</span>
                        <span style="font-size: 1rem; margin-left: 10px; font-weight: bolder;">${data.yearOfBirth}</span>
                    </td>
                </tr>
            </table>
            <div>
                <p class="shift-title">Số tiền:</p>
                <center><p class="large-content" style="font-size: 35px;font-weight: 600; margin-top:-15px">${data.money}<span style="font-size: 12px;">&nbsp;&nbsp;(VNĐ)</span></p></center>
            </div>
            <p class="footer" style="font-size: 10px; margin-top:-20px">Bệnh nhân vui lòng giữ phiếu cho đến khi kết thúc khám tại bệnh viện</p>
            <center><p style="margin-top:-10px">-----------------------------------------------</p></center>
        </div>

        <script src="js/js-barcode.js"></script>
        <script src="js/print.js"></script>
    </body>`
}