using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WCFDolgozat.Controllers
{
    public class ParticipantsController
    {
        string[] adatok = File.ReadAllLines("C:\\Users\\haluskad\\Downloads\\Participants.txt").Skip(1).ToArray();
        List<Participants> participants = new List<Participants>();

        public List<Participants> GetParticipants()
        { 
            foreach (var item in adatok)
            {
                var sor = item.Split(';');
                participants.Add(new Participants
                {
                    Id = int.Parse(sor[0]),
                    Name = sor[1],
                    Age = int.Parse(sor[2]),
                    AverageScore = double.Parse(sor[3])
                });
            }
            return participants;


        }

       


    }
}