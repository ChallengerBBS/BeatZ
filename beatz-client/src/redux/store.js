import { configureStore } from '@reduxjs/toolkit'
import tracksReducer from '../redux/Tracks/tracksSlice'

export default configureStore({
    reducer: {
        tracks: tracksReducer
    },
})