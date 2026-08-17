import axios from "axios";

const api = axios.create({
    baseURL: "http://localhost:5000/api",
});

api.interceptors.response.use(
    (response) => response,
    (error) => {
        if (error.response?.data?.detail) {
            error.message = error.response.data.detail;
        } else if (error.response?.data?.message) {
            error.message = error.response.data.message;
        } else if (error.response?.data) {
            error.message = JSON.stringify(error.response.data);
        }
        return Promise.reject(error);
    }
);

export default api;