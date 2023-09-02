using Store.Client.Models;

namespace Store.Client
{
    public static class ThingClient
    {
        private static List<Thing> Things = new () 
        {   
            new Thing()
            {
                Id = 1,
                Name = "Pollo",
                Description = "Emplumado animal que a veces vuela y a veces no..."
            },
            new Thing()
            {
                Id = 2,
                Name = "Pensamiento",
                Description = "Por si pensaste que lo de cosa era abstracto, soprendete maifren"
            }

        }; 
        public static Thing[] GetThings()
        {
            return Things.ToArray();
        }
        public static void AddThing(Thing thing)
        {
            Things.Add(thing);
        }

        public static Thing GetThing(int id)
        {
            return Things.Find(Th => Th.Id == id ) ?? throw new Exception("Could not find the thing you are asking for");
        }
        public static void UpdateThing(Thing UpdatedThing)
        {
            Thing prevThing = GetThing(UpdatedThing.Id);
            prevThing.Name = UpdatedThing.Name;
            prevThing.Description = UpdatedThing.Description;
        }
        public static void DeleteThing(int Id)
        {
            Thing Th = GetThing(Id);
            Things.Remove(Th);
        }
    }
}