import PAGES from "navigation/pages";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { createUserRequest, getUserRequest, updateUserRequest } from "../userDetail.services";

const useUserDetail = () => {
  const [user, setUser] = useState(undefined);
  const navigate = useNavigate();
  const params = useParams();

  useEffect(() => {
    if (params?.id) {
      getUserRequest(params?.id, (data: any) => {
        setUser(data);
      });
    }
  }, [params?.id]);

  const handleCancel = () => {
    navigate(PAGES.users);
  };

  const handleSubmitForm = (data: any) => {
    if (params?.id) {
      updateUserRequest(params.id, data, () => {
        navigate(PAGES.users);
      });
    } else {
      createUserRequest(data, () => {
        navigate(PAGES.users);
      });
    }
  };

  return {
    user,
    handleSubmitForm,
    handleCancel,
  };
};
export default useUserDetail;
