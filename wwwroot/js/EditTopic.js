// variables for selected image and topicID at top of scope
let selectedImage;
let topicID;

const closeBtn = document.getElementById('close-btn');
closeBtn.addEventListener('click', () => {
  dialog.close();
});

const dialog = document.querySelector('dialog');

// Event Listener for the update image button
document.addEventListener('click', (e) => {
  if (e.target.classList.contains('updateImageButton')) {
    // Capture the relevant topic id via a dataset attribute on the button
    topicID = e.target.dataset.topicId;
    // The controller method for retrieving the contents of the wwwroot/uploads afolder via an array of strings
    const ExistingImageEndpoint = '/topic/GetImages';
    // The modal dialog where the images will render
    const imageDiv = document.getElementById('imageGallery');
    // Clear previous images to avoid duplicates
    imageDiv.innerHTML = '';

    // Open the dialog
    dialog.showModal();
    dialog.scrollTop = 0;

    // Initiate the fetch call to the controller endpoint
    fetch(ExistingImageEndpoint, {
      method: 'GET',
    })
      .then((response) => response.json())
      .then((data) => {
        // Loop through the array of strings returned
        // Create an img for each string and append it to the div.
        data.forEach((imageSrc) => {
          const div = document.createElement('div');
          div.classList.add('image');

          const img = document.createElement('img');
          img.src = imageSrc;
          img.classList.add('galleryImage');

          div.appendChild(img);
          imageDiv.appendChild(div);
        });
      })
      .catch((error) => {
        console.error('error fetching images:', error);
      });
  }
});

// Event listener for when a user selects an existing image within the modal dialog
dialog.addEventListener('click', (e) => {
  const target = e.target;

  if (target.classList.contains('galleryImage')) {
    let imageSource = target.src;
    let imageUrl = new URL(imageSource);
    selectedImage = imageUrl.pathname;
    updateSelectedImageDisplay(imageSource);
    // Remove any files from the input field if an existing image from the gallery is selected
    imageUpload.value = '';
  } else if (target.classList.contains('image')) {
    let imageSource = target.children[0].src;
    updateSelectedImageDisplay(imageSource);
  }
});

// Function to update the image display on the page upon click
function updateSelectedImageDisplay(imageSrc) {
  const myImageList = document.querySelectorAll('.image');
  for (let i = 0; i < myImageList.length; i++) {
    let currentImage = myImageList[i].children[0];
    if (currentImage.src == imageSrc) {
      currentImage.parentNode.classList.contains('imageSelected')
        ? currentImage.parentNode.classList.remove('imageSelected')
        : currentImage.parentNode.classList.add('imageSelected');
      submitButtonStatus(currentImage.parentNode);
    } else {
      currentImage.parentNode.classList.remove('imageSelected');
    }
  }
}

// Function to update the submit button display
function submitButtonStatus(image) {
  const submitButton = document.getElementById('submitButton');
  if (image.classList.contains('imageSelected')) {
    submitButton.classList.remove('hidden');
  } else {
    submitButton.classList.add('hidden');
  }
}

// Event listener and function for the image upload input
const imageUpload = document.getElementById('imageUpload');
imageUpload.onchange = () => {
  let input = imageUpload.files[0];
  if (input) {
    submitButton.classList.remove('hidden');
    const myImageList = document.querySelectorAll('.image');
    for (let i = 0; i < myImageList.length; i++) {
      let currentImage = myImageList[i].children[0];
      if (currentImage.parentNode.classList.contains('imageSelected')) {
        currentImage.parentNode.classList.remove('imageSelected');
      }
    }
  }
};

const ImageUploadEndpoint = '/topic';
//  Event listener for the submit button
const submitButton = document.getElementById('submitButton');
submitButton.addEventListener('click', () => {
  let input = imageUpload.files[0];
  if (input) {
    //const TopicIDField = document.getElementById('TopicIDField').value;
    const formData = new FormData();
    formData.append('file', input);

    fetch(ImageUploadEndpoint, {
      method: 'POST',
      headers: {},
      body: formData,
    })
      .then((response) => response.json())
      .then((data) => {
        updateTopicImageString(topicID, data.filepath);
        dialog.close();
        //alert('File uploaded successfully!');
      })
      .catch((error) => {
        console.error(error);
        alert('Error uploading file');
      });
  } else {
    console.log('you have selected an existing image');
    selectedImageTrimmed = selectedImage.split('/uploads/');
    updateTopicImageString(topicID, selectedImageTrimmed[1]);
    dialog.close();
  }
});

async function updateTopicImageString(topicid, imagefilepath) {
  const imagePathUpdateEndpoint = `/topic/${topicid}/${imagefilepath}`;
  try {
    const response = await fetch(imagePathUpdateEndpoint, { method: 'POST' });
    if (!response.ok) {
      throw new Error(`Response status: ${response.status}`);
    }

    updateTopicImageStringDisplayOnWebpage(topicid);
  } catch (error) {
    console.error(error.message);
  }
}

async function updateTopicImageStringDisplayOnWebpage(topicid) {
  console.log(`The topicID is ${topicid}`);
  const topicEndpoint = `/topic/GetTopic/${topicid}`;
  fetch(topicEndpoint, { method: 'GET' })
    .then((response) => response.json())
    .then((data) => {
      const newImage = data.topicImageFilePath;
      console.log(newImage);
      const topicImageFields =
        document.getElementsByClassName('topicImageField');
      console.log(topicImageFields);
      // Loop through the Topic Image Filepath table cells (nodelist)
      // If the data attribute matches the Topic ID, update the inner content with the new filepath without page refresh needed
      for (let x of topicImageFields) {
        if (x.dataset.imageid == topicid) {
          x.innerHTML = newImage;
        }
      }
    })
    .catch((error) => console.error('Error:', error));
}
