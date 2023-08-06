$(document).ready(function () {
    let questionIndex = 0;

    function addRemoveButton(event, optionsContainer = null) {
        const removeButton = $("<button>Ta bort</button>").attr({
            class: "btn btn-danger remove-button",
            type: "button"
        });

        removeButton.on('click', function () {
            event.remove();
            if (optionsContainer != null) {
                const itemContainers = optionsContainer.find('.itemContainer');
                if (itemContainers.length <= 2) {
                    itemContainers.find('.remove-button').prop('disabled', true);
                }
            }
           
        });

        event.append(removeButton);

        // Add a class to the remove button for easy selection later
        removeButton.addClass('remove-button');
    }

    $.each(surveyQuestions, function (index, question) {
        addNewQuestion(question.id, question.name);

        if (question.options.length > 0) {
            $.each(question.options, function (indexOption, option) {
                console.log(option)
                const getAddOptionButton = $(`#question-${index}-addOptions`);
                addNewOption(getAddOptionButton, option.id, option.name)
            });
        }

        if (question.additionalInfo) {
            const getAddAdditionButton = $(`#additionalInfo-question-${index}`);
            console.log(getAddAdditionButton)
            addNewAdditionalInfo(getAddAdditionButton, question.additionalInfo)
        }
        
    });

    function addNewOption(event, optionId, name = "") {
        const questionContainer = $(event.currentTarget === undefined ? event : event.currentTarget).closest('.question-container');

        let optionsContainer = questionContainer.find('.options');

        if (optionsContainer.length === 0) {
            optionsContainer = $('<div></div>').attr({ class: 'd-flex flex-column gap-3 options' });
            questionContainer.append(optionsContainer);

            addOptionToContainer(optionsContainer, questionContainer, optionId, name);
            if (event.currentTarget !== undefined) {
                addOptionToContainer(optionsContainer, questionContainer, optionId, name);
            }
        } else {
            addOptionToContainer(optionsContainer, questionContainer, optionId, name);
        }

        // Enable the "Ta bort" buttons for all options
        optionsContainer.find('.remove-button').prop('disabled', false);
    }

    function addOptionToContainer(optionsContainer, questionContainer, optionId, name) {
        const optionCount = optionsContainer.find('input[type="text"]').length;

        if (optionCount >= 10) {
            alert('Kan enbart ha svarsalternativ mellan 2 - 10 stycken');
            return;
        }

        const itemContainer = $("<div></div>").attr({ class: "d-flex gap-3 itemContainer" }).appendTo(optionsContainer)

        const optionNumber = optionCount + 1;
        const inputName = `Questions[${questionContainer.attr('question-index')}].Options[${optionNumber}]`;

        // Hidden field for option Id
        $("<input />").attr({ type: 'hidden', name: `Id.Question[${questionContainer.attr('question-index')}].Option[${optionNumber}]`, value: optionId }).appendTo(itemContainer);

        const optionInput = $(`<input type="text" name="${inputName}" value="Alternativ ${optionNumber}">`);
        if (name !== "") { optionInput.attr("value", name) }

        optionInput.on('input', function () {
            const newValue = optionInput.val();
            optionInput.attr('value', newValue);
        });

        itemContainer.append(optionInput);

        addRemoveButton(itemContainer, optionsContainer);
    }

    function addNewAdditionalInfo(event, name = "") {
        const questionContainer = $(event.currentTarget === undefined ? event : event.currentTarget).closest('.question-container');

        if (questionContainer.find('.additional-info').length === 0) {
            const addAdditionInfo = $("<div></div>").attr({ class: "form-group additional-info" });

            $("<label></label>").append('Information').appendTo(addAdditionInfo);

            const textAreaInput = $("<textarea></textarea>").attr({
                class: "form-control",
                name: `additionalInfo-question-${questionContainer.attr('question-index')}`,
                placeholder: "Skriv din information..."
            })

            if (name !== "") { textAreaInput.val(name) }

            textAreaInput.appendTo(addAdditionInfo);

            questionContainer.append(addAdditionInfo);

            addRemoveButton(addAdditionInfo);
        }
    }

    function addNewQuestion(questionId, name = "") {
        const questionContainer = $("<div></div>").attr("class", 'd-flex flex-column gap-3 question-container').attr('question-index', questionIndex).appendTo('#questionsContainer');
        $("<input />").attr({ type: 'hidden', name: `Id.Question[${questionIndex}]`, value: questionId }).appendTo(questionContainer);

        const container = $("<div class='text-2xl flex flex-row gap-x-5'></div>");
        $(`<label for=question-${questionIndex} class="text-light mt-3"></label>`).append(`#${questionIndex}`).appendTo(container);
        $("<input />").attr({ id: `question-${questionIndex}`, class: 'form-control question', name: `question-${questionIndex}`, placeholder: 'Skriv...', type: 'text', value: name }).appendTo(container);

        const questionButtons = $("<div></div>").attr("class", "d-flex gap-1 form-group question-buttons")
        $("<button>Alternativ</button>").attr({ id: `question-${questionIndex}-addOptions`, class: "btn btn-primary add-option", type: "button" }).appendTo(questionButtons);
        $("<button>Information</button>").attr({ id: `additionalInfo-question-${questionIndex}`, class: "btn btn-primary add-additional-info", type: "button" }).appendTo(questionButtons);

        questionContainer.append(container, questionButtons);

        addRemoveButton(questionContainer);

        questionIndex++;
    }

    // Get references to the StartDate and EndDate input elements
    const startDateInput = $('#startDateInput');
    const endDateInput = $('#endDateInput');

    // Update the min attribute of the EndDate input when StartDate changes
    startDateInput.on('change', function () {
        // Parse the selected start date
        const selectedStartDate = new Date($(this).val());

        // Calculate the minimum end date as the start date + 1 month
        const minEndDate = new Date(selectedStartDate.getFullYear(), selectedStartDate.getMonth() + 1, 1);

        // Format the minimum end date as "yyyy-MM"
        const formattedMinEndDate = minEndDate.toISOString().slice(0, 7);

        // Set the minimum attribute for the EndDate input
        endDateInput.attr('min', formattedMinEndDate);

        // If the selected end date is less than the minimum end date, reset the EndDate value
        if (new Date(endDateInput.val()) < minEndDate) {
            endDateInput.val(formattedMinEndDate);
        }
    });

    $(document).on('click', '.remove-button', function () {
        const optionsContainer = $(this).closest('.options');
        const itemContainers = optionsContainer.find('.itemContainer');

        // Check if there are more than 2 options left
        if (itemContainers.length > 2) {
            $(this).closest('.itemContainer').remove(); // Remove the option
        } else {
            // Disable the remove buttons for all options if there are 2 or fewer options
            itemContainers.find('.remove-button').prop('disabled', true);
        }
    });

    $('.options').each(function () {
        const itemContainers = $(this).find('.itemContainer');
        if (itemContainers.length <= 2) {
            itemContainers.find('.remove-button').prop('disabled', true);
        }
    });

    $(document).on('click', '.add-question', function () { addNewQuestion() });
    $(document).on('click', '.add-additional-info', function (event) { addNewAdditionalInfo(event) });
    $(document).on('click', '.add-option', function (event) { addNewOption(event) });
});
