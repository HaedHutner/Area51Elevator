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

                if ( floor.MinimumSecurityClearance > agent.SecurityClearance) {
                    Log.Info ($"{agent} does not have permission to go to {floor}");
                    return;
                }
                
                Agents.Add(agent, floor);
                EnqueueFloor(floor);
                Log.Info ($"{agent} has gotten on the elevator at {CurrentFloor}, heading for {floor}");
            }
        }

        public void MoveToNextFloor () {
            lock (this)
            {
                if ( QueuedFloors.Count == 0 ) return;
                CurrentFloor = QueuedFloors.Dequeue();
               Log.Info ($"Elevator moved to {CurrentFloor}");

                var AgentKeys = new List<Agent>(Agents.Keys);
                foreach ( Agent key in AgentKeys ) {
                    if ( Agents.ContainsKey(key) && Agents[key] == CurrentFloor ) {
                        Agents.Remove(key);
                        key.InElevator = false;
                        key.CurrentFloor = CurrentFloor;
                        Log.Info ($"{key} has gotten off the elevator at {CurrentFloor}.");
                    }
                }
            }
        }
    }
}