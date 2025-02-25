using Backend.MinimalApi.Dtos;

namespace Backend.MinimalApi.Endpoints;

public static class AnimalEndpoints
{
    private static List<Animal> _animals = new()
    {
        new Animal("Dog", "Wof wof"),
        new Animal("Cat", "Meow"),
        new Animal("Duck", "Quack")
    };

    public static IEndpointRouteBuilder MapAnimalEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/animals");

        group.MapGet("", GetAll);
        group.MapGet("{type}", GetByType);
        group.MapPost("", Post);
        group.MapDelete("{type}", DeleteByType);

        return group;
    }

    private static IResult DeleteByType(string type)
    {
        var animalToDelete = _animals.FirstOrDefault(x => x.Type == type);

        if (animalToDelete is null)
        {
            return Results.NotFound();
        }

        _animals.Remove(animalToDelete);

        return Results.Ok();
    }

    private static IResult Post(Animal animal)
    {
        _animals.Add(animal);

        return Results.Created();
    }

    private static IResult GetAll()
    {
        return Results.Ok(_animals);
    }

    private static IResult GetByType(string type)
    {
        var animal = _animals
            .FirstOrDefault(x => x.Type == type);

        if (animal is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(animal);
    }
}