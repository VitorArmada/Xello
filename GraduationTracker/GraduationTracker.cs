using System;
using System.Linq;

namespace GraduationTracker
{
    public partial class GraduationTracker :IGraduationTracker
    {
        IRepository repository;
        public GraduationTracker(IRepository repository)
        {
            this.repository = repository;
        }

        public Tuple<bool, Standing> HasGraduated(Diploma diploma, Student student)
        {
            var average = 0;
            var countCourses = 0;
            var requirements = diploma.Requirements;

            for(int i = 0; i < requirements.Length; i++)
            {
                var requirement = repository.GetRequirement(requirements[i]);
                average += student.Courses.Where(c => requirement.Courses.Contains(c.Id)).Sum(c => c.Mark);
                countCourses = requirement.Courses.Count();
            }

            return CalculateStanding(average / countCourses);
        }

        public Tuple<bool, Standing> CalculateStanding(int average)
        {
            if (average < 50)
                return new Tuple<bool, Standing>(false, Standing.Remedial);
            else if (average < 80)
                return new Tuple<bool, Standing>(false, Standing.Average);
            else if (average < 95)
                return new Tuple<bool, Standing>(false, Standing.MagnaCumLaude);
            else
                return new Tuple<bool, Standing>(false, Standing.MagnaCumLaude);
        }
    }
}
