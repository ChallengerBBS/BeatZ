import { useEffect, useState} from "react";
import {Card} from 'react-bootstrap';
import * as trackService from '../services/tracksService';


export default function TrackList() {
    const [tracks, setTracks] = useState([]);

    useEffect(() => {
        trackService.getAll()
            .then((res) => setTracks(res))
            .catch((err) => console.log(err));
    }, [])



    return (
        <>
            {tracks.length > 0 ? tracks.map(track =>
                    <Card key={track.trackId}>
                        <div>
                            Track id: {track.trackId}
                        </div>
                        <div>
                            Track name: {track.trackName}
                        </div>
                        <div>
                            Artists: {track.artists.join(", ")}
                        </div>
                        <audio controls preload="none">
                            <source
                                src={`https://localhost:7297/api/tracks/play/${track.trackId}`}
                                type="audio/wav"
                            />
                            Your browser does not support the audio element.
                        </audio>
                    </Card>
            )
                : <p>No tracks in database!</p>}
        </>
    );
}