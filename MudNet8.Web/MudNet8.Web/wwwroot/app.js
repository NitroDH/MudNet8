//Little shim to handle the mudlayout when drawer is operating
window.themeFunctions = {
    toggleNavAside: async function () {
        const layoutDrawerOpen = "mud-drawer-open-responsive-md-left",
            layoutDrawerClosed = "mud-drawer-closed-responsive-md-left";
        let layoutElement = document.querySelector(".mud-layout");

        if (layoutElement) {
            let isOpen = layoutElement.classList.contains(layoutDrawerOpen);
            if (isOpen) {
                layoutElement.classList.remove(layoutDrawerOpen);
                layoutElement.classList.add(layoutDrawerClosed);
            } else {
                layoutElement.classList.remove(layoutDrawerClosed);
                layoutElement.classList.add(layoutDrawerOpen);
            }
        }
    }
}