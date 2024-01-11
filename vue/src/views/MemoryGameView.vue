<template>
    <div v-if="!isStarted" class="opening-popup" >
        <h1>Memory Game</h1>
        <button class="form-button" v-on:click="startGame">Start</button>
        <button class="form-button"><router-link :to="{ name: 'deck-detail' }">Back to Deck</router-link></button>
    </div>
    <div>
        <MemoryGame v-if="isStarted" v-bind:cards="cards"/>
    </div>
</template>

<script>
import { RouterLink } from 'vue-router'
import MemoryGame from '../components/MemoryGame.vue'
import CardService from '../services/CardService'

export default {
    data() {
        return {
            isStarted: false,
            cards: []
        }
    },
    components: {
    MemoryGame,
    RouterLink
},
    created() {
        CardService
            .getCardsByDeckId(this.$route.params.deckId)
            .then(response => {
                if(response.status === 200) {
                    this.cards = response.data;
                }
            })
    },
    methods: {
        startGame() {
            this.isStarted = true;
        }    
    }
}
</script>

<style>
.opening-popup {
    display: flex;
    flex-wrap: wrap;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    background-color: #2D1800;
    border-radius: .5rem;
    border: .05rem solid #2D1800;
    box-shadow: 0 0 .25rem #2D1800;
    color: #E5AC65;
    width: fit-content;
    padding: 5rem;
    margin: auto;
}
</style>