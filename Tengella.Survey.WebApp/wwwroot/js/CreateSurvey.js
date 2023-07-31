$(document).ready(function () {
    let questionIndex = 0;

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

        const optionInput = $(`<input type="text" name="${inputName}" value="Options ${optionNumber}">`);
        if (name !== "") { optionInput.attr("value", name) }

        optionInput.on('input', function () {
            const newValue = optionInput.val();
            optionInput.attr('value', newValue);
        });

        itemContainer.append(optionInput);
    }

    function addNewAdditionalInfo(event, name = "") {
        console.log('add', event)
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

        questionIndex++;
    }

    $(document).on('click', '.add-question', function () { addNewQuestion() });
    $(document).on('click', '.add-additional-info', function (event) { addNewAdditionalInfo(event) });
    $(document).on('click', '.add-option', function (event) { addNewOption(event) });
});
