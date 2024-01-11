<template>
        <button class="form-button" v-on:click="save(); displayPopUp()">Save</button>
        <button class="form-button"><router-link :to="{ name: 'deck-detail' }">Back to Deck</router-link></button>
        <div id="save-popup" class="form-button" v-if="isSaved">
            <span>Saved!</span>
        </div>

    <div id="modify-deck-container">
        <div id="current-deck-container">
            <h1>Current Deck</h1>
            <draggable class="scroll" id="draggable" v-model="currentCards" tag="ul" group="meals" item-key="card.cardId">
                <template #item="{ element: card }">
                    <li class="cardcontainer">
                        <p>{{ card.term }}</p>
                        <p>{{ card.definition }}</p>
                    </li>
                </template>
            </draggable>
        </div>

        <div id="all-cards-container">
            <h1>All Cards</h1>
            <draggable class="scroll" v-model="allCards" tag="ul" group="meals" item-key="card.cardId">
                <template #item="{ element: card }">
                    <li class="cardcontainer">
                        <p>{{ card.term }}</p>
                        <p>{{ card.definition }}</p>
                    </li>
                </template>
            </draggable>
        </div>
    </div>
</template>



<script>
import CardService from '../services/CardService';
import { ref, onMounted } from 'vue';
import draggable from 'vuedraggable';
import { useRoute } from 'vue-router';

/** In vue3, refs should be accessed using .value instead of this. */


/** In Vue 3, the data property is handled differently compared to Vue 2. 
 * Instead of defining data as a function that returns an object, 
 * you can use the Composition API and define your reactive data using the ref function. 
 * */

export default {
    setup() {
        /**vue3 router requires an import and the use of this constant variable */
        const route = useRoute();
        const deckId = route.params.deckId

        const oldDeck = ref([]);
        const currentCards = ref([]);
        const allCards = ref([]);
        const temp = ref([]);
        let isSaved = ref([]);

        const fetchData = () => {
            CardService.getCardsByDeckId(deckId)
                .then((responseCurrent) => {
                    oldDeck.value = responseCurrent.data;
                    currentCards.value = responseCurrent.data;
                    console.log('in fetchData currentDeck:', currentCards.value);
                    console.log('in fetchData oldDeck:', oldDeck.value);
                })
                .catch((error) => {
                    console.error('An error occurred while fetching current cards:', error);
                });

            CardService.getAllCards()
                .then((responseAll) => {
                    temp.value = responseAll.data;
                    console.log('in fetchData temp:', temp.value)

                    /** filtering cards based on cards alone was not specific enough.
                     * changed to filtering based on cardId by creating an array of cardIds
                     * then using some() to filter based on the cardIds
                     * some() tests if at least one element in the array passes the test and returns a bool
                     */
                    const currentCardIds = currentCards.value.map(card => card.cardId);

                    allCards.value = temp.value.filter((card) => {
                        /** add all cards not in the currentdeck */
                        if (currentCardIds.some(cardId => cardId === card.cardId)) {
                            return false
                        } else {
                            return true
                        }
                    })
                })
                .catch((error) => {
                    console.error('An error occurred while fetching all cards:', error);
                });
        };


        /** methods do not have to be defined inside of setup(),
         * but methods outside of setup don't have access to state within setup() directly
         * save must be within setup due to the draggable arrays.
         * The draggable arrays must be accessed in order to save them
         */
        const save = () => {

            /**filter cards in current cards,*/
            let cardsToAdd = currentCards.value.filter((card) => {
                /** if the old deck includes the card,
                  it does not need to be added and can be filtered out
                  includes returns true if the array does include the object
                */
                return !oldDeck.value.includes(card)

            })

            console.log('in save of modify deck, cardsToAdd:', cardsToAdd)

            cardsToAdd.forEach((card) => {
                console.log('in ModifyDeck add card', card)
                card.deckId = deckId
                CardService.addCardToDeck(card)

            });


            let cardsToDelete = allCards.value.filter((card) => {
                /** if the old deck includes the card,
                 * it does not need to be added and can be filtered out
                 * includes returns true if the array does include the object
                */
                return oldDeck.value.includes(card)

            })

            console.log('in modifyDeck cardsToDelete', cardsToDelete)

            cardsToDelete.forEach((card) => {
                card.deckId = deckId
                CardService.deleteCardFromDeck(card)
            });
        };

        onMounted(() => {
            fetchData();
        });

        return { deckId, oldDeck, currentCards, allCards, save };
    },
    components: {
        draggable,
    },
    data() {
        return {
            isSaved: false,
        }
    },
    methods: {
        displayPopUp() {
            this.isSaved = true;
            
            setTimeout(() => { this.isSaved = false }, 1000)
        }
    }
};

</script>

<style>
/**popUp Styling */

#save-popup {
    position: absolute;
    left: 1rem;
    display: float;
    background-color: #FF9233;
    height: fit-content;
    border-radius: .5rem;
    border: .05rem solid #C4782A;
    box-shadow: 0 0 .25rem #C4782A;
    color: #1C0B00;
}

#modify-deck-container .cardcontainer {
    max-width: 30vw;
}

.cards {
    display: flex;
    flex-wrap: wrap;
    justify-content: center;
    justify-content: space-around;
}

ul {
    list-style: none;
}

#modify-deck-container {
    display: flex;
    flex-direction: row;
    justify-content: space-evenly;
}

#current-deck-container {
    max-width: 50vw;
    margin: 1rem;
}

#all-cards-container {
    margin: 1rem;
    max-width: 50vw;
}

/* width */
::-webkit-scrollbar {
    width: 1rem;
}

/* Track */
::-webkit-scrollbar-track {
    background: #E5AC65;
}

/* Handle */
::-webkit-scrollbar-thumb {
    background: #643102;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
    background: #753B00;
}

.scroll {
    display: block;
    padding: .1rem;
    margin-top: 1rem;
    width: fit-content;
    height: 55vh;
    overflow-y: scroll;
}
</style>
