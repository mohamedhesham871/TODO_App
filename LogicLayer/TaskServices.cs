using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using LogicLayer.Dtos;
using LogicLayer.Enums;
using PresentaionLayer.Dtos;

namespace LogicLayer
{
    public class TaskServices(ITaskRepository repository) : ITaskServices
    {
        private readonly ITaskRepository _repository = repository;

        public async Task<TaskDetailsOrCreateDto> AddTaskAsync(TaskDetailsOrCreateDto task)
        {
            if(task == null)
            {
                throw new ArgumentNullException(nameof(task), "Task cannot be null"); // Fix Error Later
            }
            //convert from TaskDetailsOrCreateDto to TaskTable
            var taskTable = new DataAccessLayer.Models.TaskTable
            {
                Title = task.Title,
                Description = task.Description,
                Status = task.Status.ToString(),
                Priority = task.Priority.ToString(),
                DueDate = task.DueDate,
                CreatedDate = DateTime.UtcNow,
                LastModifiedDate = DateTime.UtcNow
            };
            await _repository.AddTaskAsync(taskTable);
            //return the created task as TaskDetailsOrCreateDto
            return (task);
        }

        public  async Task<bool> DeleteTaskAsync(int id)
        {
           if(id <= 0)
            {
                throw new ArgumentException("Invalid task ID", nameof(id));
            }
            // Ensure the task exists before attempting to delete
            var task =  _repository.GetTaskByIdAsync(id);
            if (task == null)
            {
                throw new KeyNotFoundException($"Task with ID {id} not found.");
            }
            // Proceed with deletion
           var res= await _repository.DeleteTaskAsync(task.Result);
            return res;
        }

        public async Task<List<TaskDto>> GetAllTasksAsync()
        {
            var  tasks =await _repository.GetAllTasksAsync();
            //convert from TaskTable to TaskDetailsOrCreateDto
            return tasks.Select(task => new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = Enum.Parse<Status>(task.Status), //convert From string to Enum
                Priority = Enum.Parse<Priority>(task.Priority),// convert From string to Enum
                DueDate = task.DueDate
            }).ToList();


        }

        public async Task<TaskDto> GetTaskByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid task ID", nameof(id));
            }
            var result = await _repository.GetTaskByIdAsync(id);
            if(result == null)
            {
                throw new KeyNotFoundException($"Task with ID {id} not found.");
            }
            //convert from TaskTable to TaskDetailsOrCreateDto
            var task = new TaskDto()
            {
                Id = result.Id,
                Title = result.Title,
                Description = result.Description,
                Status = Enum.Parse<Status>(result.Status),
                Priority = Enum.Parse<Priority>(result.Priority),
                DueDate = result.DueDate
            };

            return task;
        }

        public async Task<TaskDetailsOrCreateDto> UpdateTaskAsync(TaskDetailsOrCreateDto task, int id)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task), "Task cannot be null");
            }
            if (id <= 0)
            {
                throw new ArgumentException("Invalid task ID", nameof(id));
            }
            var taskTable = new TaskTable()
            {
                Id = id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status.ToString(),
                Priority = task.Priority.ToString(),
                DueDate = task.DueDate,
                LastModifiedDate = DateTime.Now // Automatically set to current time
            };
            var updatedTask = await _repository.UpdateTaskAsync(taskTable);
           // Ensure changes are saved to the database
            if (updatedTask is  null)
            {
                throw new Exception("Failed to update task");
            }
            return task;
        }
    }
}
