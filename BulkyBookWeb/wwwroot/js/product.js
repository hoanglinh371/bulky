var dataTable;
$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#dataTable').DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll",
        },
        columns: [
            { data: 'id' },
            { data: 'name' },
            { data: 'isbn' },
            { data: 'price' },
            { data: 'author' },
            { data: 'category.name' },
            { data: 'coverType.name' },
            {
                data: 'id',
                render: function (data) {
                    return `
                        <div class="d-flex justify-content-around align-items-center">
                            <a class="btn btn-sm btn-primary" href="/Admin/Product/Edit?Id=${data}">Edit</a>
                            <a class="btn btn-sm btn-danger" onclick=deleteProduct("/Admin/Product/Delete?Id=${data}")>Delete</a>
                        </div>
                    `
                }
            }
        ],
    })
}

function deleteProduct(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'delete',
                success: function (data) {
                    Swal.fire(
                        'Deleted!',
                        'Your file has been deleted.',
                        'success'
                    )
                }
            })
        }
    })
}