using System;
using System.Collections.Generic;
using TestStack.White;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems;
using TestStack.White.InputDevices;
using TestStack.White.UIItems.TreeItems;
using TestStack.White.UIItems.Finders;
using System.Windows.Automation;
using TestStack.White.WindowsAPI;

namespace addressbook_tests_white
{
    public class GroupHelper : HelperBase
    {
        public static string GROUPWINTITLE = "Group editor";
        public static string DELETEGROUPTITLE = "Delete group";

        public GroupHelper(ApplicationManager manager) : base(manager) { }
        
        public List<GroupData> GetGroupList()
        {
            List<GroupData> list = new List<GroupData>();
            Window dialogue = OpenGroupsDialogue();
            Tree tree = dialogue.Get<Tree>("uxAddressTreeView");
            TreeNode root = tree.Nodes[0];

            foreach (TreeNode item in root.Nodes)
            {
                list.Add(new GroupData()
                {
                    Name = item.Text
                });
            }                 
            CloseGroupsDialogue(dialogue);
            return list;
        }

        public void Add(GroupData newGroup)
        {
            Window dialogue = OpenGroupsDialogue();
            dialogue.Get<Button>("uxNewAddressButton").Click();
            TextBox textbox = (TextBox) dialogue.Get(SearchCriteria.ByControlType(ControlType.Edit));
            textbox.Enter(newGroup.Name);
            //Эмулировать нажатие клавиши ентер
            Keyboard.Instance.PressSpecialKey(KeyboardInput.SpecialKeys.RETURN);            
            CloseGroupsDialogue(dialogue);
        }

        private void CloseGroupsDialogue(Window dialogue)
        {
            dialogue.Get<Button>("uxCloseAddressButton").Click();
        }

        private Window OpenGroupsDialogue()
        {
            manager.MainWindow.Get<Button>("groupButton").Click();
            return manager.MainWindow.ModalWindow(GROUPWINTITLE);
        }

        public void DeleteGroup(int i)
        {
            Window dialogue = OpenGroupsDialogue();       
            Tree tree = dialogue.Get<Tree>("uxAddressTreeView");
            TreeNode root = tree.Nodes[0];
            TreeNodes node = root.Nodes;
            node[i].Select();
            dialogue.Get<Button>("uxDeleteAddressButton").Click();
            Window confirm = dialogue.ModalWindow(DELETEGROUPTITLE);
            confirm.Get<Button>("uxOKAddressButton").Click();
            CloseGroupsDialogue(dialogue);
        }

        public int NumberOfGroups()
        {
            Window dialogue = OpenGroupsDialogue();
            Tree tree = dialogue.Get<Tree>("uxAddressTreeView");
            int numberOfGroups = tree.Nodes[0].Nodes.Count;
            CloseGroupsDialogue(dialogue);
            return numberOfGroups;
        }
    }    
}