using System.Collections.Generic;
using Capstone.Models;


namespace Capstone.DAO.Interface
{
    public interface ICardDao
    {
        Card CreateCard(Card card, int deckId);
        List<Card> GetAllCards();
        List<Card> GetCardsByDeckId(int deckId);
        Card UpdateCard(Card updatedCard);
        int AddCardToDeck(int cardId, int deckId);
        int DeleteCardById(int cardId, int deckId);
    }
}
