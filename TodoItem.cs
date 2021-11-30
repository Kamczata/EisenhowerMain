using System;

namespace EisenhowerCore {
public class TodoItem
    {
        private string Title {  get; set; }
        private DateTime Deadline {  get; set; }
        private bool IsDone { get; set; }

        public TodoItem(string title, DateTime deadline)
        {
            Title = title;
            Deadline = deadline;
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

            if (this.IsDone)
            {
                return $"[x] {this.Deadline.Day}-{this.Deadline.Month} {this.Title}";
            }
            else
            {
                return $"[ ] {this.Deadline.Day}-{this.Deadline.Month} {this.Title}";
            }
        }




    }

}