using PeopleManagement.Application.Features.Tooltips;

namespace PeopleManagement.Tests.Features.Tooltips;

public sealed class TooltipsServiceTests
{
    [Fact]
    public async Task ObterAsync_DeveRetornarTooltipDoRepositorio()
    {
        var service = new TooltipsService(new FakeTooltipsRepository());

        var tooltip = await service.ObterAsync("disc", CancellationToken.None);

        Assert.NotNull(tooltip);
        Assert.Equal("disc", tooltip.ChaveCampo);
    }

    private sealed class FakeTooltipsRepository : ITooltipsRepository
    {
        public Task<TooltipResponse?> ObterAsync(string chaveCampo, CancellationToken cancellationToken)
            => Task.FromResult<TooltipResponse?>(new TooltipResponse(chaveCampo, "texto"));

        public Task SalvarAsync(string chaveCampo, string texto, CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}

