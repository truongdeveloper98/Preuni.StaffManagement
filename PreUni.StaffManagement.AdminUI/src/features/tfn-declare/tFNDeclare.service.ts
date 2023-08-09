import API from "./tFNDeclare.api";

export const getTFNDeclareByJobApplicationId = async (jobApplicationId: number, callback: any) => {
  try {
    const response = await API.getTFNDeclare(jobApplicationId);
    if (response.data) {
      if (callback) callback(response.data);
    }
  } catch (error) {
    console.log(error);
  }
};

export const updateTFNDeclareByJobApplicationId = async (
  body: any,
  jobApplicationId: number,
  callback: any
) => {
  try {
    const response = await API.updateTFNDeclare(body, jobApplicationId);

    if (response.data) {
      if (callback) callback(response.data);
    }
  } catch (error) {
    console.log(error);
  }
};
