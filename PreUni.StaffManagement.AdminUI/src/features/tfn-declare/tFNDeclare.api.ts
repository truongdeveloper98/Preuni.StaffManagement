import api from "services/api";

const apiURLs = {
  tfnDeclareById: (jobApplicationId: number, method: string) =>
    `TFNDeclare/${method}?jobApplicationId=${jobApplicationId}`,
};

const API = {
  getTFNDeclare: (jobApplicationId: number) =>
    api.get(apiURLs.tfnDeclareById(jobApplicationId, "GetTFNDeclareById")),
  updateTFNDeclare: (body: any, jobApplicationId: number) =>
    api.post(apiURLs.tfnDeclareById(jobApplicationId, "UpdateTFNDeclare"), JSON.stringify(body)),
};

export default API;
