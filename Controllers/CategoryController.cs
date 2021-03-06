using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

[Route("categories")]
public class CategoryController : ControllerBase
{
    [HttpGet]
    [Route("")]
    public async Task<ActionResult<List<Category>>> Get([FromServices] DataContext context)
    {
        var categories = await context.Categories.AsNoTracking().ToListAsync();
        return Ok(categories);
    }
    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Category>> GetbyId(int id, [FromServices] DataContext context)
    {
        var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return Ok(category);
    }
    [HttpPost]
    [Route("")]
    public async Task<ActionResult<Category>> Post([FromBody] Category model,
    [FromServices] DataContext context)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            context.Categories.Add(model);
            await context.SaveChangesAsync();
            return Ok(model);
        }
        catch (Exception)
        {
            return BadRequest(new { message = "Nao foi possivel criar a categoria" });
        }
    }
    [HttpPut]
    [Route("{id:int}")]
    public async Task<ActionResult<Category>> Put(int id, [FromBody] Category model,
    [FromServices] DataContext context)
    {
        if (model.Id != id)
            return NotFound(new { message = "Categoria nao encontrada" });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            context.Entry<Category>(model).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(model);
        }
        catch (DbUpdateConcurrencyException)
        {
            return BadRequest(new { message = "Nao foi possivel atualizar a categoria" });
        }
        catch (Exception)
        {
            return BadRequest(new { message = "Nao foi possivel atualizar a categoria" });
        }

    }
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult<Category>> Delete(int id, [FromServices] DataContext context)
    {
        var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category == null)
            return NotFound(new { message = "Categoria nao encontrada" });


        try
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return Ok(new { message = "Categoria removida com sucesso!" });

        }
        catch (Exception)
        {
            return BadRequest(new { message = "Nao foi possivel remover a categoria" });
        }
    }
}



