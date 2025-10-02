using EdTech.Application.Dtos;
using EdTech.Application.UseCases;
using EdTech.Application.Mappings;
using EdTech.Core.Interfaces.Repositories;

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
            WithName("CreateStudents").
           Produces<CreateStudentResponse>(StatusCodes.Status201Created).
           Produces(StatusCodes.Status400BadRequest);

            return group;
        }
    }
}
