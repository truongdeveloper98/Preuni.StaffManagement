import api from "services/api";

const apiURLs = {
  jobApplicationById: (userId: string) => `jobApplications/${userId}`,
  jobApplication: "jobApplications",
};

const API = {
  updateJobApplication: (data: any, id: string) => api.put(apiURLs.jobApplicationById(id), data),
  createJobApplication: (data: any) => api.post(apiURLs.jobApplication, data),
  getJobApplicationById: (id: string) => api.get(apiURLs.jobApplicationById(id)),
};

export default API;
