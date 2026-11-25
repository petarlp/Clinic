import * as request from '../lib/request';

const baseUrl = '/Patients';

export const getAll = () =>
    request.get(baseUrl);

export const getById = (id) =>
    request.get(`${baseUrl}/${id}`);

export const create = (data) =>
    request.post(baseUrl, data);

export const update = (id, data) =>
    request.put(`${baseUrl}/${id}`, data);

export const del = (id) =>
    request.remove(`${baseUrl}/${id}`);
