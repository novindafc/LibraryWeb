$(document).ready(function () {
    $('#BookLogDatatable').dataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/BookLog/Get_AllBookLog",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],
        "columns": [
            { "data": "Id", "name": "Id", "autoWidth": true },
            { "data": "StartTime", "name": "Start Time", "autoWidth": true },
            { "data": "EndTime", "name": "End Time", "autoWidth": true },
            { "data": "BookId", "name": "Book ID", "autoWidth": true },
            { "data": "MemberId", "name": "Member ID", "autoWidth": true },
            { "data": "Status", "name": "Status", "autoWidth": true },
            {
                "render": function (data,row) { return "<a href='#' class='btn btn-danger' onclick=DeleteCustomer('" + row.id+ "'); >Delete</a>"; "<a href='#' class='btn btn-danger' onclick=DeleteCustomer('" + row.id+ "'); >Edit</a>"; "<a href='#' class='btn btn-danger' onclick=DeleteCustomer('" + row.id+ "'); >Details</a>";  }
            },
        ]
    
    });
});