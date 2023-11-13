namespace PhotoLibrarizer.Engines.AsyncTaskManagement;

public class TaskQueueManager : ITaskQueueManager
{
    private readonly SemaphoreSlim semaphore;
    private readonly Queue<Func<Task>> taskQueue;

    public TaskQueueManager(int maxConcurrentTasks)
    {
        semaphore = new SemaphoreSlim(maxConcurrentTasks);
        taskQueue = new Queue<Func<Task>>();
    }
    
    public async Task ProcessQueueWithDynamicParallelism()
    {
        var maxParallelTasks = 3; // Maximum number of tasks to run in parallel
        var semaphore = new SemaphoreSlim(maxParallelTasks);

        var activeTasks = new List<Task>();

        while (taskQueue.Count > 0 || activeTasks.Count > 0)
        {
            // Start new tasks up to the maximum parallelism
            while (taskQueue.Count > 0 && activeTasks.Count < maxParallelTasks)
            {
                await semaphore.WaitAsync(); // Wait until a slot is available

                var taskFunc = taskQueue.Dequeue();
                var task = EnqueueAndRunTask(taskFunc)
                    .ContinueWith(_ => semaphore.Release()); // Release the slot when the task is completed

                activeTasks.Add(task);
            }

            // Wait for any of the active tasks to complete
            var completedTask = await Task.WhenAny(activeTasks);

            // Remove the completed task from the active tasks list
            activeTasks.Remove(completedTask);
        }
    }
    
    public async Task ProcessQueueInBatchedParallelism()
    {
        var requiredParallelTasks = 3; // Minimum number of tasks to run in parallel
        var semaphore = new SemaphoreSlim(requiredParallelTasks);

        while (taskQueue.Count > 0)
        {
            var tasksToRun = Math.Min(taskQueue.Count, requiredParallelTasks);

            var tasks = new List<Task>();
            for (int i = 0; i < tasksToRun; i++)
            {
                await semaphore.WaitAsync(); // Wait until a slot is available

                var taskFunc = taskQueue.Dequeue();
                tasks.Add(EnqueueAndRunTask(taskFunc)
                    .ContinueWith(_ => semaphore.Release())); // Release the slot when the task is completed
            }

            await Task.WhenAll(tasks); // Wait for all the tasks in this batch to complete
        }
    }

    public async Task EnqueueAndRunTask(Func<Task> taskFunc)
    {
        await semaphore.WaitAsync(); // Wait until a slot is available
        try
        {
            await taskFunc();
        }
        finally
        {
            semaphore.Release(); // Release the slot when the task is completed
        }
    }

    public async Task ProcessQueueSequentially()
    {
        while (taskQueue.Count > 0)
        {
            var taskFunc = taskQueue.Dequeue();
            await EnqueueAndRunTask(taskFunc);
        }
    }

    public void AddTask(Func<Task> taskFunc)
    {
        taskQueue.Enqueue(taskFunc);
    }
}