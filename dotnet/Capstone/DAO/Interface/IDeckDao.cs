using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO.Interface
{
    public interface IDeckDao
    {
        Deck CreateDeck(Deck deck);
        List<Deck> GetAllDecks();
        Deck GetDeckById(int deckId);
        Deck UpdateDeck(Deck deck);
    }
}
