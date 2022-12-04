// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;

/* This program is a guestbook where you can read, create and delete posts.
 * 
 * The program is made by Sofia Widholm
 * Moment 3, Programmering i C#
 * Webbutvecklingsprogrammet, Mittuniversitetet
 * Last update: 2022-12-04
*/

namespace posts 
{ 
    // Class Guestbook
    public class Guestbook
    {
        // Fields
        private string jsonFile = Directory.GetCurrentDirectory().ToString() + "guestbook.json";
        private List<Post> posts = new List<Post>();

        // Constructor
        public Guestbook() 
        {
            if (File.Exists(Directory.GetCurrentDirectory().ToString() + "guestbook.json")==true)
            {
                // Read data from JSON file and save it to a varaible
                var jsonString = File.ReadAllText(jsonFile);
                // Convert data from JSON to an array of objects
                posts = JsonSerializer.Deserialize<List<Post>>(jsonString);
            }
        }

        // Method to add post
        public Post addPost (Post post)
        {
            // Adds the object to the array of posts
            posts.Add(post);
            // Write all posts to JSON file
            writeToFile();

            return post;
        }

        // Method that delete a certain post
        public int deletePost (int index)
        {
            // Deletes the post with the specific index
            posts.RemoveAt(index);
            // Write all remaining posts to JSON file
            writeToFile();

            return index;
            
        }

        // Method that return an array of all posts
        public List<Post> getPosts()
        {
            return posts;
        }

        // Method that writes posts to JSON file
        private void writeToFile()
        {
            // Converts array of objects to JSON
            var jsonString = JsonSerializer.Serialize(posts);
            // Write data to JSON file
            File.WriteAllText(jsonFile, jsonString);
        }
    }

    // Class Post
    public class Post
    {
        // Fields
        private string author;
        private string message;

        // Set and get methods
        public string Author {
            set { this.author = value; }
            get { return author; }
            
        }

        public string Message
        {
            set { this.message = value; }
            get { return message; }
            
        }
    }


    class Program
{
        static void Main(string[] args)
        {
            // New instance of Guestbook
            Guestbook guestbook = new Guestbook();
            // Iteration variable
            int i = 0;

            // While loop that runs as long the conditional is set to true
            while (true)
            {
                // Clearing of console
                Console.Clear();
                // Cursor non-visible
                Console.CursorVisible = false;
                // Printing of menu
                Console.WriteLine("SOFIAS GÄSTBOK\n\n");
                Console.WriteLine("MENYVAL");
                Console.WriteLine("1. Skriv i gästboken");
                Console.WriteLine("2. Ta bort inlägg\n");
                Console.WriteLine("3. Stäng gästboken\n");

                // Setting the iteration variable to zero
                i = 0;

                // Foreach loop that prints all stored posts 
                foreach(Post post in guestbook.getPosts())
                {
                    // Printing of index and name of author
                    Console.WriteLine("[" + i++ + "]" + " " + "Namn: " + post.Author);
                    // Printing of message
                    Console.WriteLine(post.Message + "\n");
                }
            
            // Save option input for menu to a variable
            int option = (int) Console.ReadKey(true).Key;

            // Switch that runs different code based on value of variable option
            switch (option)
            {
                case '1':
                    // Visible cursor
                    Console.CursorVisible = true;
                    // New instance of object Post
                    Post obj = new Post();

                    // Printing of text for input
                    Console.Write("Ditt namn: ");
                    // Saving of input to variable
                    string author = Console.ReadLine();

                    while (String.IsNullOrEmpty(author))
                    {
                        // Printing of error message
                        Console.WriteLine("Du har glömt att fylla i ett namn.");
                        // Printing of text for input
                        Console.Write("Ditt namn: ");
                        // Saving of input to variable
                        author = Console.ReadLine();
                    }

                    // Saves varible to object
                    obj.Author = author;
                    // Printing of text for input
                    Console.Write("Meddelande: ");
                    // Savíng of input to variable
                    string message = Console.ReadLine();

                    while (String.IsNullOrEmpty(message))
                    {
                        // Printing of error message
                        Console.WriteLine("Du har glömt att skriva ett meddelande.");
                        // Printing of text for input
                        Console.Write("Meddelande: ");
                        // Savíng of input to variable
                        message = Console.ReadLine();
                    }

                    // Saves varible to object
                    obj.Message = message;

                    // If statement that checks that variable isn't empty
                    if (!String.IsNullOrEmpty(author) && !String.IsNullOrEmpty(message))
                    {
                        // Saves post to JSON file
                        guestbook.addPost(obj);
                    }
                    
                    break;
                case '2':
                    // Visible cursor
                    Console.CursorVisible = true;
                    // Printing of text for input
                    Console.Write("Ange nummer på inlägg som ska raderas: ");
                    // Saving input to variable
                    string postIndex = Console.ReadLine();
                    // Converting variable to integer and call of method to delete post
                    guestbook.deletePost(Convert.ToInt32(postIndex));
                    break;
                case '3':
                    // Quit program
                    Environment.Exit(0);
                    break;
            }
            }
        }
}
}