function editPcDetails(url) {
    $.ajax({
        url: url,
        type: 'GET',
        cache: false,
        success: function (result) {
            $('#modalPcDetailsWrapper').html(result);
            $('#editPcDetailsModal').modal('show');
        }
    });
}

function editInfoDetails(url) {
    $.ajax({
        url: url,
        type: 'GET',
        cache: false,
        success: function (result) {
            $('#modalInfoDetailsWrapper').html(result);
            $('#editInfoDetailsModal').modal('show');
        }
    });
}

function editSkillsAndAbilitiesDetails(url) {
    $.ajax({
        url: url,
        type: 'GET',
        cache: false,
        success: function (result) {
            $('#modalSkillsAndAbilitiesWrapper').html(result);
            $('#editSkillsAndAbilitiesModal').modal('show');
        }
    });
}

