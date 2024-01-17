<template>
    <DragDeckCards />
</template>

<script>
import DragDeckCards from '../components/DragDeckCards.vue';
import CardService from '../services/CardService.js';

export default {
    data() {
        return {
            oldDeck: [],
            deckId: this.$route.params.deckId,
        }
    },
    components: {
        DragDeckCards
},
    methods: {
        getCards() {
            CardService.getCardsByDeckId(this.$route.params.deckId)
                .then(response => {
                    this.oldDeck = response.data;
                });
        }

    }
}
</script>