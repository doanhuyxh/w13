function toastr({ type, message, timer = 5000 }) {
  return new Promise((resolve) => {
    if (document.querySelector(".toast-container")) {
      document.querySelector(".toast-container").remove();
    }
    const body = document.querySelector("body");

    const template = `
    <div class="toast-container ${type}-bg">
      <div>
        <div class="toast-frame">
          <span class="toast-message">${message}</span>
          <div class="toast-close"><i class="bi bi-x"></i></div>
        </div>
        <div class="toast-timer ${type}-timer" style="animation: timer ${timer}ms linear;"/>
      </div>
    </div>
    `;

    body.insertAdjacentHTML("afterend", template);

    const toastContainer = document.querySelector(".toast-container");

    setTimeout(() => {
      toastContainer.remove();
      resolve();
    }, timer);

    const toastClose = document.querySelector(".toast-close");

    toastClose.addEventListener("click", () => {
      toastContainer.remove();
      resolve();
    });
  });
}
