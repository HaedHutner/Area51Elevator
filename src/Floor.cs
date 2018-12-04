using static Area51Elevator.Agent;

namespace Area51Elevator
{
    public class Floor
    {
        public static Floor G = new Floor("G", SecurityLevel.Confidential);
        public static Floor S = new Floor("F", SecurityLevel.Secret);
        public static Floor T1 = new Floor("T1", SecurityLevel.TopSecret);
        public static Floor T2 = new Floor("T2", SecurityLevel.TopSecret);

        public string Name { get; private set; }
        public SecurityLevel MinimumSecurityClearance { get; private set; }

        public Floor(string name, SecurityLevel requiredSecurityClearance) {
            this.Name = name;
            this.MinimumSecurityClearance = requiredSecurityClearance;
        }
    }
}