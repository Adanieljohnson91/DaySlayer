namespace DaySlayer.Models
{
    public class Ritual
    {
        /*
         * id
         * userId
         * name
         * weight
         * isActive
         */
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public bool IsActive { get; set; }
    }
}