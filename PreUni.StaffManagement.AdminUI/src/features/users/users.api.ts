import api from "services/api";

const apiURLs = {
  users: "users",
  userById: (id: string) => `users/${id}`,
};

const API = {
  getUsers: (params: any) => api.get(apiURLs.users, { params }),
  deleteUser: (id: string) => api.delete(apiURLs.userById(id)),
};

export default API;
