(function() {
    const toggleBtn = document.getElementById('sidebarToggle');
    const sidebar = document.getElementById('arenaSidebar');

    if (!toggleBtn || !sidebar) {
        console.warn('Sidebar toggle or sidebar element not found.');
        return;
    }

    toggleBtn.addEventListener('click', function () {
        sidebar.classList.toggle('show');
    });

    // Close sidebar when clicking outside on small screens
    document.addEventListener('click', function (e) {
        if (window.innerWidth >= 992) return;
        if (!sidebar.classList.contains('show')) return;

        const clickInsideSidebar = sidebar.contains(e.target);
        const clickOnToggle = toggleBtn.contains(e.target);

        if (!clickInsideSidebar && !clickOnToggle) {
            sidebar.classList.remove('show');
        }
    });
})();