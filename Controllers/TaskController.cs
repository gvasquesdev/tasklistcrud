using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskList.Data;
using TaskList.Models;

namespace TaskList.Controllers
{
    [ApiController]
    public class TaskController : ControllerBase
    {
        //Obter todas as tarefas
        [HttpGet("/")]
        public IActionResult GetAllTarefas([FromServices] AppDbContext context)
        {
            return Ok(context.Tarefas.ToList());
        }

        //Obter uma tarefa pelo seu Id
        [HttpGet("/tarefa/{id:int}")]
        public IActionResult GetTaskById([FromServices] AppDbContext context, [FromRoute] int id) 
        {
            var task = context.Tarefas.Find(id);
            
            if(task == null) 
            {
                return NotFound("Tarefa não encontrada");
            } else {
                return Ok(task);
            }
        }
        //Criar uma tarefa
        [HttpPost("/tarefa")]
        public IActionResult PostTask([FromBody] TaskModel newTask, [FromServices] AppDbContext context) 
        {
            var task = context.Tarefas.Add(newTask);
            context.SaveChanges();

            return Ok(task);
        }

        //Atualizar uma tarefa
        [HttpPut("/tarefa/{id:int}")]
        public IActionResult PutTask([FromServices] AppDbContext context, [FromRoute] int id, [FromBody] TaskModel newTask)
        {
            var updateTask = context.Tarefas.Find(id);
            if(updateTask == null) 
            {
                return NotFound("Tarefa não encontrada");
            } else {
                updateTask.Title = newTask.Title;
                updateTask.Description = newTask.Description;
                context.SaveChanges();
                return Ok(updateTask);
            }
            
        }

        //Deletar uma tarefa
        [HttpDelete("tarefa/{id:int}")]
        public IActionResult DeleteTask([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var deleteTask = context.Tarefas.Find(id);
            context.Tarefas.Remove(deleteTask);
            context.SaveChanges();

            return Ok();
        }

        //Obter lista de tarefas baseado no status informado
        [HttpGet("/tarefa/{taskStatus}")]
        public IActionResult GetTaskById([FromServices] AppDbContext context, [FromRoute] string taskStatus) 
        {
            var tasks = context.Tarefas
            .Where(t => t.IsCompleted == taskStatus)
            .ToList();

            return Ok(tasks);
        }

        //Avançar o status de uma tarefa passando apenas o Id dela
            //quando uma tarefa for alterada para o status Finalizado, a data de conclusão deve ser preenchida
        [HttpPatch("/tarefa/{id:int}")]
        public IActionResult CompleteTask([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var completedTask = context.Tarefas.Find(id);
            if(completedTask == null) 
            {
                return NotFound("Tarefa não encontrada");
            } else {
                completedTask.IsCompleted = "finalizado";
                completedTask.CompletionDate = DateTime.Now;
                context.SaveChanges();
                return Ok(completedTask);
            }
            
        }
    }
}