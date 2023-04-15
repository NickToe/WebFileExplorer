var hierarchy = document.getElementById("hierarchy");

hierarchy.addEventListener("click", function (event) {
  var elem = event.target;

  if ((elem.tagName.toLowerCase() == "span" || elem.tagName.toLowerCase() == "div") && elem !== event.currentTarget) {
    if (elem.parentElement.classList.contains("folder")) elem = elem.parentElement;

    var type = elem.classList.contains("folder") ? "folder" : "file";

    if (type == "folder") {
      var isexpanded = elem.dataset.isexpanded == "true";

      if (isexpanded) {
        elem.classList.remove("fa-folder-o");
        elem.classList.add("fa-folder");
      }
      else {
        elem.classList.remove("fa-folder");
        elem.classList.add("fa-folder-o");
      }

      elem.dataset.isexpanded = !isexpanded;

      var toggleelems = [].slice.call(elem.parentElement.children);
      var classnames = "file,foldercontainer".split(",");

      toggleelems.forEach(function (element) {
        if (classnames.some(function (val) {
          return element.classList.contains(val);
        }))
          element.style.display = isexpanded ? "none" : "block";
      });
    }
  }
});