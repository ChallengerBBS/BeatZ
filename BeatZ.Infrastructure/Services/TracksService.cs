using BeatZ.Application.Common.Interfaces;
using BeatZ.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace BeatZ.Infrastructure.Services
{
    public class TracksService : ITrackService
    {
        private readonly IBeatzDbContext _dbContext;
        private readonly ILogger _logger;

        public TracksService(IBeatzDbContext dbContext, ILogger<TracksService> logger)
        {
            this._dbContext = dbContext;
            this._logger = logger;
        }

        public async Task<int> AddTrackAsync(Track track)
        {
            var trackToAdd = new Track()
            {
                TrackId = track.TrackId,
                TrackName = track.TrackName,
                FilePath = track.FilePath
            };

            foreach (var currentArtist in track.Artists)
            {
                var artist = this._dbContext.Artists.Where(x => x.ArtistId == currentArtist.ArtistId).FirstOrDefault();
                if (artist == null || (artist.ArtistName != currentArtist.ArtistName))
                {
                    return 0;
                }
            }
            trackToAdd.Artists.ToList().AddRange(track.Artists);

            if (trackToAdd.TrackId != 0)
            {
                this._dbContext.Tracks.Update(trackToAdd);
            }
            else
            {
                this._dbContext.Tracks.Add(trackToAdd);
            }
            await _dbContext.SaveChangesAsync(new CancellationToken());

            if (trackToAdd.TrackId > 0)
            {
                this._logger.LogInformation($"Insert/Update track: {trackToAdd}");
            }
            else
            {
                this._logger.LogInformation($"Problem occured while adding/updating track: {trackToAdd}");
            }

            return trackToAdd.TrackId;
        }

        public List<Track> GetAllTracks(Expression<Func<Track, bool>> predicate)
        {
            return this._dbContext.Tracks.Where(predicate).ToList();
        }

        public Track GetTrack(Expression<Func<Track, bool>> predicate)
        {
            return this._dbContext.Tracks.FirstOrDefault(predicate);
        }

        public async Task DeleteTrack(int id)
        {
            var track = this._dbContext.Tracks.FirstOrDefault(p => p.TrackId == id);
            if (track != null)
            {
                this._dbContext.Tracks.Remove(track);
                await _dbContext.SaveChangesAsync(new CancellationToken());
            }
        }

        public List<string> GetTrackArtistsNames(int trackId)
        {
            return this._dbContext.Tracks
                    .Where(c => c.TrackId == trackId)
                    .SelectMany(c => c.Artists)
                    .Select(c => c.ArtistName)
                    .ToList();
        }
    }
}
