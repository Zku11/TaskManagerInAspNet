function addNewTask() {
    taskListViewModel.userTasks.push(new taskElementViewModel({ id: 0, title: '' }));
    $("[name=task-title").last().focus();
}

async function manageFocusOutTaskTitle(userTaskP) {
    const title = userTaskP.title();
    if (!title || title === '') {
        taskListViewModel.userTasks.pop();
    }
    else {
        userTaskP.id(1);
    }
    const data = JSON.stringify(title);
    const response = await fetch(
        userTasksUrl,
        {
            method: 'POST',
            body: data,
            headers: {
                'Content-Type': 'application/json'
            }
        }
    );
    if (response.ok) {
        const jsonresponse = await response.json();
        userTaskP.id(jsonresponse.id);
    }
    else {
        handleErrorApi(response);
    }
}

async function getUserTasks() {
    taskListViewModel.loading(true);
    const response = await fetch(
        userTasksUrl,
        {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        }
    );
    if (!response.ok) {
        handleErrorApi(response);
        return;
    }
    const jsonresponse = await response.json();
    taskListViewModel.userTasks([]);
    jsonresponse.forEach(value => {
        taskListViewModel.userTasks.push(new taskElementViewModel(value));
    });
    taskListViewModel.loading(false);
}

async function updateUserTaskOrder() {
    const ids = getUserTasksId();
    await sendUserTasksIdsToBackend(ids);
    const sortedArray = taskListViewModel.userTasks.sorted(function (a, b) {
        return ids.indexOf(a.id().toString()) - ids.indexOf(b.id().toString());
    });
    taskListViewModel.userTasks([]);
    taskListViewModel.userTasks(sortedArray);
}

function getUserTasksId() {
    const ids = $("[name=task-title]").map(function () {
        return $(this).attr("data-id");
    }).get();
    return ids;
}

async function sendUserTasksIdsToBackend(ids) {
    var data = JSON.stringify(ids);
    await fetch(`${userTasksUrl}/sort`, {
        method: 'POST',
        body: data,
        headers: {
            'Content-Type': 'application/json'
        }
    });
}

$(function () {
    $("#reorderable").sortable({
        axis: 'y',
        stop: async function () {
            await updateUserTaskOrder();
        }
    })
});
