<template>
  <div id="capstone-app">
    <nav id="nav-header">
      <NavHeader />
    </nav>
    <router-view id="view" />
  </div>
</template>

<script>
import NavHeader from './components/NavHeader.vue';

export default {
  components: {
    NavHeader
  }
}
</script>

<style>
  @import url('https://fonts.googleapis.com/css2?family=Architects+Daughter&family=Josefin+Sans:wght@600&display=swap');


  /** Universal Styling */

* {
  font-family: 'Josefin Sans', sans-serif;
}

#deck-title {
  font-family: 'Josefin Sans', sans-serif;
  font-weight: bolder;
  font-size: 4rem;
  text-shadow: .01rem 0rem .75rem #FF9233;
}

.deckcontainer > h2 {
  font-family: 'Architects Daughter', cursive;
}

#title > a {
  font-family: 'Josefin Sans', sans-serif;
}

body {
  background: url("../src/imgs/BackgroundImg.jpg")no-repeat center center fixed;
  -webkit-background-size: cover;
  -moz-background-size: cover;
  -o-background-size: cover;
  background-size: cover;
}

a {
  text-decoration: none;
  color: #E5AC65;
}

/** Nav */

#nav-header {
  margin-bottom: 7rem;
  width: 100vh;
}

@media only screen and (max-width:62rem) and (min-width:37.6rem) {
  #nav-header {
    margin-bottom: 9rem;
  }
}

@media only screen and (max-width: 37.5rem) {
  #nav-header {
    margin-bottom: 12rem;
  }
}

/** Form Styling */

form {
  margin-top: auto;
  background-color: #1C0B00;
  padding: 1.5rem;
  border-radius: .5rem;
  box-shadow: 0 0 2rem #2D1800;
  width: 100%;
  max-width: 25rem;
}

form h1 {
  text-align: center;
  color: #E5AC65;
  font-size: x-large;
  margin-bottom: 2vh;
}

form label {
  display: block;
  margin-bottom: .5vh;
  color: #E5AC65;
}

form input {
  width: 90%;
  padding: .5rem;
  margin-bottom: 1rem;
  border: .05rem solid #2D1800;
  border-radius: .5rem;
  background-color: #E5AC65;

}

#save {
  width: 95%;

  padding: 1rem;

  background-color: #643102;
  color: #E5AC65;
  border: none;
  border-radius: .5rem;
}

#save:hover {
  cursor: pointer;
  transition: background-color 0.3s ease;
  background-color: #753B00;
}

/** Card Form Only */

.card-form,
.deck-form,
.login-form {
  margin: 1rem 0rem 2rem 5%;
}

/** Deck Detail Mini Nav Button Styling */

.form-button {
  margin: 1rem 1rem .5rem 1rem;
  padding: .5rem 1rem .5rem 1rem;
  align-items: center;

  border-radius: .5rem;
  background-color: #643102;
  border: 1px solid #1C0B00;
  box-shadow: 0 0 .5rem #2D1800;
  color: #E5AC65
}

.form-button:hover {
  transition: background-color 0.3s ease;
  cursor: pointer;

  background-color: #753B00;
  
}

/** Deck Details Styling */

.deckcontainer,
.cardcontainer {
  margin: auto;
  margin-top: .5rem;
  padding: 0rem 1.2rem .5rem 1.2rem;
  background-color: #1C0B00;
  width: 90%;

  border: .05rem solid #2D1800;
  border-radius: 1rem;
  box-shadow: 0 0 1rem #2D1800;
  color: #E5AC65;
}

.deckdetail-button-container {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  justify-content: center;
  margin: 0.5rem 5% 1rem 5%;
  color: #E5AC65
}
</style>