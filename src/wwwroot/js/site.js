let index = 0;

let outgoing = document.getElementById("carousel-a");
let active = document.getElementById("carousel-b");
let upcoming = document.getElementById("carousel-c");

addImageToCarousel();
setInterval(addImageToCarousel, 10000);

function addImageToCarousel() {
    console.log("index: ", index);
    const currentMetadata = metadata[index];

    const shuffler = outgoing;
    outgoing = active;
    active = upcoming;
    upcoming = shuffler;

    active.classList = "carousel active";
    upcoming.classList = "carousel upcoming";
    outgoing.classList = "carousel outgoing";

    // prepare upcoming
    const background = createBackgroundElement(currentMetadata.filename)
    const foreground = createForegroundImage(currentMetadata.filename);
    const datestamp = createDatestamp(currentMetadata.datestamp);
    upcoming.replaceChildren(background, foreground, datestamp);

    index++;
    if (index >= metadata.length) {
        console.log("resetting index");
        index = 0;
    }

    if (active.childNodes.length == 0) {
        setTimeout(addImageToCarousel, 0);
    }
}

function createBackgroundElement(path) {
    const element = document.createElement("div");
    element.style.backgroundImage = `url("${path}")`;
    element.classList.add("background");
    return element;
}

function createForegroundImage(path) {
    const image = new Image();
    image.src = path;
    image.classList.add("foreground");
    return image;
}

function createDatestamp(datestamp) {
    const element = document.createElement("div");
    element.innerText = datestamp;
    element.classList.add("datestamp");
    return element;
}