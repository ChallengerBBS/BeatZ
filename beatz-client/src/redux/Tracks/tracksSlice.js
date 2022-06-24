import {createSlice} from '@reduxjs/toolkit'

export const tracksSlice = createSlice({
    name: 'tracks',
    initialState: {
        tracksListDto: undefined,
    },
    reducers: {
        fetchTrackListDto: async (state) => {
             await fetch("https://localhost:7297/api/tracks")
                .then((res) => res.json())
                .then((res)=> state.tracksListDto = res);
        },
    },
})

// Action creators are generated for each case reducer function
export const {fetchTrackListDto} = tracksSlice.actions

export default tracksSlice.reducer