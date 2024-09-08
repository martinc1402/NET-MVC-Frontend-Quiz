// // Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// // for details on configuring this project to bundle and minify static web assets.

// // Write your JavaScript code.

// // 1. Initialise an array for answers
// // var answerArray = [];

// // 2. populate the array with the items in the model
// // @foreach (var q in Model)
// // {
// //   @foreach (var a in q.Answers)
// //   {
// //       @:answerArray.push("@a.AnswerText");
// //   }
// // }

// // 1. Initialise a JavaScript array for questions
// var questionArray = new Array();

// // 2. Populate the array with the items in the model
// foreach(question in Model);
// {
//   questionArray.push("@question.Title");
// }

// // 3. Initialise a counter variable at index 0
// let counter = 0;

// // 4. Populate the html content of a div with the question at the counter position of the array (initially position 0) and the answers associated with the question. Increment the counter variable by 1.
// window.addEventListener("Load", LoadQuestion());

// function LoadQuestion() {
//   console.log("Load Question");
//   const para = document.createElement("p");
//   const node = document.createTextNode(
//     questionArray[counter] + answerArray[counter]
//   );
//   para.appendChild(node);

//   const element = document.getElementById("questionDisplay");
//   element.appendChild(para);
//   counter++;
// }
// // 5. When a user answers the question (initially just clicks the 'next' button), check that the counter variable is less than the length of the array

// // 6. If the counter variable is less than the length of the array, populate the html content with the next question (i.e. the current counter position). If the counter variable isn't less than the length of the array end the quiz.
// const button = document.getElementById("nextButton");
// button.addEventListener("click", nextQuestion);

// function nextQuestion() {
//   if (counter < questionArray.length) {
//     const para = document.createElement("p");
//     const node = document.createTextNode(questionArray[counter]);
//     para.appendChild(node);

//     const element = document.getElementById("questionDisplay");
//     element.replaceChildren();
//     element.appendChild(para);
//     counter++;
//   }
// }
