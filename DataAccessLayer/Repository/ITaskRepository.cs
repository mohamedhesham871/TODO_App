using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public  interface ITaskRepository
    {
        public Task<List<TaskTable>> GetAllTasksAsync();
        public Task<TaskTable> GetTaskByIdAsync(int id);
        public Task<TaskTable> AddTaskAsync(TaskTable task);
        public Task<TaskTable> UpdateTaskAsync(TaskTable task);
        public Task<bool> DeleteTaskAsync(TaskTable task);
        public Task<List<TaskTable>> GetFilteredStatusAsync(List<string> statuses);
    }
}
