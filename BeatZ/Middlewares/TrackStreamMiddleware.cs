using BeatZ.Infrastructure.Services;

namespace BeatZ.Api.Middlewares
{
    public class TrackStreamMiddleware : IMiddleware
    {
        private readonly TracksService _tracksService;

        public TrackStreamMiddleware(TracksService tracksService)
        {
            this._tracksService = tracksService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var requestPath = context.Request.Path;

            const string RECORDINGS_PATH = "/recordings/";

            if (!requestPath.HasValue || !requestPath.Value!.StartsWith(RECORDINGS_PATH) || requestPath.Value.Length <= RECORDINGS_PATH.Length)
            {
                await next(context);
                return;
            }

            string reqTrackId = requestPath.Value.Remove(0, RECORDINGS_PATH.Length);

            int trackId;

            if (!int.TryParse(reqTrackId, out trackId))
            {
                context.Response.StatusCode = 400;
                return;
            }

            var track = this._tracksService.GetTrack(p=>p.TrackId == trackId);
        }
    }
}
