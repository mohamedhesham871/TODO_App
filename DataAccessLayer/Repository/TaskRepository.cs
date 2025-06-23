using DataAccessLayer.DBContexts;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class TaskRepository(DbApplication context) : ITaskRepository
    {
        private readonly DbApplication _context = context;

        // Add Task
        public async Task<TaskTable> AddTaskAsync(TaskTable task)
        {
            _context.TaskTables.Add(task);
            return await _context.SaveChangesAsync().ContinueWith(t => task);
        }
        // Delete Task By Id
        public async Task<bool> DeleteTaskAsync(TaskTable entity)
        {
             _context.Remove(entity);
            return await _context.SaveChangesAsync()>0 ?true:false;
        }
        // Get All Tasks
        public async Task<List<TaskTable>> GetAllTasksAsync()
        {
            var tasks =await _context.TaskTables.ToListAsync();
            return tasks;

        }
        //Get By id
        public async Task<TaskTable> GetTaskByIdAsync(int id)
        {
           var task =await _context.TaskTables.FindAsync(id);
            if (task is null)
            {
               return null!; //Handle error from Services 
            }
            return task;
        }
        // Update Task
        public async Task<TaskTable> UpdateTaskAsync(TaskTable task)
        {
            var existing = await _context.TaskTables.FindAsync(task.Id);
            if (existing == null)
                throw new Exception("Task not found");

            // Update properties manually
            existing.Title = task.Title;
            existing.Description = task.Description;
            existing.Status = task.Status;
            existing.Priority = task.Priority;
            existing.DueDate = task.DueDate;
            existing.LastModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return existing;
        }

        // Get Filter 
        public async Task<List<TaskTable>> GetFilteredStatusAsync(List<string> statuses)
        {
            

            var query = await _context.TaskTables
                                      .Where(t => statuses.Contains(t.Status))
                                      .ToListAsync();
            return query;
        }


    }
}
