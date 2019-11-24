using College.Entities.EvaluationContext.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace College.Entities.Tests.EvaluationContext.Entities
{
    [TestClass]
    public class ActivityTest
    {
        Activity activity;
        [TestInitialize]
        public void Init()
        {
            activity = new Activity(Guid.NewGuid(), Guid.NewGuid(), (decimal)2.2, (decimal)10.0);
        }
        [TestMethod]
        public void InstanceActivityWithValuesCorrect()
        {
            var asdf = activity.IsValid();
            Assert.IsTrue(activity.IsValid());
        }

        [TestMethod]
        public void InstanceActivityWithValuesInCorrect()
        {
            activity = new Activity(Guid.NewGuid(), Guid.NewGuid(), (decimal)10.2, (decimal)10.0);
            Assert.IsFalse(activity.IsValid());
            Assert.IsTrue(activity.Notifications.Any(x => x.Key == "Grade" && x.Value != null));
        }

        [TestCleanup]
        public void Clean()
        {
            _ = activity;
        }
    }
}
