using System;
using System.Collections.Generic;

namespace Area51Elevator {
    public class Elevator {
        public Floor CurrentFloor { get; private set; }
        private Queue<Floor> QueuedFloors = new Queue<Floor> ();
        private Dictionary<Agent, Floor> Agents = new Dictionary<Agent, Floor>();
        public Elevator (Floor startingFloor) {
            this.CurrentFloor = startingFloor;
        }

        public void EnqueueFloor (Floor floor) {
            lock ( this ) {
                // if the floor is already queued, return
                if ( QueuedFloors.Contains(floor) ) return;

                // get all floors between the current one and the one being queued
                Queue<Floor> between = Area51.GetFloorsInbetween(CurrentFloor, floor);
                
                foreach ( Floor queuedFloor in between) {
                    QueuedFloors.Enqueue(queuedFloor);
                }
            }
        }

        public void Enter(Agent agent, Floor floor) {
            lock ( Agents ) { 
                if ( Agents.ContainsKey(agent) ) return;
                Agents.Add(agent, floor);
                EnqueueFloor(floor);
                Console.WriteLine($"{agent.Name} has gotten on the elevator at Floor {CurrentFloor.Name}, heading for Floor {floor.Name}");
            }
        }

        public void MoveToNextFloor () {
            lock (this)
            {
                if ( QueuedFloors.Count == 0 ) return;
                CurrentFloor = QueuedFloors.Dequeue();
                Console.WriteLine("Elevator moved to Floor " + CurrentFloor.Name);

                var AgentKeys = new List<Agent>(Agents.Keys);
                foreach ( Agent key in AgentKeys ) {
                    if ( Agents.ContainsKey(key) && Agents[key] == CurrentFloor ) {
                        Agents.Remove(key);
                        key.CurrentFloor = CurrentFloor;
                        // Console.WriteLine($"{key.Name} has gotten off the elevator at Floor {Agents[key].Name}.");
                    }
                }
            }
        }
    }
}