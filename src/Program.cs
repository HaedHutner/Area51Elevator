using System;
using System.Collections.Generic;
using System.Threading;

namespace Area51Elevator
{
    class Program
    {

        const int MAX_AGENTS = 10;

        private static Random Random = new Random();
        private static Dictionary<Agent, Thread> Agents = new Dictionary<Agent, Thread>();
        private static Elevator Elevator = new Elevator();
        private static Floor[] Floors = { Floor.G, Floor.S, Floor.T1, Floor.T2 };

        static void Main(string[] args)
        {
            for ( int i = 0; i < MAX_AGENTS; i++ ) {
                Agent agent = new Agent("Agent 00" + i, GetRandomSecurityLevel(), GetRandomFloor());
                Thread agentThread = new Thread(() => {
                    while (true) {
                        if ( Elevator.CurrentFloor == agent.CurrentFloor ) {
                            Floor floor = GetRandomFloor();
                            Elevator.EnqueueFloor(floor);
                            Console.WriteLine($"{agent.Name} has entered the elevator and has requested it goes to Floor {floor.Name}.");
                        } else {
                            Elevator.EnqueueFloor(agent.CurrentFloor);
                            Console.WriteLine($"{agent.Name} has requested the elevator arrive at their current floor ( Floor {agent.CurrentFloor.Name} ).");
                        }
                    }
                });

                agentThread.Start();

                Agents.Add(agent, agentThread);
            }

            while (true) {

            }
        }

        static Agent.SecurityLevel GetRandomSecurityLevel() {
            Array values = Enum.GetValues(typeof(Agent.SecurityLevel));
            return (Agent.SecurityLevel) values.GetValue(Random.Next(values.Length));
        }

        static Floor GetRandomFloor() {
            return Floors[Random.Next(Floors.Length - 1)];
        }
    }
}
