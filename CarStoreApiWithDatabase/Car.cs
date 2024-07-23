namespace CarStoreApi
{
    public class Car
    {

        public Car( int Id , string Name, int Model)
        {
            this.Name = Name;
            this.Model = Model;
        }
        public Car(string Name , int Model) { 
            this.Name = Name;
            this.Model = Model;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Model { get; set; }

        public List<User> Users { get; set; } = [];
    }
}
