import axios from 'axios';

export default {

  getDecks() {
    console.log('Reached getdecks in deck service')
    return axios.get('/deck')
  },

  getDeckById(deckId){
    return axios.get(`/deck/${deckId}`)
  },

  createDeck(deck) {
    console.log('Reached createDeck in deck service')
    return axios.post('/deck', deck)
  },

  updateDeck(deck) {
    console.log(deck)
    return axios.put(`/deck/${deck.deckId}`, deck)
  }

}
