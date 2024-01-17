<template>
    <div id="deck-form-container">
        <form v-on:submit.prevent="submitForm" class="deck-form">
            <h1>{{isNewCardPage}}</h1>
            <div class="field">
                <label for="title">Title</label>
                <input type="text" id="title" name="title" v-model="newDeck.title">
            </div>
            <div class="field">
                <label for="desc">Description</label>
                <input type="text" id="desc" name="desc" v-model="newDeck.desc" />
            </div>
            <div class="field">
                <label for="tags">Tags</label>
                <input type="text" id="tags" name="tags" v-model="newDeck.tags" />
            </div>
            <button id="save" type="submit" value="Save">Save</button>

        </form>
    </div>
</template>

<script>
import DeckService from '../services/DeckService.js';

export default {
    computed: {
        isNewCardPage() {
            if(this.$route.name == 'newdeck') {
                return 'Create New Deck'
            }
            return 'Edit Deck';
        }
    },
    props: {
        deck: {
            type: Object,
            required: true
        }
    },
    data() {
        return {
            newDeck: {
                deckId: this.deck.deckId,
                title: this.deck.title,
                tags: this.deck.tags,
                desc: this.deck.desc,
                userId: this.deck.userId
            }
        };
    },
    methods: {
        submitForm() {
            if (!this.validateForm()) {
                return;
            }
            if (this.newDeck.deckId === 0) {
                DeckService
                    .createDeck(this.newDeck)
                    .then(response => {
                        if (response.status === 201) {
                            this.$store.commit(
                                'SET_NOTIFICATION',
                                {
                                    message: 'A new deck was added.',
                                    type: 'success'
                                }
                            );
                            this.$router.push({
                                name: 'home',
                            });
                        }
                    })
                    .catch(error => {
                        this.handleErrorResponse(error, 'adding');
                    });
            }
            else {
                DeckService
                    .updateDeck(this.newDeck)
                    .then(response => {
                        if (response.status === 200) {
                            this.$store.commit(
                                'SET_NOTIFICATION',
                                {
                                    message: 'A deck was modified.',
                                    type: 'success'
                                }
                            );
                            window.location.reload();
                        }
                    })
                    .catch(error => {
                        this.handleErrorResponse(error, 'adding');
                    })
            }

        },
        handleErrorResponse(error, verb) {
            if (error.response) {
                this.$store.commit('SET_NOTIFICATION',
                    "Error " + verb + " card. Response received was '" + error.response.statusText + "'.");
            } else if (error.request) {
                this.$store.commit('SET_NOTIFICATION', "Error " + verb + " card. Server could not be reached.");
            } else {
                this.$store.commit('SET_NOTIFICATION', "Error " + verb + " card. Request could not be created.");
            }
        },
        validateForm() {
            let msg = '';
            if (this.newDeck.title.length === 0) {
                msg += 'The new card must have a title. ';
            }
            if (this.newDeck.desc.length === 0) {
                msg += 'The new card must have a Description.';
            }
            if (this.newDeck.tags.length === 0) {
                msg += 'The new card must have a Tags.';
            }
            if (msg.length > 0) {
                this.$store.commit('SET_NOTIFICATION', msg);
                return false;
            }
            return true;
        }

    }
}
</script>

<style>
#deck-form-container{
    margin-top: 3rem;
}
</style>