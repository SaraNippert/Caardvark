<template>
    <div class="deck-detail-container">
        <DeckDetails v-bind:deck="deck" />
    </div>
    <div class="deckdetail-button-container">
        <button class="form-button" v-if="!isFormDisplayed" v-on:click="displayForm">Create Card</button>
        <button class="form-button" v-if="isFormDisplayed" v-on:click="cancelForm">Cancel</button>
        <button class="form-button" v-on:click="finishDeck">Finish Edit</button>
    </div>
    <div id="cardform">
        <CardForm v-if="isFormDisplayed" v-bind:card="card" />
    </div>
    <!--add view of all cards-->
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
        finishDeck() {
            this.$router.push({name: 'deck-detail', params:{deckId: this.deck.deckId} })
        },
        cancelForm() {
            this.isFormDisplayed = false;
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