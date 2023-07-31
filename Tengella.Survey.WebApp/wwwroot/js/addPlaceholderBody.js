$(document).ready(function () {

    $(document).ready(function () {
        const dropdown = $('#surveyType');
        const textarea = $('#body-textarea');

        const initialTextareaValue = textarea.val();

        function updateTextarea() {
            const selectedSurveyName = dropdown.find(':selected').text();

            if (selectedSurveyName) {
                const surveyLinkPlaceholder = '<p><a href="{SurveyLink}">Länk till enkäten</a></p>';
                const unsubscribeLink = '<p><a href="{UnsubscribeLink}">Avregistrera dig från fler utskick</a></p>';

                if (!textarea.val().includes('{SurveyLink}')) {
                    const updatedTextareaValue = initialTextareaValue.trim() + '\n' + surveyLinkPlaceholder + unsubscribeLink;
                    textarea.val(updatedTextareaValue);
                }

            } else {
                textarea.val(initialTextareaValue);
            }
        }

        updateTextarea();

        dropdown.on('change', updateTextarea);
    });
});