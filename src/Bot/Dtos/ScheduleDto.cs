namespace Bot.Dtos;

public class ScheduleDto
{
    public ScheduleStatusDto ScheduleStatus { get; set; }

    public bool IsPastDue { get; set; }
}