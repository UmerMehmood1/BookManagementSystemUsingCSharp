using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BookVP
{
    public class BookDAL
    {
        private readonly string dataSourceString = "Data Source=UMER;Initial Catalog=BookDB;Integrated Security=True;Encrypt=False";

        public void AddBook(Book newBook)
        {
            using (SqlConnection connection = new SqlConnection(dataSourceString))
            {
                string query = "INSERT INTO Books (Title, Author, Genre) VALUES (@Title, @Author, @Genre)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", newBook.Title);
                command.Parameters.AddWithValue("@Author", newBook.Author);
                command.Parameters.AddWithValue("@Genre", newBook.Genre);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Book added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to add book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void UpdateBook(string oldTitle, Book updatedBook)
        {
            using (SqlConnection connection = new SqlConnection(dataSourceString))
            {
                string query = "UPDATE Books SET Title = @NewTitle, Author = @NewAuthor, Genre = @NewGenre WHERE Title = @OldTitle";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@OldTitle", oldTitle);
                command.Parameters.AddWithValue("@NewTitle", updatedBook.Title);
                command.Parameters.AddWithValue("@NewAuthor", updatedBook.Author);
                command.Parameters.AddWithValue("@NewGenre", updatedBook.Genre);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Book updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to update book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void DeleteBook(string title)
        {
            using (SqlConnection connection = new SqlConnection(dataSourceString))
            {
                string query = "DELETE FROM Books WHERE Title = @Title";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", title);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Book deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to delete book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();
            using (SqlConnection connection = new SqlConnection(dataSourceString))
            {
                string query = "SELECT * FROM Books";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Book book = new Book()
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Title = reader["Title"].ToString(),
                        Author = reader["Author"].ToString(),
                        Genre = reader["Genre"].ToString()
                    };
                    books.Add(book);
                }
            }
            return books;
        }
    }
}
