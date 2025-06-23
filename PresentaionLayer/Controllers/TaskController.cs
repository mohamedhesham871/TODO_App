using LogicLayer;
using LogicLayer.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentaionLayer.Dtos;

namespace PresentaionLayer.Controllers
{
    public class TaskController(ITaskServices services, ILogger<TaskController> _logger, IWebHostEnvironment _environment) : Controller
    {
        private readonly ITaskServices services = services;

        [HttpGet]
        public IActionResult Index()
        {
            // Get all Tasks
            var Tasks = services.GetAllTasksAsync().Result;
            return View(Tasks);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var Task = await services.GetTaskByIdAsync(id);
            return View(Task);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {

            if (id <= 0) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var task = services.DeleteTaskAsync(id);

                    if (task is not null) return RedirectToAction("Index");
                    else
                    {
                        ModelState.AddModelError("", "Can't Delete Task");
                        return RedirectToAction("Delete", new { id });
                    }
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        ModelState.AddModelError("", ex.Message);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                    }
                }
            }
            return View("Error");

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TaskDetailsOrCreateDto task)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   
                    var createdtask = await services.AddTaskAsync(task);

                    if (createdtask is not null) return RedirectToAction("Index");
                    else
                    {
                        ModelState.AddModelError("", "Can't Create Task");
                        return View(task);
                    }
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        ModelState.AddModelError("", ex.Message);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                    }
                }
            }
            return View(task);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await services.GetTaskByIdAsync(id);
            if (task is null) return NotFound();
            var taskDto = new TaskDetailsOrCreateDto
            {
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                Priority = task.Priority,
                DueDate = task.DueDate
            };
            return View(taskDto);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TaskDetailsOrCreateDto task, int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await services.UpdateTaskAsync(task, id);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        ModelState.AddModelError("", ex.Message);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                    }
                }
            }
            return View(task);

        }
        //Mark as complete
        [HttpPost]
        public async Task<IActionResult> MarkAsComplete(int id)
        {
            if (id <= 0) return BadRequest();

            var task = await services.GetTaskByIdAsync(id);
            if (task == null) return NotFound();

            var taskDto = new TaskDetailsOrCreateDto
            {
                Title = task.Title,
                Description = task.Description,
                Status = Status.Completed,
                Priority = task.Priority,
                DueDate = task.DueDate
            };
            await Edit(taskDto, id);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> FilterByStatus(List<string> statuses)
        {
            if (statuses == null || !statuses.Any())
            {
                ModelState.AddModelError("", "Please select at least one status to filter.");
                return View("Index", await services.GetAllTasksAsync());
            }
            try
            {
                var filteredTasks = await services.GetFilteredStatusAsync(statuses);
                if (filteredTasks == null || !filteredTasks.Any())
                {
                    ModelState.AddModelError("", "No tasks found for the selected status(es).");
                    return View("Index", await services.GetAllTasksAsync());
                }
                return View("Index", filteredTasks);
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError("", ex.Message);
                }
                else
                {
                    _logger.LogError(ex.Message);
                }
            }
            return View("Index", await services.GetAllTasksAsync());
        }
    }
}
