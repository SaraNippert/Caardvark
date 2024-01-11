using Capstone.DAO.Interface;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CardController : Controller
    {
        public ICardDao CardDao;
        public IUserDao UserDao;

        public CardController(ICardDao cardDao, IUserDao userDao)
        {
            this.CardDao = cardDao;
            this.UserDao = userDao;
        }

        [HttpPut("{cardId}")]
        public ActionResult<Card> Card(Card changedCard)
        {
            Card newCard = CardDao.UpdateCard(changedCard);

            if (newCard == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(newCard);
            }

        }

        [HttpGet()]
        public ActionResult<List<Card>> GetAllCards()
        {
            List<Card> cards = CardDao.GetAllCards();
            return Ok(cards);
        }

        [HttpGet("deck/{deckId}")]
        public ActionResult<List<Card>> GetCards(int deckId)
        {
            List<Card> cards = CardDao.GetCardsByDeckId(deckId);
            return Ok(cards);
        }

        [HttpPost()]
        public ActionResult<Card> CreateCard(JsonCard card)
        {
            Card newCard = new Card();
            newCard.cardId = card.cardId;
            newCard.definition = card.definition;
            newCard.term = card.term;

            User user = UserDao.GetUserByUsername(User.Identity.Name);
            newCard.userId = user.UserId;

            Card added = CardDao.CreateCard(newCard, card.deckId);

            return Created($"/card/{added.cardId}", added);
        }

        [HttpPost("{cardId}/{deckId}")]
        public ActionResult<int> AddCardToDeck(JsonCard card)
        {
            int result = CardDao.AddCardToDeck(card.cardId, card.deckId);

            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }

        [HttpDelete("{cardId}/{deckId}")]
        public ActionResult DeleteCard(int cardId, int deckId)
        {
            int numOfDeleted = CardDao.DeleteCardById(cardId, deckId);

            if (numOfDeleted == 1)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
