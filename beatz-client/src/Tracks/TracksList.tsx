import {Fragment} from "react";

const getTracks = () => {
    fetch("https://localhost:7297/api/tracks")
        .then((res) => res.json())
        .then((data) => console.log(data));
}

function TrackList(){
    return (
        <Fragment>
            <button onClick={() => getTracks()}>Load Tracks</button>
        </Fragment>
    );
}

export default TrackList