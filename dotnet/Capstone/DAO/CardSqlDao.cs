using Capstone.DAO.Interface;
using Capstone.Exceptions;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace Capstone.DAO

{
    public class CardSqlDao : ICardDao
    {
        private readonly string _connectionString;

        public CardSqlDao(string dbConnectionString)
        {
            _connectionString = dbConnectionString;
        }

        public Card CreateCard(Card card, int deckId)
        {

            string sqlCreateCard = "INSERT INTO cards (term, definition, user_id) " +
                                   "OUTPUT INSERTED.card_id " +
                                   "VALUES (@term, @definition, @user_id)";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlCreateCard, conn))
                    {
                        cmd.Parameters.AddWithValue("@term", card.term);
                        cmd.Parameters.AddWithValue("@definition", card.definition);
                        cmd.Parameters.AddWithValue("@user_id", card.userId);

                        card.cardId = (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            AddCardToDeck(card.cardId, deckId);

            return card;

        }

        public List<Card> GetAllCards()
        {
            List<Card> cards = new List<Card>();

            string sqlGetAllCards = "SELECT card_id, term, definition, user_id " +
                         "FROM cards";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlGetAllCards, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Card card = MapRowToCard(reader);
                        cards.Add(card);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return cards;
        }

        public List<Card> GetCardsByDeckId(int deckId)
        {
            List<Card> cards = new List<Card>();

            string sqlGetCardsByDeckId = "SELECT cards.card_id, deck_id, term, definition, user_id " +
                                         "FROM cards " +
                                         "JOIN cardxdeck ON cardxdeck.card_id = cards.card_id " +
                                         "WHERE deck_id = @deck_id";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlGetCardsByDeckId, conn);
                    cmd.Parameters.AddWithValue("@deck_id", deckId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Card card = new Card();
                        card = MapRowToCard(reader);
                        cards.Add(card);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return cards;
        }

        public Card UpdateCard(Card updatedCard)
        {
            string sqlUpdateCard = "UPDATE cards " +
                                   "SET term=@term, definition=@definition, user_id=@user_id " +
                                   "WHERE card_id = @card_id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sqlUpdateCard, conn))
                {
                    cmd.Parameters.AddWithValue("@term", updatedCard.term);
                    cmd.Parameters.AddWithValue("@definition", updatedCard.definition);
                    cmd.Parameters.AddWithValue("@user_id", updatedCard.userId);
                    cmd.Parameters.AddWithValue("@card_id", updatedCard.cardId);

                    int count = cmd.ExecuteNonQuery();

                    if (count == 1)
                    {
                        return updatedCard;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public int AddCardToDeck(int cardId, int deckId)
        {
            int resultDeckId;

            string sqlAddCardToDeck = "INSERT INTO cardxdeck(deck_id, card_id) " +
                "OUTPUT INSERTED.card_id " +
                "VALUES (@deck_id, @card_id)";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sqlAddCardToDeck, conn))
                    {
                        cmd.Parameters.AddWithValue("@card_id", cardId);
                        cmd.Parameters.AddWithValue("@deck_id", deckId);

                        resultDeckId = (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return resultDeckId;

        }

        public int DeleteCardById(int cardId, int deckId)
        {
            int numOfRows = 0;

            string sqlDeleteCardById = "DELETE FROM cardxdeck " +
                                       "WHERE card_id=@card_id AND deck_id=@deck_id";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlDeleteCardById, conn);
                    cmd.Parameters.AddWithValue("@card_id", cardId);
                    cmd.Parameters.AddWithValue("@deck_id", deckId);

                    numOfRows = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("Sql Exception Occurred", ex);
            }

            return numOfRows;
        }

        private Card MapRowToCard(SqlDataReader reader)
        {
            Card card = new Card();
            card.cardId = Convert.ToInt32(reader["card_id"]);
            card.term = Convert.ToString(reader["term"]);
            card.definition = Convert.ToString(reader["definition"]);
            card.userId = Convert.ToInt32(reader["user_id"]);
            return card;
        }

    }
}
