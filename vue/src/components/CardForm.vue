<template>
    <div class="card-container">
        <form v-on:submit.prevent="submitForm" class="card-form">
            <div class="field">
                <label for="term">Term</label>
                <input type="text" id="term" name="term" v-model="newCard.term">
            </div>
            <div class="field">
                <label for="definition">Definition</label>
                <input type="text" id="definition" name="definition" v-model="newCard.definition">
            </div>
            <button id="save" type="submit" value="Save">Save</button>
        </form>
    </div>
</template>

<script>
import CardService from '../services/CardService';

export default{
    props: {
        card: {
            type: Object,
            required: true
        }
    },
    data() {
        return {
            newCard: {
                cardId: this.card.cardId,
                deckId: this.card.deckId,
                term: this.card.term,
                definition: this.card.definition,
                userId: this.card.userId
            }
        };
    },
    methods: {
        submitForm() {
            if (!this.validateForm()) {
                return;
            }
            if (this.newCard.cardId === 0) {
                CardService
                    .createCard(this.newCard)
                    .then(response => {
                        if (response.status === 201) {
                            this.$store.commit(
                                'SET_NOTIFICATION',
                                {
                                    message: 'A new card was added.',
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
            else {
                CardService
                    .updateCard(this.newCard)
                    .then(response => {
                        if (response.status === 200) {
                            this.$store.commit(
                                'SET_NOTIFICATION',
                                {
                                    message: 'A new card was added.',
                                    type: 'success'
                                }
                            );
                            window.location.reload();
                            console.log("Reached update Card!");
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
            if (this.newCard.term.length === 0) {
                msg += 'The new card must have a title. ';
            }
            if (this.newCard.definition.length === 0) {
                msg += 'The new card must have a Description.';
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