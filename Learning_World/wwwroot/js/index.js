document.addEventListener("DOMContentLoaded", function () {
    const contentDiv1 = document.getElementById('div1');

    function loadContent(url, viewName, content, scriptPath) {
        history.pushState(null, '', url);
        fetch(viewName)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.text();
            })
            .then(html => {
                content.innerHTML = html;
                //if (scriptPath) {
                //    return loadScript(scriptPath);
                //}
            })
            //.then(() => {
            //    if (window.initializeLesson) {
            //        window.initializeLesson();
            //    }
            //})
            .catch(error => console.error('Error:', error));
    }

    //function loadScript(scriptPath) {
    //    return new Promise((resolve, reject) => {
    //        const script = document.createElement('script');
    //        script.src = scriptPath + '?v=' + new Date().getTime(); // Add timestamp to prevent caching
    //        script.onload = function () {
    //            console.log(`${scriptPath} loaded and executed.`);
    //            resolve();
    //        };
    //        script.onerror = function () {
    //            console.error(`Failed to load script: ${scriptPath}`);
    //            reject();
    //        };
    //        document.body.appendChild(script);
    //    });
    //}

    // Function to set active and load content
    function setActive(element) {
        const items = document.querySelectorAll('#moduleList .list-group-item');
        items.forEach(item => {
            item.style.backgroundColor = '';
            item.style.borderLeft = '';
            item.style.marginLeft = '';
        });
        element.style.backgroundColor = '#e9ecef';
        element.style.borderLeft = '3px solid #0d6efd';
        element.style.marginLeft = '-2px';
    }

    document.querySelectorAll('#moduleList .list-group-item').forEach(function (item) {
        item.addEventListener('click', function (e) {
            e.preventDefault();
            const moduleId = this.getAttribute('data-module-id');
            const courseId = this.getAttribute('data-course-id');
            setActive(this);
            loadContent(`/Learn/index/${courseId}/module/` + moduleId, `/Learn/PartsPartialView/${moduleId}`, contentDiv1, '/js/lessons.js');
        });
    });

    // Function to load initial content
    function loadInitialContent() {
        const currentUrl = window.location.pathname;
        const urlSegments = currentUrl.split('/');
        const courseId = urlSegments[3];
        let moduleId = urlSegments[5]; // Get the module ID from the URL

        // If no moduleId is present in the URL, default to the first module
        if (!moduleId) {
            const firstModule = document.querySelector("#moduleList .list-group-item");
            if (firstModule) {
                moduleId = firstModule.getAttribute('data-module-id');
            }
        }

        // Load the content for the selected module
        const selectedModule = document.querySelector(`#moduleList .list-group-item[data-module-id="${moduleId}"]`);
        if (selectedModule) {
            setActive(selectedModule);
            loadContent(`/Learn/index/${courseId}/module/${moduleId}`, `/Learn/PartsPartialView/${moduleId}`, contentDiv1, '/js/lessons.js');
        }
    }

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
