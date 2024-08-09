using gestion_congregacion.api.Features.MeetingEvents;
using gestion_congregacion.api.Features.Meetings;
using Microsoft.EntityFrameworkCore;
using PuppeteerSharp;
using System.Globalization;

namespace gestion_congregacion.api.Features.FetchMeetingEvents
{
    public class FetchMeetingEventsServices :IFetchMeetingEventsServices
    {
        private readonly IConfiguration _configuration;
        private readonly IMeetingRepository _meetingRepository;

        public FetchMeetingEventsServices(
            IConfiguration configuration,
            IMeetingRepository meetingRepository)
        {
            _configuration = configuration;
            _meetingRepository = meetingRepository;
        }  

        public async Task SaveEventsFromJW()
        {
            var lastEvent = await _meetingRepository
                .GetQuery()
                .OrderByDescending( i => i.WeekDate)
                .FirstOrDefaultAsync()
                ;
            //var lastEventDate = lastEvent?.WeekDate ?? ;


            //await FetchEventsFromJW(new DateOnly(2024, 8, 5));
        }

        public async Task<string> GetFetchScript()
        {
            var path = Path.Join(AppDomain.CurrentDomain.BaseDirectory, "Features", "FetchMeetingEvents", "fetch-jw.js");
            return await File.ReadAllTextAsync(path);
        }

        public async Task<List<MeetingEvent>> FetchEventsFromJW(DateOnly weekDay)
        {
            var weekOfTheYear = GetWeekOfYear(weekDay);
            List<MeetingEvent> events;
            var url = $"https://wol.jw.org/es/wol/meetings/r4/lp-s/{weekDay.Year}/{weekOfTheYear}/";
            using (var browser = await Puppeteer
                                        .LaunchAsync(new LaunchOptions { Headless = true }))
            using (var page = await browser.NewPageAsync())
            {
                await page.SetUserAgentAsync(_configuration["DefaultUserAgent"]);
                await page.GoToAsync(url);
                var script = await GetFetchScript();
                events = await page.EvaluateExpressionAsync<List<MeetingEvent>>(script);
            }
            return events;
        }
        public int GetWeekOfYear(DateOnly date)
        {
            var cultureInfo = CultureInfo.CurrentCulture;
            var calendar = cultureInfo.Calendar;
            var dateTime = date.ToDateTime(TimeOnly.MinValue);
            var weekOfYear = calendar.GetWeekOfYear(dateTime, cultureInfo.DateTimeFormat.CalendarWeekRule, cultureInfo.DateTimeFormat.FirstDayOfWeek);
            return weekOfYear;
        }
    }
}
