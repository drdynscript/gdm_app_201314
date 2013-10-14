(function () {
    // create a map in the "map" div, set the view to a given place and zoom
    var map = L.map('map-contact').setView([51.087243, 3.669174], 13);

    // add an OpenStreetMap tile layer
    L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(map);

    // add a marker in the given location, attach some popup content to it and open the popup
    L.marker([51.087243, 3.669174]).addTo(map)
        .bindPopup('<strong>Bachelor in de grafische en digitale media</strong><br>Industrieweg 232<br>9030 Mariakerke')
        .openPopup();
})();
function ShowContactFormError() {
    $("#status-contactform").removeClass();
    $("#status-contactform").addClass("alert alert-error");
    $("#status-contactform").html("<strong>Error!</strong> There was an error posting the contact form. Please try again later.");
}

function ShowContactFormSuccess() {
    $("#contactform").removeClass();
    $("#contactform").addClass("alert alert-success");
}

function ShowContactFormPreloader() {
}

function HideContactFormPreloader() {
}
function ShowNewsletterFormError() {
    $("#status-newsletterform").removeClass();
    $("#status-newsletterform").addClass("alert alert-error");
    $("#status-newsletterform").html("<strong>Error!</strong> There was an error posting the newsletter form. Please try again later.");
}

function ShowNewsletterFormSuccess() {
    $("#newsletterform").removeClass();
    $("#newsletterform").addClass("alert alert-success");
}

function ShowNewsletterFormPreloader() {
}

function HideNewsletterFormPreloader() {
}
