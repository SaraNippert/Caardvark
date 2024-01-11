<template>
    <div class="cardcontainer">
        <div id="card-details-container">
            <p id="card-details-term">{{ currentCard.term }}</p>
            <p id="card-details-definition">{{ currentCard.definition }}</p>
        </div>
        <div id="card-edit-buttons">
            <div id="edit-buttons">
                <button class="form-button" id="card-editBtn" v-on:click="editCard"
                    v-if="!isFormDisplayed && isEditPage">Edit</button>
                <button class="form-button" id="card-editBtn" v-on:click="cancelCard"
                    v-if="isFormDisplayed && isEditPage">Cancel</button>
            </div>
            <div id="delete-button">
                <button class="form-button" id="card-editBtn" v-on:click="deleteCard"
                    v-if="isEditPage && !isDeleting">Delete</button>
                <button class="form-button" id="card-cancelBtn" v-on:click="deleteCard" v-if="isEditPage && isDeleting">Cancel
                    Delete</button>
            </div>
        </div>
        <DeleteConfirmation class="cardcontainer" v-if="isDeleting" v-bind:card="currentCard" />
    </div>
    <CardForm v-if="isFormDisplayed" v-bind:card="currentCard" />
</template>

<script>
import CardForm from '../components/CardForm.vue';
import DeleteConfirmation from './DeleteConfirmation.vue';

export default {
    data() {
        return {
            isFormDisplayed: false,
            isDeleting: false,

        }
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
        editCard() {
            this.isFormDisplayed = true;
        },
        cancelCard() {
            this.isFormDisplayed = false;
        },
        deleteCard(card) {
            this.isDeleting = !this.isDeleting;
        }
    },
    components: {
        CardForm,
        DeleteConfirmation
    },
    props:
        ['currentCard']
}
</script>

<style>
#card-details-container {
    display: flex;
    flex-direction: row;
    justify-content: space-between;

}

#card-details-term {
    margin-right: 2rem;
    width: 6rem;
}

#card-details-definition {
    display: flex;
    text-align: right;
}

#card-edit-buttons {
    display: flex;
}

#card-editBtn {
    margin: .25rem;
}


</style>