using EdTech.Application.Dtos;
using EdTech.Application.UseCases;
using EdTech.Application.Mappings;
using EdTech.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EdTech.WebApi.Endpoints
{
    public static class StudentEndpoints
    {
        public static RouteGroupBuilder MapStudentEndpoints(this RouteGroupBuilder group)
        {

            group.MapPost("/", async (CreateStudentRequest student, IStudentRepository repository) =>
            {
                    var command = new CreateStudentCommand(repository);
                    var idStudent = await command.Handle(student);

                    return Results.Created($"/students/{idStudent}", student.ToResponse(idStudent));                
            }).
            WithName("CreateStudent").
           Produces<CreateStudentResponse>(StatusCodes.Status201Created).
           Produces(StatusCodes.Status400BadRequest);

            group.MapPut("/", async (UpdateStudentRequest student, IStudentRepository repository) =>
            {
                var command = new UpdateStudentCommand(repository);
                await command.Handle(student);

                return Results.Created($"/students/", Results.NoContent);
            }).
           WithName("UpdateStudent").
           Produces<NoContent>(StatusCodes.Status201Created).
           Produces(StatusCodes.Status400BadRequest);

            return group;
        }
    }
}
