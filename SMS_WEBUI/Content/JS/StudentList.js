const deleteBtns = document.querySelectorAll('.delete-btn');
deleteBtns.forEach(btn => {
    btn.addEventListener('click', (event) => {
        event.preventDefault();
        let v = btn.getAttribute('id');

        console.log("delete clicked for" + v);
        const xhr = new XMLHttpRequest();
        xhr.open('GET', "Student/Delete?studentId=" + btn.getAttribute('id'), true);
        xhr.onload = () => {
            if (xhr.status === 200) {
                var responsee = JSON.parse(xhr.response);
                if (responsee.mapped) {
                    ptag.textContent = "Already mapped with an Teacher! Cannot delete";
                    toaster.style.display = 'flex';
                    toaster.style.backgroundColor = 'red';
                    setTimeout(displayToast, 3000);
                }
                else if (responsee.flag) {
                    deleteRowFromTable(btn.getAttribute('id'));
                    ptag.textContent = "Student record deleted successfully";
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