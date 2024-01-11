<template>
    <div id="NewDeckFormView">
        <DeckForm v-bind:deck="deck"/>
    </div>
    
</template>

<script>
import DeckForm from '../components/DeckForm.vue'

export default{
    components:{
        DeckForm
    },
    data(){
        return{
            deck:{
                deckId: 0,
                userId: 0,
                title:'',
                desc:'',
                tags:''
            }
        }
    }
}
</script>

<style scoped>
#deck-form-container {
    display: flex;
    justify-content: center;
    height: fit-content;
}
</style>