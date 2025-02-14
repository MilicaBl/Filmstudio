let content = document.getElementById("content");
let movielist = document.createElement("ul");
let userForm = document.createElement("div");
let registerForm = document.createElement("div");
let registerFormText = document.createElement("p");
let loginFormText = document.createElement("p");
let city = document.createElement("input");
let loginName = document.createElement("input");
let userName = document.createElement("input");
let filmStudioName = document.createElement("input");
let registerBtn = document.createElement("button");
let loginBtn = document.createElement("button");
let registerButton = document.createElement("button");
let loginButton = document.createElement("button");
let closeFormsBtn = document.createElement("button");
let logoutBtn = document.createElement("button");
let nav = document.getElementById("nav");
let rentedMoviesBtn = document.createElement("button");
let loginPassword = document.createElement("input");
let registerPassword = document.createElement("input");
let allMoviesBtn = document.createElement("button");
let errorMsgContainer = document.createElement("div");
closeFormsBtn.innerText = "Stäng";
closeFormsBtn.classList.add("closeFormsBtn");
logoutBtn.innerText = "Logga ut";
registerButton.innerText = "Registrera";
loginButton.innerText = "Logga in";
logoutBtn.classList.add("logoutBtn");
rentedMoviesBtn.innerText = "Lånade filmer";
rentedMoviesBtn.classList.add("rentedMoviesBtn");
allMoviesBtn.innerText = "Tillgängliga filmer";
userForm.classList.add("form");
registerForm.classList.add("form");

function showNav() {
  nav.innerHTML = "";
  let user = localStorage.getItem("user");
  user
    ? nav.append(allMoviesBtn, rentedMoviesBtn, logoutBtn)
    : nav.append(loginButton, registerButton);
}
function showMovies() {
  content.innerHTML = "";
  getMovies();
  content.append(movielist);
}

function showForms() {
  document.getElementById("nav").style.visibility = "hidden";
  content.innerHTML = "";
  printLoginForm();
  printRegisterForm();
  content.append(closeFormsBtn, userForm, registerForm);
}

