import {Fragment, useState} from "react";
import {Button, Card} from 'react-bootstrap';

const getTracks = (setSongs: any) => {
    fetch("https://localhost:7297/api/tracks")
        .then((res) => res.json())
        .then((data) => {
            console.log(data)
            setSongs(data);
        });
}

function TrackList() {
    const [songs, setSongs] = useState([]);

    return (
        <Fragment>
            <Button className="btn btn-primary" type="button" onClick={() => getTracks(setSongs)}>Load Tracks</Button>
            {songs && songs.map((track) => <Card>{track.trackName}</Card>)}
        </Fragment>
    );
}

export default TrackList