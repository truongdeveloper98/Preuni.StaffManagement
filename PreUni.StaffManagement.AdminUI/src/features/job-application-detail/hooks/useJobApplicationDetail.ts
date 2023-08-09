import PAGES from "navigation/pages";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import {
  createJobApplicationRequest,
  getJobApplicationRequest,
  updateJobApplicationRequest,
} from "../jobApplicationDetail.service";

const useJobApplicationDetail = () => {
  const [jobApplication, setJobApplication] = useState(undefined);
  const params = useParams();
  const { id: paramId } = params;
  const navigate = useNavigate();

  useEffect(() => {
    if (paramId) {
      getJobApplicationRequest(paramId, (data: any) => {
        setJobApplication(data);
      });
    }
  }, [paramId]);

  const handleCancel = () => {
    navigate(PAGES.jobApplication);
  };

  const handleSubmitForm = (data: any) => {
    if (paramId) {
      updateJobApplicationRequest(paramId, data, () => {
        navigate(PAGES.jobApplication);
      });
    } else {
      createJobApplicationRequest(data, () => {
        navigate(PAGES.jobApplication);
      });
    }
  };

  return {
    jobApplication,
    paramId,
    handleSubmitForm,
    handleCancel,
  };
};

export default useJobApplicationDetail;
