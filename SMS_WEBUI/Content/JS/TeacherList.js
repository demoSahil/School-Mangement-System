const deleteBtns = document.querySelectorAll('.delete-btn');
deleteBtns.forEach(btn => {
    btn.addEventListener('click', (event) => {
        event.preventDefault();
        let v = btn.getAttribute('id');

        console.log("delete clicked for" + v);
        const xhr = new XMLHttpRequest();
        xhr.open('GET', "/A_Teachers/Teacher/Delete?teacherId=" + btn.getAttribute('id'), true);
        xhr.onload = () => {
            if (xhr.status === 200) {
                var responsee = JSON.parse(xhr.response);
                if (responsee.mapped) {
                    ptag.textContent = "Already mapped with an Student! Cannot delete";
                    toaster.style.display = 'flex';
                    toaster.style.backgroundColor = 'red';
                    setTimeout(displayToast, 3000);
                }
                else if (responsee.flag) {
                    deleteRowFromTable(btn.getAttribute('id'));
                    ptag.textContent = "Teacher record deleted successfully";
                    toaster.style.display = 'flex';
                    setTimeout(displayToast, 3000);
                }
            }
            else {
                console.log("Not ok response");
            }
        }

        xhr.send();
    });

});

function deleteRowFromTable(idToBeDeleted) {

    const identities = document.querySelectorAll('.student-id');

    identities.forEach(id => {
        if (id.getAttribute('id') == idToBeDeleted) {
            id.parentElement.remove();
            console.log("debugger" + "removed success-- " + id)
        }
    })
}


// Searching


const searchBar = document.getElementById('search');
const tableBody = document.getElementById('table-body');

searchBar.addEventListener('input', () => Searching());

function Searching() {
    const xhr = new XMLHttpRequest();

    xhr.open('GET', '/A_Teachers/Teacher/Search?value=' + searchBar.value, true);

    xhr.onload = () => {
        if (xhr.status === 200) {
            RenderDataIntoTable(JSON.parse(xhr.response));

        }
        else {
            alert("failurw");
        }
    }

    xhr.send();
}

// Rendering Data into Table
function RenderDataIntoTable(data) {
    tableBody.innerHTML = "";
    let str = "";
    data.forEach(d => {
        str += '<tr> <td class="student-id" id=' + d.TeacherId +
            '>' + d.TeacherId + '</td>' +
            '<td>' + d.TeacherName + '</td>' +
            '<td>' + d.Subject + '</td>' +
            '<td>' + d.ContactNumber + '</td>' +
            '<td><a href="/A_Teachers/Teacher/StudentsLinkedWithTeacher?teacherId=' +
            d.TeacherId +
            '&teacherName=' + d.TeacherName +
            '">' + d.studentsIDUnderTeacher.length +
            '</a></td>' +
            '<td> <a href="/A_Teachers/Teacher/Edit/' + d.TeacherId + '">Edit</a>' +
            ' | ' +
            '<a href="/A_Teachers/Teacher/Details/' + d.TeacherId + '">Details</a>' +
            ' | ' +
            '<a href="/A_Teachers/Teacher/Delete/' + d.TeacherId + '">Delete</a>' +
            '</td>' +
            '</tr>';
    });
    tableBody.innerHTML = str; 

}


