import { post } from './requestService';

export const register = (email, password) => {
    return post('users', { email, password });
}

export const login = (email, password) => {
    return post('sessions', { email, password })
}
