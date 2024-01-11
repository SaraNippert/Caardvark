<template>
    <div @click="flipCard">
        <div id="card-front" v-show="!isToggle" class="animated flipInX flashcard">
            <h2>{{ currentCard.term }}</h2>
            <p>Click to show definition</p>
        </div>
        <div id="card-back" v-show="isToggle" class="animated flipInX flashcard">
            <h2>{{ currentCard.definition }}</h2>
            <p>Click to show term</p>
        </div>
    </div>
</template>
  
<script>
import 'primeicons/primeicons.css'

export default {
    props: {
        currentCard: {
            type: Object,
        },
    },
    computed: {
        isToggle() {
            return this.$store.state.cardFlipped
        },
    },
    methods: {
        flipCard() {
            this.$store.commit('FLIP_CARD')
        },
    },
}
</script>
  
<style scoped>

* {
    color: #E5AC65;
}

/** Flashcard Styling */

.flashcard {
    display: flex;
    flex-direction: column;
    justify-content: center;
    text-align: center;
    width: 45rem;
    height: 15rem;
    margin: .5rem;
    padding: 1rem;
    cursor: pointer;
    border-radius: 1rem;
    box-shadow: 0 0px 10px rgba(0, 0, 0, 0.4);
}

#card-front {
    background-color: #1C0B00;
}

#card-back {
    background-color: #643102;
}

h2 {
    font-size: 2em;
}

p {
    font-size: 1em;
}

/** Card Flip Animation */
.animated {
    animation-duration: 0.75s;
    animation-fill-mode: both;
}

@keyframes flipInX {
    from {
        transform: perspective(400px) rotate3d(1, 0, 0, 90deg);
        animation-timing-function: ease-in;
        opacity: 0;
    }

    40% {
        transform: perspective(400px) rotate3d(1, 0, 0, -10deg);
        animation-timing-function: ease-in;
    }

    60% {
        transform: perspective(400px) rotate3d(1, 0, 0, 10deg);
        opacity: 1;
    }

    80% {
        transform: perspective(400px) rotate3d(1, 0, 0, -5deg);
    }

    to {
        transform: perspective(400px);
    }
}

.flipInX {
    backface-visibility: visible;
    animation-name: flipInX;
}
</style>