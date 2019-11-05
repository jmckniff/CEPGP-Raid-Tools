using System;

namespace CEPGP.Domain
{
    public class Attendance
    {
        public Member Member { get; }
        public Period Period { get; }
        public int Total { get; }
        public int Attended { get; }
        public double AttendancePercent { get; }

        public Attendance(Member member, Period period, int total, int attended)
        {
            Member = member;
            Period = period;
            Total = total;
            Attended = attended;

            var attendancePercent = ((double)attended / total) * 100;

            AttendancePercent = Math.Round(attendancePercent, 0, MidpointRounding.AwayFromZero);
        }
    }
}