function getMovies() {
  const token = localStorage.getItem("user");
  fetch("http://localhost:5001/api/films", {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`,
    },
  })
    .then((res) => res.json())
    .then((data) => printMovies(data));
}
function printMovies(movies) {
  movielist.innerHTML = "";
  const user=localStorage.getItem("user")
  //If user than get ony movies that are available for renting
  const availableMovies = user 
  ? movies.filter((movie) => movie.filmCopies && movie.filmCopies.some((copy) => !copy.isRented)) 
  : movies;
 
  availableMovies.forEach((movie) => {
    let movieCard = document.createElement("li");
    let movieName = document.createElement("p");
    movieName.innerText = movie.title;
    let movieDirector = document.createElement("p");
    movieDirector.innerText = "Regissör: " + movie.director;
    let movieGenre = document.createElement("p");
    movieGenre.innerText = movie.genre;
    let movieImage = document.createElement("img");
    movieImage.src = movie.imageUrl;
    let rentBtn = document.createElement("button");
    rentBtn.innerText = "Låna filmen";
    rentBtn.addEventListener("click", () =>  rentMovie(movie.id));

    let user = localStorage.getItem("user");
    user
      ? rentBtn.classList.add("rentBtn")
      : (rentBtn.style.visibility = "hidden");

    movieCard.append(movieName, movieDirector, movieImage, movieGenre, rentBtn);
    movielist.append(movieCard);
  });
}

function printLoginForm() {
  loginFormText.innerText = "Logga in som filmstudio!";
  loginName.type = "text";
  loginName.placeholder = "Användarnamn";
  loginPassword.type = "password";
  loginPassword.placeholder = "Lösenord";
  loginBtn.innerText = "Logga in";

  loginBtn.addEventListener("click", () => {
    loginUser(loginName.value, loginPassword.value);
  });

  userForm.append(loginFormText, loginName, loginPassword, loginBtn);
}

function printRegisterForm() {
  registerFormText.innerText = "Första gången här? Skapa ett konto.";
  filmStudioName.type = "text";
  filmStudioName.placeholder = "Namn på filmstudion";
  city.type = "text";
  city.placeholder = "Stad";
  userName.type = "text";
  userName.placeholder = "Användarnamn";
  registerPassword.type = "password";
  registerPassword.placeholder = "Lösenord";
  registerBtn.innerText = "Skapa konto";

  registerBtn.addEventListener("click", () => {
    registerUser(
      filmStudioName.value,
      city.value,
      userName.value,
      registerPassword.value
    );
  });

  registerForm.append(
    registerFormText,
    filmStudioName,
    city,
    userName,
    registerPassword,
    registerBtn
  );
}

//AUTH(login register)
function loginUser(userName, password) {
  let user = {
    userName: userName,
    password: password,
  };

  fetch("http://localhost:5001/api/users/authenticate", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(user),
  })
    .then((res) => {
      if (!res.ok) {
        // If answe is not ok throw new error
        return res.json().then((data) => {
          throw new Error(data.message || "Inloggning misslyckades");
        });
      }
      return res.json();
    })
    .then((data) => {
      localStorage.setItem("user", data.token);
      localStorage.setItem("userId", data.filmStudio?.filmStudioId);
      init();
    })
    .catch((error) => {
      printErrormessage(error);
      userForm.append(errorMsgContainer);
    });
}

function registerUser(filmStudioName, city, userName, password) {
  let newUser = {
    filmStudioName: filmStudioName,
    city: city,
    userName: userName,
    password: password,
  };
  fetch("http://localhost:5001/api/filmstudio/register", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(newUser),
  })
    .then((res) => {
      if (!res.ok) {
        return res.json().then((data) => {
          throw new Error(data.message || "Registreringen misslyckades");
        });
      }
      return res.json();
    })
    .then((data) => {
      console.log(data);
      alert("Ditt konto har registrerats!");
      init();
    })
    .catch((error) => {
      printErrormessage(error);
      userForm.append(errorMsgContainer);
    });
}

function logout() {
  localStorage.clear();
  init();
}

// Function to display error messages based on the type of response from the server
function printErrormessage(messages) {
  errorMsgContainer.innerHTML = "";
  // If there are validation errors, display each error message
  if (messages.errors) {
    Object.entries(messages.errors).forEach(([field, messages]) => {
      messages.forEach((msg) => {
        let errorMsg = document.createElement("p");
        errorMsg.innerText = msg;
        errorMsgContainer.append(errorMsg);
      });
    });
  } else if (Array.isArray(messages)) {
    // If the messages are in an array format, display each message description
    messages.forEach((message) => {
      let errorMsg = document.createElement("p");
      errorMsg.innerText = message.description;
      errorMsgContainer.append(errorMsg);
    });
  } else {
    // If it's a single error message, display it
    let errorMsg = document.createElement("p");
    errorMsg.innerText = messages.description || messages.message;
    errorMsgContainer.append(errorMsg);
  }
}
document.querySelectorAll("input").forEach((input) => {
  input.addEventListener("input", () => {
    // Clear error messages when user modifies input fields
    errorMsgContainer.innerHTML = "";
  });
});

function emptyInput() {
  userName.value = "";
  registerPassword.value = "";
  loginName.value = "";
  loginPassword.value = "";
  filmStudioName.value = "";
  city.value = "";
}

//Renting and returning movie functions
function printRentedMovies(movies) {
  movielist.innerHTML = "";

  movies.forEach((movie) => {
    let movieCard = document.createElement("li");
    let movieName = document.createElement("p");
    movieName.innerText = movie.film.title;
    let movieDirector = document.createElement("p");
    movieDirector.innerText = "Regissör: " + movie.film.director;
    let movieGenre = document.createElement("p");
    movieGenre.innerText = movie.film.genre;
    let movieImage = document.createElement("img");
    movieImage.src = movie.film.imageUrl;

    let returnMovieBtn = document.createElement("button");
    returnMovieBtn.innerText = "Lämna tillbaka";
    returnMovieBtn.addEventListener("click", () => returnMovie(movie.film.id));
    returnMovieBtn.classList.add("returnMovieBtn");

    movieCard.append(
      movieName,
      movieDirector,
      movieImage,
      movieGenre,
      returnMovieBtn
    );
    movielist.append(movieCard);
  });
}

function rentMovie(id) {
  const userId = localStorage.getItem("userId");
  const token = localStorage.getItem("user");

  fetch(`http://localhost:5001/api/films/rent?id=${id}&studioid=${userId}`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
  })
    .then((res) => {
      if (!res.ok) {
        throw new Error(`Fel: ${res.status} - ${res.statusText}`);
      }
      return res.json();
    })
    .then(() => {
      alert("Du har lånat filmen!");
      init();
    })
    .catch((error) => {
      alert(error);
      init();
    });
}

function returnMovie(id) {
  const userId = localStorage.getItem("userId");
  const token = localStorage.getItem("user");
  fetch(`http://localhost:5001/api/films/return?id=${id}&studioid=${userId}`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
  })
    .then((res) => {
      if (!res.ok) {
        throw new Error(`Fel: ${res.status} - ${res.statusText}`);
      }
      return res.json();
    })
    .then((data) => {
      alert(data.message);
      getRentedMovies();
    })
    .catch((error) => {
      console.error(error);
    });
}

function getRentedMovies() {
  const token = localStorage.getItem("user");
  fetch("http://localhost:5001/api/mystudio/rentals", {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
  })
    .then((res) => {
      if (!res.ok) {
        throw new Error(`Fel: ${res.status} - ${res.statusText}`);
      }
      return res.json();
    })
    .then((data) => {
      printRentedMovies(data);
    })
    .catch((error) => {
      console.error(error);
    });
}

//Button events
loginButton.addEventListener("click", () => {
  showForms();
  document.getElementById("content").style.flexDirection = "column";
});

registerButton.addEventListener("click", () => {
  showForms();
  document.getElementById("content").style.flexDirection = "column-reverse";
});

closeFormsBtn.addEventListener("click", () => {
  init();
  document.getElementById("content").style.flexDirection = "column";
});

logoutBtn.addEventListener("click", () => logout());

rentedMoviesBtn.addEventListener("click", () => getRentedMovies());
allMoviesBtn.addEventListener("click", () => showMovies());

//init function
function init() {
  document.getElementById("nav").style.visibility = "visible";
  showNav();
  showMovies();
  emptyInput();
}
init();
