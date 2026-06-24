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
        //Show error message
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
        return;
    }
    const jsonresponse = await response.json();
    taskListViewModel.userTasks([]);
    jsonresponse.forEach(value => {
        taskListViewModel.userTasks.push(new taskElementViewModel(value));
    });
    taskListViewModel.loading(false);
}
