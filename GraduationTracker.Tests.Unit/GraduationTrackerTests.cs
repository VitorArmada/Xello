using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Moq;


namespace GraduationTracker.Tests.Unit
{
    [TestClass]
    public class GraduationTrackerTests
    {
        public Diploma AnyDiploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                Requirements = new int[] { 100, 102, 103, 104 }
            };

        public Student[] AnyStudents = new[]
        {
            new Student
            {
                Id = 1,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=95 },
                    new Course{Id = 2, Name = "Science", Mark=95 },
                    new Course{Id = 3, Name = "Literature", Mark=95 },
                    new Course{Id = 4, Name = "Physichal Education", Mark=95 }
                }
            },
            new Student
            {
                Id = 2,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=80 },
                    new Course{Id = 2, Name = "Science", Mark=80 },
                    new Course{Id = 3, Name = "Literature", Mark=80 },
                    new Course{Id = 4, Name = "Physichal Education", Mark=80 }
                }
            },
            new Student
            {
                Id = 3,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=50 },
                    new Course{Id = 2, Name = "Science", Mark=50 },
                    new Course{Id = 3, Name = "Literature", Mark=50 },
                    new Course{Id = 4, Name = "Physichal Education", Mark=50 }
                }
            },
            new Student
            {
                Id = 4,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=40 },
                    new Course{Id = 2, Name = "Science", Mark=40 },
                    new Course{Id = 3, Name = "Literature", Mark=40 },
                    new Course{Id = 4, Name = "Physichal Education", Mark=40 }
                }
            }
        };

        public Mock<IRepository> Setup()
        {
            var repository = new Mock<IRepository>();

            repository.Setup(r => r.GetDiploma(It.IsAny<int>())).Returns(new Diploma());
            repository.Setup(r => r.GetRequirement(100)).Returns(new Requirement { Id = 100, Name = "Math", MinimumMark = 50, Courses = new int[] { 1 }, Credits = 1 });
            repository.Setup(r => r.GetRequirement(102)).Returns(new Requirement { Id = 102, Name = "Science", MinimumMark = 50, Courses = new int[] { 2 }, Credits = 1 });
            repository.Setup(r => r.GetRequirement(103)).Returns(new Requirement { Id = 103, Name = "Literature", MinimumMark = 50, Courses = new int[] { 3 }, Credits = 1 });
            repository.Setup(r => r.GetRequirement(104)).Returns(new Requirement { Id = 104, Name = "Physichal Education", MinimumMark = 50, Courses = new int[] { 4 }, Credits = 1 });
            repository.Setup(r => r.GetStudent(It.IsAny<int>())).Returns(new Student());
            return repository;
        }

        [TestMethod]
        public void AtleastOneStudentGraduated_ReturnsTrue()
        {
            var repository = Setup();
            var tracker = new GraduationTracker(repository.Object);

            var graduated = new List<Tuple<bool, Standing>>();

            foreach(var student in AnyStudents)
            {
                graduated.Add(tracker.HasGraduated(AnyDiploma, student));      
            }
           
            Assert.IsTrue(graduated.Any());
        }

        [TestMethod]
        public void StudentHasOneCourse_ReturnsTrue()
        {
            var repository = Setup();
            var tracker = new GraduationTracker(repository.Object);
            var diploma = AnyDiploma;
            var student = AnyStudents[2];

            Assert.AreEqual(new Tuple<bool, Standing>(false, Standing.MagnaCumLaude), tracker.HasGraduated(diploma, student));
        }

        [TestMethod]
        public void CalculateStanding_ReturnsCorrectValue()
        {
            var repository = Setup();
            var tracker = new GraduationTracker(repository.Object);

            Assert.AreEqual(new Tuple<bool, Standing>(false, Standing.Remedial), tracker.CalculateStanding(49));
        }
    }

    




}
