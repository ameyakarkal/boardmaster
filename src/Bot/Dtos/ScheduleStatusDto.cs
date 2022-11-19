using System;

namespace Bot.Dtos;

public class ScheduleStatusDto
{
    public DateTime Last { get; set; }

    public DateTime Next { get; set; }

    public DateTime LastUpdated { get; set; }
}