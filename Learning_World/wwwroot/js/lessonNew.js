document.addEventListener("DOMContentLoaded", function () {
    const sidebar = document.getElementById('sidebar');
    const toggleSidebarBtn = document.getElementById('toggleSidebarBtn');
    const menuText = toggleSidebarBtn.querySelector('.menu-text');
    const lessonItems = document.querySelectorAll('.lesson-item');
    const partTitles = document.querySelectorAll('.part-title');
    const contentDiv1 = document.getElementById('main');


    // just push the url in the browser and excute the viewname

    function loadContent(url, viewName, content, pushState = true) {
        if (pushState) {
            // Push state to history only when navigating, not during popstate
            history.pushState({ url, viewName }, '', url);
        }

        fetch(viewName)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.text();
            })
            .then(html => {
                content.innerHTML = html;
            })
            .catch(error => console.error('Error loading view:', error));
    }

    // Toggle sidebar collapse
    toggleSidebarBtn.addEventListener('click', function () {
        sidebar.classList.toggle('collapsed');
        menuText.textContent = sidebar.classList.contains('collapsed') ? 'Show menu' : 'Hide menu';
    });

    // Handle lesson item clicks
    lessonItems.forEach(item => {
        item.addEventListener('click', function (e) {
            e.preventDefault();
            selectLesson(this);
        });
    });

    // Handle part title clicks to expand/collapse
    partTitles.forEach(title => {
        title.addEventListener('click', function () {
            const icon = this.querySelector('i');
            icon.classList.toggle('bi-chevron-right');
            icon.classList.toggle('bi-chevron-down');
        });
    });

    // Set active lesson
    function setActive(element) {
        lessonItems.forEach(item => item.classList.remove('active-section'));
        if (element) {
            element.classList.add('active-section');
        }
    }

    // Select and load a lesson when click
    function selectLesson(element) {
        const lessonId = element.dataset.lessonId;
        const lessonType = element.dataset.lessonType;
        const moduleId = element.dataset.moduleId;

        setActive(element);
        loadContent(
            `/Learn/lesson/${moduleId}/${lessonType}/${lessonId}`,
            `/Learn/LessonDisplayPartialView/${moduleId}/${lessonType}/${lessonId}`,
            contentDiv1
        );
    }

    document.getElementById("mark-btn").onclick = function () {

        myFunction(this);
    };

    function myFunction(element) {
        const spanText = document.getElementById("completed-span")
        spanText.innerText = "Completed";
        element.innerText = "Go to next";
    }


    // Load the initial content when the page loads (not clicked on button)
    function loadInitialContent() {
        const urlSegments = window.location.pathname.split('/');
        const moduleId = urlSegments[3];
        const lessonType = urlSegments[4];
        const lessonId = urlSegments[5];

        if (moduleId && lessonType && lessonId) {
            const selectedLesson = document.querySelector(`.lesson-item[data-lesson-id="${lessonId}"][data-lesson-type="${lessonType}"]`);

            if (selectedLesson) {
                const partContainer = selectedLesson.closest('.part-container');
                const collapseElement = partContainer.querySelector('.collapse');

                const bootstrapCollapse = new bootstrap.Collapse(collapseElement, { toggle: false });
                bootstrapCollapse.show();

                setActive(selectedLesson);
                loadContent(
                    `/Learn/lesson/${moduleId}/${lessonType}/${lessonId}`,
                    `/Learn/LessonDisplayPartialView/${moduleId}/${lessonType}/${lessonId}`,
                    contentDiv1,
                    false  // Don't push state on initial load
                );
            }
        } else {
            //const firstLesson = document.querySelector('.lesson-item');
            //if (firstLesson) {
            //    selectLesson(firstLesson);
            //}
        }
    }

    // Handle browser back/forward button navigation
    window.addEventListener('popstate', function (event) {
        const state = event.state;

        if (state) {
            // Restore the state without pushing a new one
            loadContent(state.url, state.viewName, contentDiv1, state.scriptPath, false);

            const urlSegments = state.url.split('/');
            const moduleId = urlSegments[3];
            const lessonType = urlSegments[4];
            const lessonId = urlSegments[5];

            const selectedLesson = document.querySelector(`.lesson-item[data-lesson-id="${lessonId}"][data-lesson-type="${lessonType}"]`);

            if (selectedLesson) {
                const partContainer = selectedLesson.closest('.part-container');
                const collapseElement = partContainer.querySelector('.collapse');

                const bootstrapCollapse = new bootstrap.Collapse(collapseElement, { toggle: false });
                bootstrapCollapse.show();

                setActive(selectedLesson);
            }
        } else {
            loadInitialContent();
        }
    });

    // Load the appropriate content on initial load
    loadInitialContent();
});

