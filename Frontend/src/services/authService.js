import * as request from '../lib/request';

const baseUrl = '/Auth';

export const login = (email, password) =>
    request.post(`${baseUrl}/login`, {
        email,
        password,
    });

export const register = (email, password) =>
    request.post(`${baseUrl}/register`, {
        email,
        password,
    });

export const getProfile = () =>
    request.get(`${baseUrl}/me`);

export const logout = () => {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('username');
    localStorage.removeItem('user_type');
    localStorage.removeItem('user_email');
};