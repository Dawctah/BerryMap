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
            ":O",
            "Message delayed until " + (DateTime.Now.Year + 1),
            "Is that a JoJo reference!?",
            "Now available in board-game form",
            "Cool!",
            "Radical!",
            "Written in the stars",
            "Inspired!",
            "text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text ",
            "Dairy Free",
            "SEAN!",
            "Berry berry = new Berry();",
            "Boogey Wonderland",
            "Moon big",
            "Shop at Sahara",
            "Fox only",
            "2.0",
            "As the prophecy has foretold...",
            "Music by [REDACTED]",
            "Mudkipz",
            "Furret wuz here",
            "Hey, you know my super-cool Rattata? My Rattata is different from regular Rattata. It’s like my Rattata is in the top percentage of all Rattata, you know what I’m saying?",
            "he walk",
            "#Dexit",
            "Pokemon Let's Go Home & Sleep",
            "Gigantimax Wailord",
            "7.8/10",
            "Cynthia",
            "Gen 5 music hits hard",
            "Alolan Exeggutor",
            "Best Selling",
            "Er, what was his name again?",
            "Hello, and welcome to the world of Berry Farming!",
            "Praise Helix",
            "Turning Frying Pans into Drying Pans since 2021",
            "Brock Obama",
            "Not gonna Raichu a love song",
            "Press A to pound",
            "Are you a boy or a girl?",
            "Gary was here, Ash is a loser",
            "Diglett's lower body",
            "A wild " + APIController.GetPokemonName() + " appeared!",
            "Super effective!",
            "Extra spicy",
            "We're no strangers to love",
            "WOOP!",
            "Poketuber Reacts to Jaiden's First Nuzlocke",
            "Poketuber Reacts to Jaiden's Second Nuzlocke"
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
