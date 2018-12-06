using System;

namespace Area51Elevator {
    public class Agent {
        public enum SecurityLevel { Confidential = 0, Secret = 1, TopSecret = 2 }

        public string Name { get; private set; }

        public SecurityLevel SecurityClearance { get; private set; }

        public Floor CurrentFloor { get; set; }

        public bool InElevator { get; set; }

        public Agent (string name, SecurityLevel level, Floor beginningFloor) {
            this.Name = name;
            this.SecurityClearance = level;
            this.CurrentFloor = beginningFloor;
        }

        public void RequestFloor(Elevator elevator) {
            if (elevator.CurrentFloor == CurrentFloor) {
                Floor requestedFloor = Area51.GetRandomFloor();
                elevator.Enter(this, Area51.GetRandomFloor());
                InElevator = true;
            } else {
                elevator.EnqueueFloor (CurrentFloor);
                Log.Info ($"{this} has requested the elevator go to their current floor ( {CurrentFloor} ).");
            }
        }

        public override string ToString() {
            return $"{Name} ({SecurityClearance})";
        }
    }
}