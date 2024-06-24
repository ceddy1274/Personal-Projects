
// Define the maximum number of tries
const TRIES = 6;

// Fetch the JSON file
fetch('digital_design.json')
.then(response => response.json())
.then(data => {
    // Pick a random item
    const item = data[Math.floor(Math.random() * data.length)];
    const word = item.name.toUpperCase();
    const picture = item.image;

    document.getElementById('image').innerHTML = `<img src="${picture}" alt="Hangman Image" style="width: 100%;">`;


    main(word, item.description);

})
.catch(error => console.error('Error:', error));


// Main function
function main(word, description) {
    // Get references to the HTML elements
    const bodyParts = {
        head: document.getElementById('head'),
        body: document.getElementById('body'),
        leftArm: document.getElementById('left-arm'),
        rightArm: document.getElementById('right-arm'),
        leftLeg: document.getElementById('left-leg'),
        rightLeg: document.getElementById('right-leg'),
        playagain: document.getElementById('play-again')
    };

    // Hide the body parts and play again button initially
    Object.values(bodyParts).forEach(part => part.style.display = 'none');

    // Initialize the blanks string and guesses
    let blanks = Array.from(word, c => (c === ' ' ? ' ' : '_'));
    let guesses = 0;

    // Set the description, blanks, and guesses in the HTML
    document.getElementById("description").innerHTML = description;
    document.getElementById("blanks").innerHTML = blanks.join(" ");
    document.getElementById("guesses").innerHTML = `${guesses}/${TRIES}`;

    // Create buttons for each letter of the alphabet
    createAlphabetButtons();

    // Add event listener to the form
    document.getElementById('textbox').addEventListener('click', function(event) {
        event.preventDefault();
        if (event.target.type === 'submit') {
            guesses = handleGuess(event, word, blanks, guesses, bodyParts);
        }
    });
}

function createAlphabetButtons() {
    const alphabet = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
    const form = document.getElementById('textbox');
    alphabet.split('').forEach(letter => {
        const button = document.createElement("input");
        button.type = "submit";
        button.value = letter;
        form.appendChild(button);
    });
}

function handleGuess(event, word, blanks, guesses, bodyParts) {
    const letter = event.target.value;
    if (guesses < TRIES && blanks.includes("_")) {
        if (word.includes(letter)) {
            // Update the blanks with the correct letter
            for (let i = 0; i < word.length; i++) {
                if (word[i] === letter) {
                    blanks[i] = letter;
                }
            }
            document.getElementById("blanks").innerHTML = blanks.join(" ");
        } else {
            // Increment the number of guesses and show the corresponding body part
            guesses++;
            console.log("Guesses: " + guesses);
            document.getElementById("guesses").innerHTML = `${guesses}/${TRIES}`;
            showBodyPart(guesses, bodyParts);
        }
        // If all the blanks are filled, show a message and the play again button
        if (!blanks.includes("_")) {
            setTimeout(function() {
                alert("You win!");
                bodyParts.playagain.style.display = 'block';
            }, 100);
        }
        // If the play again button is clicked, reload the page
        if (event.target.id === 'play-again') {
            location.reload();
        }
    } 
    return guesses; //FOR TESTING
}

function showBodyPart(guesses, bodyParts) {
    const parts = ['head', 'body', 'leftLeg', 'rightLeg', 'leftArm', 'rightArm'];
    if (guesses <= parts.length) {
        bodyParts[parts[guesses - 1]].style.display = 'block';
    }
    if (guesses === TRIES) {
        setTimeout(function() {
            alert("You lose!");
            bodyParts.playagain.style.display = 'block';
        }, 10);
    }
}
