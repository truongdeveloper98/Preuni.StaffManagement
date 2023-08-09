import api from "services/api";

const apiURLs = {
  user: (userId: string) => `users/${userId}`,
  users: "users",
};

const API = {
  updateUser: (data: any, id: string) => api.put(apiURLs.user(id), data),
  createUser: (data: any) => api.post(apiURLs.users, data),
  getUserById: (id: string) => api.get(apiURLs.user(id)),
};

export default API;
