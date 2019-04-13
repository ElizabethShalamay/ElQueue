namespace ElQueue.DAL.Enums
{
    public enum QueueStatus
    {
        FreeTimeSlot,
        TakingTurn,
        Queueing,
        Waiting,
        Serving,
        Evaluation
    }
}
