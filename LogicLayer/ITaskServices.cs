using LogicLayer.Dtos;
using PresentaionLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface ITaskServices
    {
        Task<List<TaskDto>> GetAllTasksAsync();
        Task<TaskDto> GetTaskByIdAsync(int id);
        Task<TaskDetailsOrCreateDto> AddTaskAsync(TaskDetailsOrCreateDto task);
        Task<TaskDetailsOrCreateDto>  UpdateTaskAsync(TaskDetailsOrCreateDto task,int id);
        Task<bool> DeleteTaskAsync(int id);
        public  Task<List<TaskDto>> GetFilteredStatusAsync(List<string> statuses);
    }
}
