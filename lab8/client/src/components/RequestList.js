import axios from "axios";
const link = 'http://localhost:8000'

export default class RequestList {
    static async getAll() {
        const response = await axios.get(`${link}/list`)
        return response;
    }

    static async getById(id) {
        const response = await axios.get(`${link}/list/` + id)
        return response;
    }

    static async delById(id) {
        const response = await axios.delete(`${link}/list/` + id)
        return response;
    }

    static async putById(id, data) {
        const response = await axios.put(`${link}/list/` + id, data)
        return response;
    }

    static async addElem(data){
        const response = await axios.post(`${link}/list`, data)
        return response;
    }
}