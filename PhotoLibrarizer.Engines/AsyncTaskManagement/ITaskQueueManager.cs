namespace PhotoLibrarizer.Engines.AsyncTaskManagement
{
    public interface ITaskQueueManager
    {
        Task ProcessQueueWithDynamicParallelism();
        Task ProcessQueueInBatchedParallelism();
        Task EnqueueAndRunTask(Func<Task> taskFunc);
        Task ProcessQueueSequentially();
        void AddTask(Func<Task> taskFunc);
    }
}