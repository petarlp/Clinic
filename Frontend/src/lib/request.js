
const API_URL = 'http://localhost:5001/api';

const buildOptions = (data) => {
    const options = {
        headers: {}
    };

    if (data !== undefined && data !== null) {
        options.body = JSON.stringify(data);
        options.headers['Content-Type'] = 'application/json';
    }

    const token = localStorage.getItem('accessToken');

    if (token) {
        options.headers.Authorization = `Bearer ${token}`;
    }

    return options;
};

const request = async (method, url, data) => {
    const fullUrl = `${API_URL}${url}`;

    console.log('API REQUEST:', fullUrl);

    const response = await fetch(fullUrl, {
        ...buildOptions(data),
        method,
    });

    if (response.status === 204) {
        return {};
    }

    const contentType = response.headers.get('content-type');

    const result = contentType?.includes('application/json')
        ? await response.json()
        : await response.text();

    if (!response.ok) {
        throw result;
    }

    return result;
};

export const get = request.bind(null, 'GET');
export const post = request.bind(null, 'POST');
export const put = request.bind(null, 'PUT');
export const remove = request.bind(null, 'DELETE');
export const patch = request.bind(null, 'PATCH');
