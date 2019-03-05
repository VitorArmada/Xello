using System;

namespace GraduationTracker
{
    public interface IGraduationTracker
    {
        Tuple<bool, Standing> HasGraduated(Diploma diploma, Student student);
        Tuple<bool, Standing> CalculateStanding(int average);
    }
}
