<template>
    <div class="deckcontainer">
        <h2>
            {{ deck.title }}
        </h2>
        <div>
            <p>{{ deck.desc }}</p>
            <p id="deck-detail-tag">{{ deck.tags }}</p>
        </div>
        <button class="form-button" id="deck-editBtn" v-on:click="editDeck" v-if="!isEditing && isEditPage">Edit</button>
        <button class="form-button" id="deck-editBtn" v-on:click="cancelDeck" v-if="isEditing && isEditPage">Cancel</button>
    </div>
    <div>
        <DeckForm v-if="isEditing" v-bind:deck="deck" />
    </div>
</template>

<script>
import DeckForm from './DeckForm.vue';


export default {
    data() {
        return {
            isEditing: false,
        };
    },
    computed: {
        isEditPage() {
            if (this.$route.name == 'deck-edit') {
                return true;
            }
            return false;
        }
    },
    methods: {
        editDeck() {
            this.isEditing = true;
        },
        cancelDeck() {
            this.isEditing = false;
        }
    },
    props: ['deck'],
    components: { DeckForm }
}
</script>

<style>
#deck-detail-tag {
    margin: 1rem 1rem .5rem 1rem;
    padding: .5rem 1rem .5rem 1rem;

    border-radius: .5rem;
    background-color: #643102;
    border: 1px solid #1C0B00;
    box-shadow: 0 0 .5rem #2D1800;
    color: #E5AC65;
    width: fit-content;
    font-size: x-small;
}
</style>
