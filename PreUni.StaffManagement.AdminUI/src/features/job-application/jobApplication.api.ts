import api from "services/api";

const apiURLs = {
  jobApplications: "jobApplications",
  jobApplicationById: (id: string) => `jobApplications/${id}`,
};

const API = {
  getJobApplications: (params: any) => api.get(apiURLs.jobApplications, { params }),
  deleteJobApplications: (id: string) => api.delete(apiURLs.jobApplicationById(id)),
};

export default API;
