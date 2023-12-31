$(document).ready(function () {
    let questionIndex = 0;
    const sliderCounts = {};
    function addRemoveButton(event, optionsContainer = null, path = null) {
        const removeButton = $("<button></button>")
            .attr({
                class: "btn delete-image-button",
                title: "Ta bort",
                type: "button"
            });

        removeButton.on('click', function () {
            event.remove();
            if (optionsContainer != null) {

                const itemContainers = optionsContainer
                    .find('.itemContainer');

                const questionCount = $(this).parent().find('input[type="hidden"]').attr('question-index');

                sliderCounts[questionCount] = itemContainers.length;

                if (itemContainers.length <= 2) {
                    itemContainers
                        .find('.remove-button')
                        .prop('disabled', true);
                }
            }
           
        });

        if (path) {
            path.append(removeButton)
        } else {    
            event.append(removeButton);
        }
        
        removeButton.addClass('remove-button');
    }

    $.each(surveyQuestions, function (index, question) {
        addNewQuestion(question.id, question.name);

        if (question.options.length > 0) {
            $.each(question.options, function (indexOption, option) {
                const getAddOptionButton = $(`#question-${index}-addOptions`);
                addNewOption(getAddOptionButton, option.id, option.name)
            });
        }

        if (question.additionalInfo) {
            const getAddAdditionButton = $(`#additionalInfo-question-${index}`);
            addNewAdditionalInfo(getAddAdditionButton, question.additionalInfo)
        }
        
    });

    function addNewOption(event, optionId, name = "") {
        const questionContainer = $(event.currentTarget === undefined ? event : event.currentTarget)
            .closest('.question-container');

        let optionsContainer = questionContainer
            .find('.options');

        const sliderContainer = $("<div></div>")
            .attr({
                class: "sliderContainer"
            });

        const sliderCount = sliderCounts[questionIndex];

        const sliderLabel = $("<label>Gr&auml;nsv&auml;rde</label>")
            .attr({
                class: "text-white",
                for: `question[${ questionIndex }]-limitSlider`
            });

        const sliderInput = $("<input>")
            .attr({
                type: "range",
                min: 0,
                max: sliderCount,
                value: Math.floor(sliderCount / sliderCount),
                class: "slider w-100",
                id: `question[${questionIndex}]-limitSlider`,
            })

        const sliderOutput = $("<output></output>");

        sliderContainer.append(sliderLabel, sliderInput, sliderOutput)

        if (optionsContainer.length === 0) {
            optionsContainer = $('<div></div>')
                .attr({
                    class: 'd-flex flex-column gap-3 options'
                });

            questionContainer.append(sliderContainer, optionsContainer);

            addOptionToContainer(optionsContainer, questionContainer, optionId, name);
            if (event.currentTarget !== undefined) {
                addOptionToContainer(optionsContainer, questionContainer, optionId, name);
            }
        } else {
            addOptionToContainer(optionsContainer, questionContainer, optionId, name);
        }

        optionsContainer
            .find('.remove-button')
            .prop('disabled', false);
    }

    function addOptionToContainer(optionsContainer, questionContainer, optionId, name) {
        const optionCount = optionsContainer
            .find('input[type="text"]').length;

        if (optionCount >= 10) {
            alert('Kan enbart ha svarsalternativ mellan 2 - 10 stycken');
            return;
        }

        const itemContainer = $("<div></div>")
            .attr({
                class: "d-flex gap-3 itemContainer"
            })
            .appendTo(optionsContainer)

        const optionNumber = optionCount + 1;
        const inputName = `Questions[${questionContainer.attr('question-index')}].Options[${optionNumber}]`;

        $("<input />")
            .attr({
                type: 'hidden',
                name: `Id.Question[${questionContainer.attr('question-index')}].Option[${optionNumber}]`,
                "question-index": questionContainer.attr('question-index'),
                value: optionId
            })
            .appendTo(itemContainer);

        const optionInput = $(`<input>`)
            .attr({
                type: "text",
                name: inputName,
                value: `Alternativ ${optionNumber}`
            });

        if (name !== "") {
            optionInput
                .attr({
                    value: name
                })
        }

        optionInput.on('input', function () {
            optionInput
                .attr({
                    value: optionInput.val()
                });
        });

        itemContainer.append(optionInput);

        const questionCount = questionContainer
            .attr('question-index');

        const itemCount = $(optionsContainer)
            .find('.itemContainer');

        sliderCounts[questionCount] = itemCount.length;

        addRemoveButton(itemContainer, optionsContainer);
    }

    function addNewAdditionalInfo(event, name = "") {
        const questionContainer = $(event.currentTarget === undefined ? event : event.currentTarget).closest('.question-container');

        if (questionContainer.find('.additional-info').length === 0) {

            const addAdditionInfo = $("<div></div>")
                .attr({
                    class: "d-flex flex-column gap-3 form-group additional-info text-light"
                });

            $("<label></label>")
                .append('Information')
                .appendTo(addAdditionInfo);

            const containerField = $("<div class='d-flex text-2xl flex flex-row gap-3'></div>")
                .appendTo(addAdditionInfo);

            const textAreaInput = $("<textarea></textarea>")
                .attr({
                    class: "form-control",
                    name: `additionalInfo-question-${questionContainer.attr('question-index')}`,
                    placeholder: "Skriv din information..."
                })

            if (name !== "") { textAreaInput.val(name) }

            textAreaInput
                .appendTo(containerField);

            questionContainer
                .append(addAdditionInfo);

            addRemoveButton(addAdditionInfo, null, containerField);
        }
    }

    function addNewQuestion(questionId, name = "") {
        const questionContainer = $("<div></div>")
            .attr({
                class: 'd-flex flex-column gap-3 question-container bg-gradient rounded px-3 my-3',
                "question-index": questionIndex
            })
            .appendTo('#questionsContainer');

        $("<input />")
            .attr({
                type: 'hidden',
                name: `Id.Question[${questionIndex}]`,
                value: questionId,
            })
            .appendTo(questionContainer);

        const container = $("<div class='text-2xl flex flex-row gap-2'></div>");

        $(`<label for=question-${questionIndex} class="text-light mt-3 mb-2"></label>`)
            .append(`fr\u00E5ga ${1 + questionIndex}`)
            .appendTo(container);

        const containerField = $("<div class='d-flex text-2xl flex flex-row gap-3'></div>")
            .appendTo(container);

        $("<input />")
            .attr({
                id: `question-${questionIndex}`,
                class: 'form-control question',
                name: `question-${questionIndex}`,
                placeholder: 'Skriv...',
                type: 'text', value: name
            })
            .appendTo(containerField);

        const questionButtons = $("<div></div>")
            .attr({
                class: "d-flex gap-3 form-group question-buttons bg-gradient p-2 rounded"
            })

        $("<button></button>")
            .attr({
                id: `question-${questionIndex}-addOptions`,
                class: "btn shape-image-button add-option",
                type: "button",
                title: "L\u00E4gg till flervalsfr\u00E5ga"
            })
            .appendTo(questionButtons);

        $("<button></button>")
            .attr({
                id: `additionalInfo-question-${questionIndex}`,
                class: "btn information-image-button add-additional-info",
                type: "button",
                title: "L\u00E4gg till informationsruta"
            })
            .appendTo(questionButtons);

        questionContainer
            .append(container, questionButtons);

        addRemoveButton(questionContainer, null, containerField)

        questionIndex++;
    }

    const startDateInput = $('#startDateInput');
    const endDateInput = $('#endDateInput');

    startDateInput.on('change', function () {
        const selectedStartDate = new Date($(this).val());

        const minEndDate = new Date(
            selectedStartDate.getFullYear(),
            selectedStartDate.getMonth() + 1, 1);

        const formattedMinEndDate = minEndDate.toISOString().slice(0, 7);

        endDateInput
            .attr({min: formattedMinEndDate });

        if (new Date(endDateInput.val()) < minEndDate) {
            endDateInput
                .val(formattedMinEndDate);
        }
    });

    $(document).on('click', '.add-question', function () { addNewQuestion() });
    $(document).on('click', '.add-additional-info', function (event) { addNewAdditionalInfo(event) });
    $(document).on('click', '.add-option', function (event) { addNewOption(event) });
});
