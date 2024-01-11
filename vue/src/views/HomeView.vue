<template>
  <div class="loading" v-if="isLoading">
    <p>Loading...</p>
  </div>
  <div v-else id="home-grid">
    <h1 id="deck-title">Flashcard Decks</h1>
    <DeckList id="home-decklist" v-bind:decks="decks" />
  </div>
  <img id="sittingaardvark" src="../imgs/SittingAardvark.PNG"/>
</template>

<script>
import DeckList from '../components/DeckList.vue';
import deckService from '../services/DeckService.js';

export default {
  components: {
    DeckList,
  },
  data() {
    return {
      decks: [],
      isLoading: true
    };
  },
  methods: {
    getDecks() {
      deckService.getDecks()
        .then(response => {
          this.decks = response.data;
          this.isLoading = false;
        })
        .catch(error => {
          this.handleError();
        })
    },
  },
  created() {
    console.log('Reached created in home view')
    this.getDecks();
  }


};
</script>

<style>
#deck-title {
  margin: auto;
  margin-bottom: 2rem;
  width: fit-content;
  text-align: center;

  padding: .8rem .8rem .8rem .8rem;
}
/** This is for the hover highlight to only apply to DeckList in this View */
#home-decklist > .deck > a > .deckcontainer:hover{
    background-color: #2D1800;
}

#sittingaardvark{
  max-width: 10rem;
  position: fixed;
  right: 1rem;
  bottom: 1rem;
}
</style>
