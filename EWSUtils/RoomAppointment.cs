using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EWSUtils
{
    [Serializable]
    public class RoomAppointment
    {
        public string Subject { get; set; }

        public String Start { get; set; }
        public string StartTime { get; set; }

        public String End { get; set; }
        public string EndTime { get; set; }
        
        public string Room_Alias { get; set; }
        public string Organizer { get; set; }
        public String Location { get; set; }

        public DateTime StartTimeForSorting { get; set; }
        public DateTime EndTimeForSorting { get; set; }


        public RoomAppointment()
        {
        }
        public RoomAppointment(string aSubject, String aStartTime, String aEndTime, string aRoomAlias, string aOrganizer, DateTime aStartTimeForSorting, DateTime aEndTimeForSorting)
        {
            Subject = aSubject;
            Start = aStartTime;
            End = aEndTime;
            Room_Alias = aRoomAlias;
            Organizer = aOrganizer;
            StartTimeForSorting = aStartTimeForSorting;
            EndTimeForSorting = aEndTimeForSorting;
        }
    }
}
