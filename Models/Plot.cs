namespace BerryMap.Models
{
    [System.Serializable]
    public class Plot
    {
        private Location location;

        public Berry[] Berries { get; private set; }

        public Location Location => location;

        public int Size => Berries.Length;

        public Plot(int size, Location location)
        {
            Berries = new Berry[size];

            this.location = location;
        }

        public override string ToString()
        {
            string result = $"{Location.Name}:\t";
            if (!Location.Name.Contains("Fuego"))
                result += "\t";

            for (int k = 0; k < Berries.Length; k++)
            {
                if (Berries[k] != null)
                    result += Berries[k].ToString();
                else
                    result += "Empty";

                if (k < Berries.Length - 1)
                    result += ", ";
            }

            return result;
        }
    }
}
