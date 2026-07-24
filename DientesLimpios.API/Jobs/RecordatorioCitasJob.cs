using DientesLimpios.Aplicacion.CasosDeUso.Citas.Comandos.EnviarRecordatorioCita;
using DientesLimpios.Aplicacion.Utilidades.Mediador;

namespace DientesLimpios.API.Jobs
{
    public class RecordatorioCitasJob : BackgroundService
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly TimeZoneInfo zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("America/Managua");

        public RecordatorioCitasJob(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested) 
            {
                var ahora = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,zonaHoraria);

                if (ahora.Hour == 8) 
                {
                    using var scope = scopeFactory.CreateScope();
                    var mediador = scope.ServiceProvider.GetRequiredService<IMediator>();
                    await mediador.Send(new ComandoEnviarRecordatorioCita());
                }

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }
}
