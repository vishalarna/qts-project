namespace QTD2.Domain.Entities.Core
{
    public class Task_Position : Common.Entity
    {
        public int TaskId { get; set; }

        public int PositionId { get; set; }

        public virtual Task Task { get; set; }

        public virtual Position Position { get; set; }

        public Task_Position()
        {
        }

        public Task_Position(Task task, Position position)
        {
            TaskId = task.Id;
            PositionId = position.Id;
            Task = task;
            Position = position;
        }
    }
}
