using Backend.MinimalApi.Dtos;
using Backend.MinimalApi.Interfaces;

namespace Backend.MinimalApi.Endpoints;

public static class AnimalEndpoints
{
    public static IEndpointRouteBuilder MapAnimalEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/animals");

        group.MapGet("", GetAll);
        group.MapGet("{id}", GetById);
        group.MapPost("", Post).RequireAuthorization();
        group.MapPut("", Put).RequireAuthorization();
        group.MapDelete("{id}", Delete).RequireAuthorization("AdminOnly");

        return group;
    }

    private static async Task<IResult> GetAll(IAnimalService service)
    {
        var collection = await service.GetAllAnimalsAsync();

        collection = collection.ToList();

        if (!collection.Any())
        {
            return Results.NoContent();
        }

        return Results.Ok(collection);
    }

    private static async Task<IResult> GetById(IAnimalService service, int id)
    {
        var animal = await service.GetAnimalByIdAsync(id);

        if (animal is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(animal);
    }

    private static async Task<IResult> Post(IAnimalService service, AnimalDto animalDto)
    {
        var createdAnimal = await service.PostAnimalAsync(animalDto);

        return Results.Ok(createdAnimal);
    }

    private static async Task<IResult> Put(IAnimalService service, AnimalDto animalDto)
    {
        var updated = await service.UpdateAnimalAsync(animalDto);
        if (updated is null)
        {
            return Results.NotFound();
        }
        return Results.Ok(updated);
    }

    private static async Task<IResult> Delete(IAnimalService service, int id)
    {
        var deleted = await service.DeleteAnimalAsync(id);
        if (!deleted)
        {
            return Results.NotFound();
        }
        return Results.Ok();
    }
}