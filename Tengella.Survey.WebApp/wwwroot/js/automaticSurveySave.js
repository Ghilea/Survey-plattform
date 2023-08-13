$(document).ready(function () {
    let formChanged = false;
    const SaveDelay = 5000;

    $('form :input').change(function () {
        formChanged = true;
    });

    setInterval(function () {
        if (formChanged) {
            saveFormData();
            formChanged = false;
        }
    }, SaveDelay);

    function saveFormData() {

        const dataToSend = {};
        const formData = $('form').serializeArray();

        formData.forEach(function (entry) {
            dataToSend[entry.name] = entry.value;
        });

        const pathSegments = window.location.pathname.split('/');
        const id = pathSegments[pathSegments.length - 1];

        dataToSend.id = id;

        const urlParams = new URLSearchParams(window.location.search);
        const emailAddress = urlParams.get('emailAddress');

        dataToSend.emailAddress = emailAddress;

        $.ajax({
            contentType: 'application/json',
            type: 'POST',
            url: '/Api/ApiSurvey/SaveFormData',
            data: JSON.stringify(dataToSend)
        });
    }
});