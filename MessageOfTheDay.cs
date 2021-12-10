using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryMap
{
    public static class MessageOfTheDay
    {
        private static List<string> messages = new List<string>()
        {
            "Snom :3",
            "Drink plenty of water",
            "Don't forget to stretch",
            "12% water",
            "Message delayed until " + (DateTime.Now.Year + 1),
            ".PlantBerry(OmegaAwesomeBerry);",
            "I see you've escaped the dungeons of Emperor Charizard",
            "Logical",
            "Now available in board-game form",
            "Cool!",
            "Radical!",
            "Written in the stars",
            "Inspired!",
            "text text text text text text",
            "Dairy Free",
            "SEAN!",
            "Berry berry = new Berry();",
            "Boogey Wonderland",
            "Moon big",
            "Shop at Sahara",
            "I write these words in code, for anything not written in pixels cannot be trusted.",
            "Help, they have me locked in a basement and are forcing me to code for them, this is my only way to contact the outside world, if you see this please help me!",
            "Fox only",
            "2.0",
            "As the prophecy has foretold...",
            "Music by [REDACTED]",
            "Mudkips?",
            "Furret wuz here",
            "he walk",
            "#Dexit",
            "I made this one with my tears",
            "Pokemon Let's Go Home & Sleep",
            "Gigantimax Wailord",
            "Execute Order 34",
            "I have crippling depression",
            "7.8/10",
            "Cynthia",
            "Gen 5 OST best OST, don't @ me",
            "Alolan Exeggutor",
            "Best Selling"
        };

        public static string Message
        {
            get
            {
                Random random = new Random();
                return messages[random.Next(messages.Count)];
            }
        }
    }
}
