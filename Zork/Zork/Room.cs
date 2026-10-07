namespace Zork
{
    public class Room
    {
        public string name { get; }

        public string description { get; set; }

        public Room(string name, string description = "")
        {
            this.name = name;
            this.description = description;
        }

        public override string ToString() => name;
    }
}
