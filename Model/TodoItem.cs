using System;

namespace EisenhowerCore {
public class TodoItem
    {
        public string Title {  get; private set; }
        public DateTime Deadline {  get; private set; }
        public bool IsDone { get; private set; }

        public TodoItem(string title, DateTime deadline)
        {
            Title = title;
            Deadline = deadline;
            IsDone = false;
        }

        public string GetTitle()
        {
            return this.Title;
        }

        public DateTime GetDeadline()
        {
            return this.Deadline;
        }

        public void Mark()
        {
            IsDone = true;
        }

        public void UnMark()
        {
            IsDone = false;
        }

        public override string ToString()
        {
            // [x] 12-6 submit assignment
            // [ ] 28-6 submit assignment
            string dateAndTitle = $"{this.Deadline.Day}-{this.Deadline.Month} {this.Title}";
            return ((this.IsDone ? "[x]" : "[ ]") + $" {dateAndTitle}");
        }

        public int GetTitleLength() => Title.Length;


    }

}