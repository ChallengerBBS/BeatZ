using BeatZ.Domain.Entities;
using System.Linq.Expressions;

namespace BeatZ.Application.Common.Interfaces
{
   public interface ITrackService
    {
        public Task<int> AddTrackAsync(Track track);

        public List<Track> GetAllTracks(Expression<Func<Track, bool>> predicate);

        public Track GetTrack(Expression<Func<Track, bool>> predicate);

        public List<string> GetTrackArtistsNames(int trackId);

        public Task DeleteTrack(int id);
    }
}
