import {Fragment} from "react";
import {Button, Card} from 'react-bootstrap';

import {useSelector, useDispatch} from 'react-redux'
import {fetchTrackListDto} from '../redux/Tracks/tracksSlice'

function TrackList() {
    const tracks = useSelector((state) => state.tracks.tracksListDto);
    const dispatch = useDispatch()

    return (
        <Fragment>
            <Button className="btn btn-primary" type="button" onClick={() => dispatch(fetchTrackListDto())}>Load
                Tracks</Button>
            {tracks && tracks.map((track) =>
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

export default TrackList