<template>
    <div class="decks">
        <div class="deck" v-for="deck in decks" v-bind:key="deck.deckId">
            <router-link v-bind:to="{
                name: 'deck-detail',
                params: { deckId: deck.deckId }
            }">
                <DeckDetails v-bind:deck="deck"/>
            </router-link>
        </div>
    </div>
</template>

<script>
import DeckDetails from './DeckDetails.vue';

export default {
    components: {
        DeckDetails
    },
    props: {
        decks: {
            type: Array,
            required: true
        }
    }
};
</script>

<style>

.decks {
    display: flex;
    flex-wrap: wrap;
    justify-content: center;
    justify-content: space-around;
    margin: 4rem
}
.deck {
    margin: 2rem;
}

</style>