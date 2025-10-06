using EdTech.Application.Mappings;
using EdTech.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using EdTech.Application.Dtos.Requests;
using EdTech.Application.Dtos.Responses;
using EdTech.Application.UseCases.Command;
using EdTech.Application.UseCases.Query;
using EdTech.Application.UseCases.Queries;

namespace EdTech.WebApi.Endpoints
{
    public static class StudentEndpoints
    {
        public static RouteGroupBuilder MapStudentEndpoints(this RouteGroupBuilder group)
        {
            group.MapPost("/", CreateStudent)
                 .WithName("CreateStudent")
                 .Produces<StudentResponse>(StatusCodes.Status201Created)
                 .Produces(StatusCodes.Status400BadRequest);

            group.MapPut("/", UpdateStudent)
                 .WithName("UpdateStudent")
                 .Produces<NoContent>(StatusCodes.Status204NoContent)
                 .Produces(StatusCodes.Status400BadRequest);

            group.MapDelete("/{id:guid}", DeleteStudent)
                 .WithName("DeleteStudent")
                 .Produces<NoContent>(StatusCodes.Status204NoContent)
                 .Produces(StatusCodes.Status400BadRequest);

            group.MapGet("/{id:guid}", GetStudentById)
                 .WithName("QueryStudentById")
                 .Produces<StudentResponse>(StatusCodes.Status200OK)
                 .Produces(StatusCodes.Status400BadRequest);

            group.MapGet("/{page:int}/{pageSize:int}", GetStudentsPaged)
                 .WithName("QueryStudentsPaged")
                 .Produces<PagedResponse<StudentResponse>>(StatusCodes.Status200OK)
                 .Produces(StatusCodes.Status400BadRequest);

            group.MapGet("/{name}/{page:int}/{pageSize:int}", GetStudentByName)
                .WithName("QueryStudentsByNamePaged")
                .Produces<PagedResponse<StudentResponse>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest);

            return group;
        }

        private static async Task<IResult> CreateStudent(CreateStudentRequest student, IStudentRepository repository)
        {
            var command = new CreateStudentCommand(repository);
            var idStudent = await command.Handle(student);
            return Results.Created($"/students/{idStudent}", student.ToResponse(idStudent));
        }

        private static async Task<IResult> UpdateStudent(UpdateStudentRequest student, IStudentRepository repository)
        {
            var command = new UpdateStudentCommand(repository);
            await command.Handle(student);
            return Results.NoContent();
        }

        private static async Task<IResult> DeleteStudent(Guid id, IStudentRepository repository)
        {
            var command = new DeleteStudentCommand(repository);
            await command.Handle(id);
            return Results.NoContent();
        }

        private static async Task<IResult> GetStudentById(Guid id, IStudentRepository repository)
        {
            var query = new GetStudentByIdQuery(repository);
            var student = await query.Handle(id);
            return Results.Ok(student);
        }

        private static async Task<IResult> GetStudentByName(string name, int page, int pageSize, IStudentRepository repository)
        {
            var query = new GetStudentByNamePaged(repository);
            var student = await query.Handle(name,page, pageSize);
            return Results.Ok(student);
        }

        private static async Task<IResult> GetStudentsPaged(int page, int pageSize, IStudentRepository repository)
        {
            var query = new GetStudentPagedQuery(repository);
            var students = await query.Handle(page, pageSize);
            return Results.Ok(students);
        }
    }
}
