namespace todoApi.Application.Tests.Commands;

using Moq;
using todoApi.Application.Commands;
using todoApi.Application.Models;
using todoApi.Application.Repositories;

public class CreateTodoHandlerTests
{
    private readonly Mock<ITodoReadRepository> _readRepo = new();
    private readonly Mock<ITodoWriteRepository> _writeRepo = new();
    private readonly CreateTodoHandler _handler;

    public CreateTodoHandlerTests()
    {
        _handler = new CreateTodoHandler(_writeRepo.Object, _readRepo.Object);
    }

    [Fact]
    public async Task Handle_WithValidName_CreatesTodo()
    {
        _readRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Todo>());
        _writeRepo.Setup(r => r.CreateAsync(It.IsAny<Todo>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Todo t, CancellationToken ct) => t);

        var result = await _handler.Handle(
            new CreateTodoCommand("new todo", false),
            CancellationToken.None);

        Assert.Equal("new todo", result.Name);
        Assert.False(result.IsComplete);
        _writeRepo.Verify(r => r.CreateAsync(It.IsAny<Todo>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithDuplicateName_ThrowsException()
    {
        _readRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Todo>
          {
              new() { Id = 1, Name = "existing todo", IsComplete = false }
          });

        var act = () => _handler.Handle(
            new CreateTodoCommand("Existing Todo", false),
            CancellationToken.None);

        await Assert.ThrowsAsync<InvalidOperationException>(act);
    }

    [Fact]
    public async Task Handle_WithCompletedTodo_SetsIsComplete()
    {
        _readRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Todo>());
        _writeRepo.Setup(r => r.CreateAsync(It.IsAny<Todo>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Todo t, CancellationToken ct) => t);

        var result = await _handler.Handle(
            new CreateTodoCommand("completed todo", true),
            CancellationToken.None);

        Assert.True(result.IsComplete);
    }
}