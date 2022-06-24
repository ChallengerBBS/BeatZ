import * as request from './requester';

const baseUrl = 'https://localhost:7297/api';

export const getAll = () => request.get(`${baseUrl}/Tracks`);

export const getTrack = (trackId) => {
    return request.get(`${baseUrl}/tracks/${trackId}`);
};

export const create = async (trackData, token) => {
    let response = await fetch(`${baseUrl}/tracks`, {
        method: 'POST',
        headers: {
            'content-type': 'application/json',
            'X-Authorization': token,
        },
        body: JSON.stringify({...trackData})
    });

    return await response.json();
};

export const update = (trackId, trackData) => request.put(`${baseUrl}/tracks/${trackId}`, trackData);


export const destroy = (trackId, token) => {
    return fetch(`${baseUrl}/tracks/${trackId}`, {
        method: 'DELETE',
        headers: {
            'X-Authorization': token
        }
    }).then(res => res.json());
};