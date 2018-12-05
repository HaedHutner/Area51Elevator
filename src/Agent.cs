using System;

namespace Area51Elevator {
    public class Agent {
        public enum SecurityLevel { Confidential = 0, Secret = 1, TopSecret = 2 }

        public string Name { get; private set; }
        public SecurityLevel SecurityClearance { get; private set; }
        public Floor CurrentFloor { get; set; }

        public Agent (string name, SecurityLevel level, Floor beginningFloor) {
            this.Name = name;
            this.SecurityClearance = level;
            this.CurrentFloor = beginningFloor;
        }

        public void RequestFloor(Elevator elevator) {
            if (elevator.CurrentFloor == CurrentFloor) {
                Floor requestedFloor = Area51.GetRandomFloor();
                if ( requestedFloor.MinimumSecurityClearance > this.SecurityClearance) {
                    Console.WriteLine ($"{Name} ( {SecurityClearance} ) does not have permission to go to Floor {requestedFloor.Name} ( {requestedFloor.MinimumSecurityClearance} )");
                } else {
                    elevator.Enter(this, Area51.GetRandomFloor());
                }
            } else {
                elevator.EnqueueFloor (CurrentFloor);
                Console.WriteLine ($"{Name} has requested the elevator go to their current floor ( Floor {CurrentFloor.Name} ).");
            }
        }
    }
}