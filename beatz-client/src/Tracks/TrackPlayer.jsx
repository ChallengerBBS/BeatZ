import {Fragment, useState} from "react";
import {Button, Card} from 'react-bootstrap';

function TrackPlayer() {
    const [songs, setSongs] = useState([]);

    return (
        <Fragment>
            {songs && songs.map((track) =>
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
                </Card>
            )}
        </Fragment>
    );
}

export default TrackPlayer