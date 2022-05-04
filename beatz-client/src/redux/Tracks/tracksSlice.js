import {createSlice} from '@reduxjs/toolkit'

export const tracksSlice = createSlice({
    name: 'tracks',
    initialState: {
        tracksListDto: undefined,
    },
    reducers: {
        fetchTrackListDto: async (state) => {
            state.tracksListDto = await fetch("https://localhost:7297/api/tracks")
                .then((res) => res.json());
        },
    },
})

// Action creators are generated for each case reducer function
export const {fetchTrackListDto} = tracksSlice.actions

export default tracksSlice.reducer