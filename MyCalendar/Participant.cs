using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalendar
{
    public class Participant
    {
        public int Participant_Id { get; }
        public string Participant_Name { get; }
        public string Participant_Password { get; }

        public Participant (string Participant_Id, string Participant_Name, string Participant_Password)
        {
            this.Participant_Id = Convert.ToInt32(Participant_Id);
            this.Participant_Name = Participant_Name;
            this.Participant_Password = Participant_Password;
        }
    }
}
