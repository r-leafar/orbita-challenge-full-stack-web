using EdTech.Application.Dtos;
using EdTech.Application.UseCases;
using EdTech.Core.Entities;
using EdTech.Core.Interfaces.Repositories;

namespace EdTech.WebApi.Endpoints
{
    public static class StudentEndpoints
    {
        public static RouteGroupBuilder MapStudentEndpoints(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateStudentRequest student, IStudentRepository repository) =>
            {
                var command =  new CreateStudentCommand(repository);
                var idStudent = await command.Handle(student);

                return Results.Created($"/students/{idStudent}", student);
            }).
            WithName("CreateStudents").
           Produces<CreateStudentRequest>(StatusCodes.Status201Created).
           Produces(StatusCodes.Status400BadRequest);

            return group;
        }
    }
}
