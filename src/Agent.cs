namespace Area51Elevator {
    public class Agent {
        public enum SecurityLevel { Confidential, Secret, TopSecret }

        public string Name { get; private set; }
        public SecurityLevel SecurityClearance { get; private set; }
        public Floor CurrentFloor { get; set; }

        public Agent (string name, SecurityLevel level, Floor beginningFloor) {
            this.Name = Name;
            this.SecurityClearance = level;
            this.CurrentFloor = beginningFloor;
        }
    }
}