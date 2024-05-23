var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/todotask/getall' },
        "columns": [
            { data: 'title', "width": "10%" },
            { data: 'description', "width": "25%" },
            { data: 'dueDate', "width": "10%" },
            { data: 'label.name', "width": "5%" },
            { data: 'priority', "width": "5%" },
            { data: 'status', "width": "5%" },
            { data: 'createdAt', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/todotask/edit?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>
                     <a href="/todotask/delete?id=${data}" class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
                },
                "width": "10%"
            },
        ]
    });
}

