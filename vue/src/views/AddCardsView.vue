<template>
    <ModifyDeck />
</template>

<script>
import ModifyDeck from '../components/ModifyDeck.vue';
import CardService from '../services/CardService.js';

export default {
    data() {
        return {
            oldDeck: [],
            deckId: this.$route.params.deckId,
        }
    },
    components: {
        ModifyDeck,
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