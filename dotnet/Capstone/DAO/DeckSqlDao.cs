using Capstone.DAO.Interface;
using Capstone.Exceptions;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class DeckSqlDao : IDeckDao
    {
        private readonly string _connectionString;

        public DeckSqlDao(string dbConnectionString)
        {
            _connectionString = dbConnectionString;
        }

        public Deck CreateDeck(Deck deck)
        {
            string sqlCreateDeck = "INSERT INTO decks (title, tags, desc, user_id) " +
                "OUTPUT INSERTED.deck_id " +
                "VALUES (@title, @tags, @desc, @user_id)";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlCreateDeck, conn))
                    {
                        cmd.Parameters.AddWithValue("@deck_title", deck.title);
                        cmd.Parameters.AddWithValue("@deck_tags", deck.tags);
                        cmd.Parameters.AddWithValue("@deck_desc", deck.desc);
                        cmd.Parameters.AddWithValue("@user_id", deck.userId);

                        deck.deckId = (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return deck;
        }

        public Deck GetDeckById(int deckId)
        {
            Deck deck = new Deck();

            string sqlGetDeckById = "SELECT user_id, deck_id, tags, title, [desc] " +
                         "FROM decks " +
                         "WHERE deck_id = @deck_id";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlGetDeckById, conn);
                    cmd.Parameters.AddWithValue("@deck_id", deckId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        deck = MapRowToDeck(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return deck;
        }

        public List<Deck> GetAllDecks()
        {
            List<Deck> decks = new List<Deck>();

            string sqlGetAllDecks = "SELECT user_id, deck_id, tags, title, [desc] " +
                                    "FROM decks";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlGetAllDecks, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Deck deck = MapRowToDeck(reader);
                        decks.Add(deck);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return decks;
        }

        public Deck UpdateDeck(Deck deck)
        {
            string sqlUpdateDeck = "UPDATE decks " +
                "SET title=@title, tags=@tags, desc=@desc " +
                "WHERE deck_id = @deck_id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sqlUpdateDeck, conn))
                {
                    cmd.Parameters.AddWithValue("@title", deck.title);
                    cmd.Parameters.AddWithValue("@tags", deck.tags);
                    cmd.Parameters.AddWithValue("@desc", deck.desc);
                    cmd.Parameters.AddWithValue("@deck_id", deck.deckId);

                    int count = cmd.ExecuteNonQuery();

                    if (count == 1)
                    {
                        return deck;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        private Deck MapRowToDeck(SqlDataReader reader)
        {
            Deck deck = new Deck();
            deck.userId = Convert.ToInt32(reader["user_id"]);
            deck.title = Convert.ToString(reader["title"]);
            deck.desc = Convert.ToString(reader["desc"]);
            deck.deckId = Convert.ToInt32(reader["deck_id"]);
            deck.tags = Convert.ToString(reader["tags"]);
            return deck;
        }
    }
}
