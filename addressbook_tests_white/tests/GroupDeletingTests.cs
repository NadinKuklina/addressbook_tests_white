using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_tests_white
{
    [TestFixture]
    public class GroupDeletingTests : TestBase
    {
        [Test]
        public void TestDeleteGroup()
        {
            int i = 1;
            if (app.Groups.NumberOfGroups() <= i)
            {
                for (int j = 0; j < i; j++)
                {
                    GroupData newGroup = new GroupData()
                    {
                        Name = "newGroup" + j.ToString()
                    };
                    app.Groups.Add(newGroup);
                }               
            }

            List<GroupData> oldGroups = app.Groups.GetGroupList();           

            app.Groups.DeleteGroup(i);

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.RemoveAt(i);
            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
