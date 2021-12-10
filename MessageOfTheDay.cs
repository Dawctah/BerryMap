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
            "Eat your green vegetables",
            "Drink plenty of water",
            "Don't forget to stretch",
            "12% water",
            "Message delayed until " + (DateTime.Now.Year + 1),
            ".PlantBerry(OmegaAwesomeBerry);",
            "We stand with Potato Justice!",
            "I see you've escaped the dungeons of Emperor Charizard",
            "Logical",
            "Now available in board-game form",
            "Cool!",
            "Radical!",
            "Written in the stars"
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
