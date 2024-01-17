<template>
    <div class="deck-detail-container">
        <DeckDetails v-bind:deck="deck" />
    </div>
    <div class="deckdetail-button-container">
        <button class="form-button" v-if="!isFormDisplayed" v-on:click="displayForm">Create Card</button>
        <button class="form-button" v-if="isFormDisplayed" v-on:click="cancelForm">Cancel</button>
        <button class="form-button" v-on:click="editDeck">Edit Deck</button>
        <button class="form-button" v-on:click="manageCardsView">Manage Cards</button>
        <button class="form-button" v-on:click="beginStudySession">Begin Study Session</button>
        <button class="form-button" v-on:click="memoryGame">Memory Game</button>
        <button class="form-button" v-on:click="raceGame">Race Game</button>
    </div>
    <div id="cardform">
        <CardForm v-if="isFormDisplayed" v-bind:card="card" />
    </div>
    <div class="card-container">
        <CardDetails id="cardsInDeck" v-for="currentCard in cards" v-bind:key="currentCard.cardId"
            v-bind:currentCard="currentCard" />
    </div>
</template>

<script>
import CardService from '../services/CardService.js';
import DeckService from '../services/DeckService.js';

import CardDetails from '../components/CardDetails.vue';
import CardForm from '../components/CardForm.vue';
import DeckDetails from '../components/DeckDetails.vue';

export default {
    data() {
        return {
            deck: {},
            cards: {},
            card: {
                cardId: 0,
                deckId: this.$route.params.deckId,
                cardTerm: '',
                cardDefinition: '',
                userId: 0,
            },
            isFormDisplayed: false,
        }
    },
    components: {
        DeckDetails,
        CardForm,
        CardDetails
    },
    methods: {
        displayForm() {
            this.isFormDisplayed = true;
        },
        editDeck() {
            this.$router.push({ name: 'deck-edit', params: { deckId: this.deck.deckId } })
        },
        manageCardsView() {
            this.$router.push({ name: 'managecards', params: { deckId: this.deck.deckId } })
        },
        cancelForm() {
            this.isFormDisplayed = false;
        },
        memoryGame() {
            this.$router.push({ name: 'memory-game', params: { deckId: this.deck.deckId } })
        },
        raceGame() {
            this.$router.push({ name: 'race-game', params: { deckId: this.deck.deckId } })
        },
        beginStudySession() {
            this.$router.push({ name: 'study-session', params: { deckId: this.deck.deckId } })
        },
        getDeck() {
            DeckService.getDeckById(this.$route.params.deckId)
                .then(response => {
                    this.deck = response.data;
                })
                .catch(error => {
                    this.handleError();
                })
        },
        getCards() {
            CardService.getCardsByDeckId(this.$route.params.deckId)
                .then(response => {
                    this.cards = response.data;
                })
                .catch(error => {
                    this.handleError();
                })
        }
    },
    created() {
        this.getDeck();
        this.getCards();
    }
}
</script>