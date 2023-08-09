import API from "./bankAccount.api";

export const getBankAccountByJobApplicationId = async (jobApplicationId: number, callback: any) => {
  try {
    const response = await API.getBankAccount(jobApplicationId);
    if (response.data) {
      if (callback) callback(response.data);
    }
  } catch (error) {
    console.log(error);
  }
};

export const updateBankAccount = async (body: any, jobApplicationId: number, callback: any) => {
  try {
    const response = await API.updateBankAccount(body, jobApplicationId);

    if (response.data) {
      if (callback) callback(response.data);
    }
  } catch (error) {
    console.log(error);
  }
};
