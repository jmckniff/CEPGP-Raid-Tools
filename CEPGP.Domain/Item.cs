using System.Text.RegularExpressions;

namespace CEPGP.Domain
{
    public class Item
    {
        public int Id { get; }

        public Item(int id)
        {
            Id = id;
        }

        public static Item Parse(string itemString)
        {
            if (string.IsNullOrEmpty(itemString))
            {
                return null;
            }

            var regex = new Regex(@"\|Hitem:(\d+):");

            var match = regex.Match(itemString);

            if (!match.Success)
            {
                return null;
            }

            var itemIdString = match.Groups[1].Value;

            var itemId = 0;

            if (int.TryParse(itemIdString, out itemId))
            {
                return new Item(itemId);
            }

            return null;
        }
    }
}
