import { useSessionStore } from '../stores/session'
import axios from 'axios';

const baseUrl = 'http://localhost:3000/';

export const post = async (path, body, jwt = null, headers = {}) => {
    console.log(body);
    const response = await axios.post(baseUrl + path, body, { headers: { 'Content-Type': 'Application/Json', 'Authorization': "Bearer " + jwt, ...headers }});

    return response;
}

export const get = async(path, jwt = null, headers = {}) => {
    const response = await axios.get(baseUrl + path, { headers: { 'Content-Type': 'Application/Json', 'Authorization': "Bearer " + jwt, ...headers }});

    return response;
}
