using Capstone.DAO.Interface;
using Capstone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DeckController : ControllerBase
    {
        public IDeckDao DeckDao;
        public IUserDao UserDao;

        public DeckController(IDeckDao deckDao, IUserDao userDao)
        {
            this.DeckDao = deckDao;
            this.UserDao = userDao;
        }

        [HttpPut("{deckId}")]
        public ActionResult<Deck> UpdateDeck(Deck deck)
        {
            Deck newDeck = DeckDao.UpdateDeck(deck);

            if (newDeck == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(newDeck);
            }
        }

        [HttpGet()]
        public ActionResult<IList<Deck>> GetDecks()
        {
            IList<Deck> decks = DeckDao.GetAllDecks();
            return Ok(decks);
        }

        [HttpGet("{deckId}")]
        public ActionResult<Deck> GetDeckById(int deckId)
        {
            Deck deck = DeckDao.GetDeckById(deckId);

            if (deck != null)
            {
                return Ok(deck);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost()]
        public ActionResult<Deck> CreateDeck(Deck deck)
        {
            User user = UserDao.GetUserByUsername(User.Identity.Name);
            deck.userId = user.UserId;

            Deck added = DeckDao.CreateDeck(deck);

            return Created($"/deck/{added.deckId}", added);
        }
    }
}
