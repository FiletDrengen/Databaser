using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace Databaser
{
    public class Login : GameObject
    {
        private SpriteFont userfont;
        public static string user;
        public static string password;

        public static void DatabaseSetup()
        {
            var connection = new SQLiteConnection("Data Source=Fisker.db; Version=3; New=True");
            connection.Open();

            var command = new SQLiteCommand("DROP TABLE Bait", connection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS Bait (bait VARCHAR(18), Alive BOOLEAN)", connection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("INSERT INTO Bait (bait, Alive) VALUES ('Orm', true);", connection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("INSERT INTO Bait (bait, Alive) VALUES ('PowerBait', false);", connection);
            command.ExecuteNonQuery();

            command = new SQLiteCommand("DROP TABLE User", connection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS User (Username VARCHAR(18),Password VARCHAR(18), PRIMARY KEY (Username))", connection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("INSERT INTO User (Username, Password) VALUES ('Admin','Admin');", connection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("INSERT INTO User (Username, Password) VALUES ('Hoffe', '123');", connection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("INSERT INTO User (Username, Password) VALUES ('Kreie','111');", connection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("INSERT INTO User (Username, Password) VALUES ('Pepega', 'Password');", connection);
            command.ExecuteNonQuery();

            command = new SQLiteCommand("DROP TABLE Vehicle", connection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS Vehicle (vehicles VARCHAR(18))", connection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("INSERT INTO Vehicle (vehicles) VALUES ('Boat'); ", connection);
            command.ExecuteNonQuery();

            command = new SQLiteCommand("DROP TABLE Highscore", connection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS Highscore (" +
                "Score INTEGER, " +
                "realm VARCHAR(15)," +
                "Username VARCHAR(15)," +
                "FOREIGN KEY (Username) REFERENCES User(Username))", connection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("INSERT INTO Highscore (Score, realm, Username) VALUES ('100','Sø','Hoffe')", connection);
            command.ExecuteNonQuery();

            command = new SQLiteCommand("DROP TABLE Realm", connection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS Realm (Realms VARCHAR(18))", connection);
            command.ExecuteNonQuery();

            command = new SQLiteCommand("INSERT INTO Realm (Realms) VALUES ('Hav');", connection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("INSERT INTO Realm (Realms) VALUES ('Kyst');", connection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("INSERT INTO Realm (Realms) VALUES ('Flod');", connection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("INSERT INTO Realm (Realms) VALUES ('Sø');", connection);
            command.ExecuteNonQuery();

            command = new SQLiteCommand("DROP TABLE FishList", connection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS FishList (FISH VARCHAR(18), Value INTEGER)", connection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("INSERT INTO FishList (FISH,Value) VALUES ('Gold','10');", connection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("INSERT INTO FishList (FISH,Value) VALUES ('Puffer','10');", connection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("INSERT INTO FishList (FISH,Value) VALUES ('Shark','10');", connection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("INSERT INTO FishList (FISH,Value) VALUES ('Eel','10');", connection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("INSERT INTO FishList (FISH,Value) VALUES ('Green','10');", connection);
            command.ExecuteNonQuery();

            string querry = "Select * from User Where Username = '" + user + "' and Password = '" + password + "'";

            SQLiteDataAdapter sda = new SQLiteDataAdapter(querry, connection);
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);

            //command = new SQLiteCommand("SELECT * from Bait", connection);
            //var dataset = command.ExecuteReader();
            //while (dataset.Read())
            //{
            //    var test = dataset.GetBoolean(1);
            //    //var id = dataset.GetInt32(0);
            //    var name = dataset.GetString(0);
            //    Console.WriteLine($"{name}  {test} ");
            //}
            //connection.Close();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}