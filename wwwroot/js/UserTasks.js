function addNewTask() {
    taskListViewModel.userTasks.push(new taskElementViewModel({ id: 0, title: '' }));
    $("[name=task-title").last().focus();
}

function manageFocusOutTaskTitle(userTaskP) {
    const title = userTaskP.title();
    if (!title || title === '') {
        taskListViewModel.userTasks.pop();
    }
    else {
        userTaskP.id(1);
    }
}
