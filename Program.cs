using System;
using System.IO;
using System.Data.SQLite;
using System.Collections.Generic;

namespace CoursSQLite
{
    class Program
    {
        public static void Introduction()
        {  
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("-------- AJOUT DE DONNEES DANS LA BDD SQLITE (TEST) -------");
            Console.WriteLine("-----------------------------------------------------------");
            Space();
        }
        public static string CheminBDD()
        {
            string sqlpath = "D:\\testBDD\\bdd.sqlite";
            return sqlpath;
        }
        public static string AjoutNom()
        {

            string nom = "";

            while (nom.Trim() == "")
            {
                Console.Write("Ajouter votre nom : ");
                nom = Console.ReadLine();
            }
            return nom;
        }
        
        /*public static string DeleteNom()
        {

            string nom = "";

            while (nom.Trim() == "")
            {
                Console.Write("Choissisez le nom à supprimer : ");
                nom = Console.ReadLine();
                if ()
                {

                }
            }
            return nom;
        }*/

        public static string AjoutPrenom()
        {
            string prenom = "";

            while (prenom.Trim() == "")
            {
                Console.Write("Ajouter votre prénom : ");
                prenom = Console.ReadLine();
            }
            return prenom;
        }

        public static int AjoutAge()
        {
            int age = 0;

            while (age == 0)
            {
                
                Console.Write("Ajouter votre age : ");
                try
                {
                    age = int.Parse(Console.ReadLine());
                    
                }
                catch
                {
                    Console.WriteLine("Vous devez écrire un age valide.");
                }
            }
            return age;
        }


        // Création de la base de données
        public static void CreateBDD()
        {
            SQLiteConnection.CreateFile(CheminBDD());
            SQLiteConnection con = new SQLiteConnection("Data Source=D:\\testBDD\\bdd.sqlite;Version=3;");
            con.Open();
            string sqlReq = "create table clients (nom varchar(20), prenom varchar(20), age INTEGER(100))";
            SQLiteCommand command = new SQLiteCommand(sqlReq, con);
            command.ExecuteNonQuery();

            con.Close();
        }

        // Ajout de données dans la bdd
        public static void addData(string nom, string prenom, int age)
        {
            SQLiteConnection con = new SQLiteConnection("Data Source=D:\\testBDD\\bdd.sqlite;Version=3;");
            con.Open();
            string sqlAdd = $"INSERT INTO clients(nom, prenom, age) VALUES('{nom}', '{prenom}', {age})";
            SQLiteCommand command = new SQLiteCommand(sqlAdd, con);
            command.ExecuteNonQuery();

            con.Close();

        }
        
        // Supprime des données dans la bdd
        /* public static void DeleteData(string nom)
        {
            SQLiteConnection con = new SQLiteConnection("Data Source=F:\\testSQlite\\bdd.sqlite;Version=3;");
            con.Open();
            string sqlDel = $"DELETE FROM clients WHERE nom = ('{nom}')";
            SQLiteCommand command = new SQLiteCommand(sqlDel, con);
            command.ExecuteNonQuery();

            con.Close();

        } */

        // Lire les données dans la BDD
        public static void ReadData()
        {
            SQLiteConnection con = new SQLiteConnection("Data Source=D:\\testBDD\\bdd.sqlite;Version=3;");
            con.Open();
            string sqlRead = "SELECT * FROM clients";
            SQLiteCommand command = new SQLiteCommand(sqlRead, con);
            SQLiteDataReader read = command.ExecuteReader();

            while(read.Read())
            {
                Console.WriteLine($"Nom : {read.GetString(0)} ");
                Console.WriteLine($"Prénom : {read.GetString(1)} ");
                Console.WriteLine($"Age : {read.GetInt64(2)} ");
                Console.WriteLine();
            }
            con.Close();
        }

        public static void Space()
        {
            Console.WriteLine();
        }

        public static Dictionary<string, string> Choix()
        {
            Dictionary<string, string> choix = new Dictionary<string, string>
            {
                {"aide", "Affiche les commandes du programmes" },
                {"ajouter", "Ajoute le nom, prénom et age dans la base de données" },
                {"afficher", "Affiche les données de la base de données" },
                /*{"supprimer", "supprimer les données de la base de données" },*/
                {"quitter", "Quitter le programme" }
            };
            Console.WriteLine("Taper 'aide' pour afficher les commandes.");
            return choix;
        }

        static void Main(string[] args)
        {
            Introduction();
            if (!File.Exists(CheminBDD())) CreateBDD();


           Dictionary<string, string> choix =  Choix();

            while (true)
            {
                Console.Write("Terminal> ");
                string com = Console.ReadLine();
                Space();

                if (com.ToLower() == "aide")
                {

                    Console.WriteLine("-----------------------------------------------------------");
                    Console.WriteLine("----------- Liste des commandes du programme --------------");
                    Console.WriteLine("-----------------------------------------------------------");
                    Space();
                    foreach (KeyValuePair<string, string>dic in choix)
                    {
                        Console.WriteLine($"{dic.Key} : {dic.Value}");
                    }
                    Space();
                }

                if (com.ToLower() == "ajouter")
                {
                    // Demande a l'utilisateur d'ajouter un nom et un prenom
                    addData(AjoutNom(), AjoutPrenom(), AjoutAge());
                    Space();
                    Console.WriteLine("Les infos on bien été ajoutées");
                    Space();
                    continue;
                }

                // Cherche une solution pour supprimer dans la BDD
                /*if(com.ToLower() == "supprimer")
                {
                    DeleteData(DeleteNom());
                    Space();
                    continue;
                }*/
                
                if (com.ToLower() == "afficher")
                {
                    Console.WriteLine("-----------------------------------------------------------");
                    Console.WriteLine("--------------- Liste de données dans la BDD --------------");
                    Console.WriteLine("-----------------------------------------------------------");
                    Space();
                    ReadData();
                    Space();
                    continue;
                }
                
                if (com.ToLower() == "quitter")
                {
                    Console.WriteLine("A bientôt ! :-)");
                    Environment.Exit(0);
                }
            }
        }
    }
}
