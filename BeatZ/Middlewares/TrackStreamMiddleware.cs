using BeatZ.Application.Common.Interfaces;
using BeatZ.Infrastructure.Services;
using System.Buffers;

namespace BeatZ.Api.Middlewares
{
    public class TrackStreamMiddleware : IMiddleware
    {
        private readonly ITrackService _tracksService;

        public TrackStreamMiddleware(ITrackService tracksService)
        {
            this._tracksService = tracksService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var requestPath = context.Request.Path;

            const string TRACKS_PATH = "/api/tracks/play/";

            if (!requestPath.HasValue || !requestPath.Value!.StartsWith(TRACKS_PATH) || requestPath.Value.Length <= TRACKS_PATH.Length)
            {
                await next(context);
                return;
            }

            string reqTrackId = requestPath.Value.Remove(0, TRACKS_PATH.Length);

            int trackId;

            if (!int.TryParse(reqTrackId, out trackId))
            {
                context.Response.StatusCode = 400;
                return;
            }

            var track = this._tracksService.GetTrack(p=>p.TrackId == trackId);

            if (string.IsNullOrWhiteSpace(track.FilePath))
            {
                context.Response.StatusCode = 404;
                return;
            }

            string requestRange = context.Request.Headers["Range"];


            await using var fs = new FileStream(track.FilePath, FileMode.Open, FileAccess.Read);

            if (!string.IsNullOrWhiteSpace(requestRange))
            {
                context.Response.StatusCode = StatusCodes.Status206PartialContent;

                var parts = requestRange.Split('=');
                var rangeParts = parts[1].Split('-');

                long from = string.IsNullOrWhiteSpace(rangeParts[0]) ? 0 : long.Parse(rangeParts[0]);
                long to = string.IsNullOrWhiteSpace(rangeParts[1]) ? fs.Length : long.Parse(rangeParts[1]);

                fs.Position = from;

                if (to > fs.Length)
                {
                    to = fs.Length;
                }

                context.Response.Headers.ContentLength = to - from;
                context.Response.Headers["Content-Range"] = $"bytes {from}-{to - 1}/{fs.Length}";
                context.Response.Headers["Content-Type"] = "audio/mpeg";
                context.Response.Headers["Accept-Ranges"] = "bytes";

                var buffer = ArrayPool<byte>.Shared.Rent(8192);

                try
                {
                    long read;
                    long totalRead = 0;
                    while ((read = await fs.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        totalRead += read;
                        long position = from + totalRead;
                        if (position >= to)
                        {
                            long last = read - (position - to);
                            if (last > 0)
                            {
                                await context.Response.Body.WriteAsync(buffer, 0, (int)last);
                            }

                            break;
                        }

                        await context.Response.Body.WriteAsync(buffer, 0, (int)read);
                    }
                }
                finally
                {
                    ArrayPool<byte>.Shared.Return(buffer);
                }
            }
            else
            {
                context.Response.StatusCode = 200;
                context.Response.Headers.ContentLength = fs.Length;
                context.Response.Headers["Content-Type"] = "audio/mpeg";
                await fs.CopyToAsync(context.Response.Body);
            }
        }
    }
}
