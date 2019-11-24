using Microsoft.VisualStudio.TestTools.UnitTesting;
using College.Entities.ActivityContext.Entities;
using System;
using System.Linq;

namespace College.Entities.Tests.ActivityContext.Entities
{
    [TestClass]
    public class ActivityTest
    {
        Activity activity;
        Discipline discipline;
        [TestInitialize]
        public void Init()
        {
            discipline = new Discipline(Guid.NewGuid(), "LTP9");
            activity = new Activity(discipline, "Leia", DateTime.Now, (decimal)12.2, (decimal)87.8, null);
        }
        [TestMethod]
        public void InstanceActivityWithValuesCorrect()
        {
            Assert.IsTrue(activity.IsValid());
        }

        [TestMethod]
        public void InstanceActivityWithValuesInCorrect()
        {
            activity = new Activity(discipline, "Leia", DateTime.Now, (decimal)12.2, (decimal)87.9, null);
            Assert.IsFalse(activity.IsValid());
            Assert.IsTrue(activity.Notifications.Any(x => x.Key == "Grade" && x.Value != null));
        }

        [TestCleanup]
        public void Clean()
        {
        }
    }
}
