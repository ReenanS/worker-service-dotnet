using Microsoft.Extensions.Options;
using Quartz;

namespace Infrastructure;

public class QuartzJobSetup : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        // Configura o Job que será executado a cada 1 minuto
        var jobKey1 = JobKey.Create(nameof(JobA));
        options
            .AddJob<JobA>(jobBuilder => jobBuilder.WithIdentity(jobKey1))
            .AddTrigger(trigger =>
                trigger
                    .ForJob(jobKey1)
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInMinutes(1).RepeatForever()));

        // Configura o Job que será executado a cada 3 minutos
        var jobKey2 = JobKey.Create(nameof(JobB));
        options
            .AddJob<JobB>(jobBuilder => jobBuilder.WithIdentity(jobKey2))
            .AddTrigger(trigger =>
                trigger
                    .ForJob(jobKey2)
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInMinutes(3).RepeatForever()));

        // Configura o Job que será executado a cada 5 minutos
        var jobKey3 = JobKey.Create(nameof(JobC));
        options
            .AddJob<JobC>(jobBuilder => jobBuilder.WithIdentity(jobKey3))
            .AddTrigger(trigger =>
                trigger
                    .ForJob(jobKey3)
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInMinutes(5).RepeatForever()));

    }
}