using EdTech.Application.Mappings;
using EdTech.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using EdTech.Application.Dtos.Requests;
using EdTech.Application.Dtos.Responses;
using EdTech.Application.UseCases.Command;
using EdTech.Application.UseCases.Query;

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
           Produces<StudentResponse>(StatusCodes.Status204NoContent).
           Produces(StatusCodes.Status400BadRequest);

            group.MapPut("/", async (UpdateStudentRequest student, IStudentRepository repository) =>
            {
                var command = new UpdateStudentCommand(repository);
                await command.Handle(student);

                return Results.NoContent();
            }).
           WithName("UpdateStudent").
           Produces<NoContent>(StatusCodes.Status204NoContent).
           Produces(StatusCodes.Status400BadRequest);

            group.MapDelete("/{id:guid}", async (Guid id, IStudentRepository repository) =>
            {
                var command = new DeleteStudentCommand(repository);
                await command.Handle(id);

                return Results.NoContent();
            }).
           WithName("DeleteStudent").
           Produces<NoContent>(StatusCodes.Status204NoContent).
           Produces(StatusCodes.Status400BadRequest);

         group.MapGet("/{id:guid}", async (Guid id, IStudentRepository repository) =>
            {
                var query = new GetStudentByIdQuery(repository);
                return await query.Handle(id);

            }).
         WithName("QueryStudentById").
         Produces<StudentResponse>(StatusCodes.Status200OK).
         Produces(StatusCodes.Status400BadRequest);

        group.MapGet("/{page:int}/{pageSize:int}", async (int page,int pageSize,IStudentRepository repository) =>
        {
            var query = new GetStudentPagedQuery(repository);
            return await query.Handle(page,pageSize);

        }).
        WithName("QueryStudentsPaged").
        Produces<PagedResponse<StudentResponse>>(StatusCodes.Status200OK).
        Produces(StatusCodes.Status400BadRequest);

            return group;
        }
    }
}
