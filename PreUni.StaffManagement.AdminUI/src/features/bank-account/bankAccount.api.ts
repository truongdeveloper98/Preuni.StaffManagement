import api from "services/api";

const apiURLs = {
  bankAccountById: (jobApplicationId: number, method: string) =>
    `BankAccount/${method}?jobApplicationId=${jobApplicationId}`,
};

const API = {
  getBankAccount: (jobApplicationId: number) =>
    api.get(apiURLs.bankAccountById(jobApplicationId, "GetBankAccountById")),
  updateBankAccount: (body: any, jobApplicationId: number) =>
    api.put(apiURLs.bankAccountById(jobApplicationId, "UpdateBankAccount"), body),
};

export default API;
