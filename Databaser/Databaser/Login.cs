﻿using Microsoft.Xna.Framework;
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
        public Texture2D loginTexture;
        public static ContentManager content;
        public Rectangle Rectangle;
        public static SQLiteConnection connection = new SQLiteConnection("Data Source=Fisker.db; Version=3; New=True");

        public Login(Rectangle newRectangle)
        {
            loginTexture = content.Load<Texture2D>("Tile1");
            this.Rectangle = newRectangle;
        }

        public static void DatabaseSetup()
        {
            SQLiteConnection connection = new SQLiteConnection("Data Source=Fisker.db; Version=3; New=True");
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
            command = new SQLiteCommand("INSERT INTO User (Username, Password) VALUES ('ADMIN','ADMIN');", connection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("INSERT INTO User (Username, Password) VALUES ('HOFFE', 'HOFFE');", connection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("INSERT INTO User (Username, Password) VALUES ('KREIE','FCM');", connection);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("INSERT INTO User (Username, Password) VALUES ('PEPEGA', 'PASSWORD');", connection);
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

            connection.Close();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(loginTexture, Rectangle, Color.Gray);
        }

        public static bool Userlogin(string user, string pass)
        {
            SQLiteConnection connection = new SQLiteConnection("Data Source=Fisker.db; Version=3; New=True");
            connection.Open();
            string querry = "Select * from User Where Username = '" + user + "' and Password = '" + pass + "'";

            var command = new SQLiteCommand(querry, connection);
            var dataset = command.ExecuteReader();

            if (dataset.Read())
            {
                return true;
            }
            connection.Close();
            return false;
        }

        //public static string Loadhighscore()
        //{
        //    SQLiteConnection connection = new SQLiteConnection("Data Source=Fisker.db; Version=3; New=True");
        //    connection.Open();
        //    string querry = "Select * from Highscore";
        //    var command = new SQLiteCommand(querry, connection);
        //    var dataset = command.ExecuteReader();

        //    if (dataset.Read())
        //    {
        //        return true;
        //    }
        //    connection.Close();
        //    return false;
        //}

        //public static bool InsertIntoHighscore(string user, string realm, string score)
        //{
        //    SQLiteConnection connection = new SQLiteConnection("Data Source=Fisker.db; Version=3; New=True");
        //    connection.Open();
        //    var command = new SQLiteCommand("INSERT INTO Highscore (Score, realm, Username) VALUES ('" + score + "','" + realm + "','" + user + "')", connection);
        //    var dataset = command.ExecuteReader();

        //    if (dataset.Read())
        //    {
        //        return true;
        //    }
        //    connection.Close();
        //    return false;
        //}
    }
}